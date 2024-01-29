// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Tilde.Translation.Exceptions;

namespace Tilde.Translation.Internal
{
    internal class ApiClient : IDisposable
    {
        private const HttpStatusCode HttpStatusCodeTooManyRequests = (HttpStatusCode)429;

        private readonly KeyValuePair<string, string?>[] _headers;

        private readonly HttpClient _httpClient;

        private readonly Uri _serverUrl;

        internal ApiClient(
              Uri serverUrl,
              IHttpClientFactory httpClientFactory,
              IEnumerable<KeyValuePair<string, string?>> headers
        )
        {
            _serverUrl = serverUrl;
            _headers = headers.ToArray();

            _httpClient = httpClientFactory.CreateClient();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        /// <summary>
        /// Send HTTP GET request 
        /// </summary>
        /// <param name="relativeUri"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ConnectionException"></exception>
        public async Task<HttpResponseMessage> ApiGetAsync(string relativeUri, CancellationToken cancellationToken)
        {
            using var requestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(_serverUrl, relativeUri),
                Method = HttpMethod.Get
            };
            return await ApiCallAsync(requestMessage, cancellationToken);
        }

        /// <summary>
        /// Send HTTP POST request
        /// </summary>
        /// <param name="relativeUri"></param>
        /// <param name="content"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ConnectionException"></exception>
        public async Task<HttpResponseMessage> ApiPostAsync(string relativeUri, HttpContent content, CancellationToken cancellationToken)
        {
            using var requestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(_serverUrl, relativeUri),
                Method = HttpMethod.Post,
                Content = content,

            };
            return await ApiCallAsync(requestMessage, cancellationToken);
        }

        /// <summary>
        /// Send generic HTTP request
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ConnectionException"></exception>
        private async Task<HttpResponseMessage> ApiCallAsync(
              HttpRequestMessage requestMessage,
              CancellationToken cancellationToken)
        {
            try
            {
                foreach (var header in _headers)
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                }

                return await _httpClient.SendAsync(requestMessage, cancellationToken).ConfigureAwait(false);
                // Distinguish cancellation due to user-provided token or request time-out
            }
            catch (TaskCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (TaskCanceledException ex)
            {
                throw new ConnectionException($"Request timed out: {ex.Message}", ex);
            }
            catch (HttpRequestException ex)
            {
                throw new ConnectionException($"Request failed: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new ConnectionException($"Unexpected request failure: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Validates if HTTP response encountered any error, if yes, throws corresponding error 
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        /// <exception cref="AuthorizationException"></exception>
        /// <exception cref="TooManyRequestsException"></exception>
        /// <exception cref="TextTranslationException"></exception>
        public static async Task EnsureSuccessStatusCodeAsync(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode)
            {
                return;
            }

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.Unauthorized:
                    {
                        throw new AuthorizationException();
                    }
                case HttpStatusCodeTooManyRequests:
                    {
                        throw new TooManyRequestsException();
                    }
                default:
                    {
                        string errorMessage = "";
                        int errorCode = 0;
                        bool errorParsed = false;
                        try
                        {
                            var json = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);

                            if (json.Length > 0)
                            {
                                var result = await JsonSerializer.DeserializeAsync<Tilde.Translation.Models.Errors.ApiError>(json).ConfigureAwait(false);

                                if (result?.Error != null)
                                {
                                    errorParsed = true;

                                    errorCode = result.Error.Code;
                                    errorMessage = result.Error.Message!;
                                }
                                else
                                {
                                    errorParsed = true;

                                    errorCode = (int)responseMessage.StatusCode;

                                    errorMessage = Encoding.ASCII.GetString(((MemoryStream)json).ToArray());
                                }
                            }
                        }
                        catch (Exception)
                        {
                            // do nothing 
                        }

                        if (!errorParsed)
                        {
                            errorCode = (int)responseMessage.StatusCode;
                            errorMessage = responseMessage.StatusCode.ToString();
                        }

                        throw new TextTranslationException(errorCode, errorMessage);
                    }
            }
        }
    }
}
