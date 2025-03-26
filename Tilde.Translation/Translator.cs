// Copyright 2024 Tilde SIA (https://tilde.ai/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using Tilde.Translation.Exceptions;
using Tilde.Translation.Internal;
using Tilde.Translation.Models;
using Tilde.Translation.Models.Document;
using Tilde.Translation.Models.Engine;

namespace Tilde.Translation
{
    /// <summary>
    /// Translation client for Tilde MT api 
    /// </summary>
    public sealed class Translator : IDisposable
    {
        /// <summary>
        /// Poll interval for document translation status while waiting for document translation to finish
        /// </summary>
        private readonly TimeSpan DocumentStatusWaitPollInterval = TimeSpan.FromSeconds(1);

        /// <summary>
        /// API client for making all API requests
        /// </summary>
        private ApiClient _client;

        /// <summary>
        /// Initializes <see cref="Translator"/> client with ApiKey authentication
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="options"></param>
        /// <exception cref="ArgumentNullException">apiKey was not provided</exception>
        /// <exception cref="ArgumentException">apiKey is not valid</exception>
        public Translator(string apiKey, TranslatorOptions? options = null)
        {
            if (apiKey == null)
            {
                throw new ArgumentNullException(nameof(apiKey));
            }

            if (!Guid.TryParse(apiKey, out var parsedApiKey))
            {
                throw new ArgumentException($"{nameof(apiKey)} is not valid API key");
            }

            Initialize(new AuthenticationProvider(apiKey), options);
        }

        /// <summary>
        /// Initializes <see cref="Translator"/> client with Json Web token authentication
        /// </summary>
        /// <param name="getToken">Function to get Bearer token to use for requests</param>
        /// <param name="options"></param>
        public Translator(Func<string> getToken, TranslatorOptions? options = null)
        {
            Initialize(new AuthenticationProvider(getToken), options);
        }

        private void Initialize(AuthenticationProvider authentication, TranslatorOptions? options = null)
        {
            options ??= new TranslatorOptions();

            if (options.AppInfo != null)
            {
                if (string.IsNullOrWhiteSpace(options.AppInfo.AppVersion))
                {
                    throw new ArgumentNullException($"{nameof(options)}.{nameof(options.AppInfo)}.{nameof(options.AppInfo.AppVersion)}");
                }

                if (string.IsNullOrWhiteSpace(options.AppInfo.AppName))
                {
                    throw new ArgumentNullException($"{nameof(options)}.{nameof(options.AppInfo)}.{nameof(options.AppInfo.AppName)}");
                }
            }

            var headers = new Dictionary<string, string?>(options.Headers, StringComparer.OrdinalIgnoreCase);

            if (!headers.ContainsKey("User-Agent"))
            {
                headers.Add("User-Agent", ConstructUserAgentString(options.AppInfo, options.SendPlatformInfo));
            }

            if (!headers.ContainsKey("X-APP-ID") && options.AppInfo?.AppId != null)
            {
                headers.Add("X-APP-ID", options.AppInfo?.AppId);
            }

            if (!string.IsNullOrEmpty(options.AppInfo?.Origin))
            {
                headers.Add("Origin", options.AppInfo?.Origin);
            }

            _client = new ApiClient(new Uri(options.ServerUrl), options.ClientFactory, headers, authentication);
        }

        /// <summary>
        /// Translate texts 
        /// </summary>
        /// <param name="texts">Texts to translate. Must not be empty</param>
        /// <param name="sourceLanguageCode">Language code of input language, or null for auto-detection</param>
        /// <param name="targetLanguageCode">Language code of target language</param>
        /// <param name="domain">Translation domain</param>
        /// <param name="options">Additional options for translation request</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="TildeException"></exception>
        public async Task<Models.Text.Translation> TranslateTextAsync(
              IEnumerable<string> texts,
              string? sourceLanguageCode,
              string targetLanguageCode,
              string? domain = null,
              TranslateOptions? options = null,
              CancellationToken cancellationToken = default)
        {
            options ??= new();

            var emptyTexts = texts.Any(string.IsNullOrEmpty);
            if (emptyTexts)
            {
                throw new ArgumentException($"{nameof(texts)} contains empty texts");
            }

            List<Guid> termCollections = [];
            if (options.TermCollectionId != null)
            {
                if (Guid.TryParse(options.TermCollectionId, out var parsedGuid))
                {
                    termCollections.Add(parsedGuid);
                }
                else
                {
                    throw new ArgumentException($"'{options.TermCollectionId}' is not a valid terminology collection");
                }
            }

            Guid? parsedEngineId = null;

            if (options.EngineId != null)
            {
                if (Guid.TryParse(options.EngineId, out var parsedGuid))
                {
                    parsedEngineId = parsedGuid;
                }
                else
                {
                    throw new ArgumentException($"'{options.EngineId}' is not valid Engine id");
                }
            }

            var requestData = new Internal.Models.Text.TranslationRequest()
            {
                SourceLanguage = sourceLanguageCode,
                Domain = domain,
                EngineId = parsedEngineId,
                TargetLanguage = targetLanguageCode,
                TermCollections = termCollections,
                Text = texts
            };

            HttpContent content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");

            using var responseMessage = await _client.ApiPostAsync("/api/translate/text", content, cancellationToken).ConfigureAwait(false);

            await ApiClient.EnsureSuccessStatusCodeAsync(responseMessage).ConfigureAwait(false);

            var json = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var result = await JsonSerializer.DeserializeAsync<Models.Text.Translation>(json).ConfigureAwait(false);

            return result!;
        }

        /// <summary>
        /// Translate single text
        /// </summary>
        /// <param name="text">Text to translate</param>
        /// <param name="sourceLanguageCode">Language code of input language, or null for auto-detection</param>
        /// <param name="targetLanguageCode">Language code of target language</param>
        /// <param name="domain">Translation domain</param>
        /// <param name="options">Additional options for translation request</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="TildeException"></exception>
        public async Task<Models.Text.Translation> TranslateTextAsync(
              string text,
              string? sourceLanguageCode,
              string targetLanguageCode,
              string? domain = null,
              TranslateOptions? options = null,
              CancellationToken cancellationToken = default)
        {
            var result = await TranslateTextAsync(
                    new[] { text },
                    sourceLanguageCode,
                    targetLanguageCode,
                    domain,
                    options,
                    cancellationToken
                )
                .ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// Start document translation process. This will not wait for translation to finish. 
        /// <br></br>
        /// To follow translation progress use <see cref="TranslateDocumentStatusAsync(DocumentHandle, CancellationToken)"/>
        /// <br></br>
        /// Or to wait for translation to finish use <see cref="TranslateDocumentWaitUntilDoneAsync(DocumentHandle, CancellationToken)"/>
        /// <br></br>
        /// And when translation is completed sucessfully you download result using <see cref="TranslateDocumentResultAsync(DocumentHandle, Stream, CancellationToken)"/> 
        /// </summary>
        /// <param name="inputFileInfo">document to translate</param>
        /// <param name="sourceLanguageCode">Language code of input language, or null for auto-detection</param>
        /// <param name="targetLanguageCode">Language code of target language</param>
        /// <param name="domain">Translation domain</param>
        /// <param name="options">Additional options for translation request</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="DocumentHandle"/> which can be used to interact with document that is being translated</returns>
        /// <exception cref="TildeException"></exception>
        public async Task<DocumentHandle> TranslateDocumentAsync(
            FileInfo inputFileInfo,
            string? sourceLanguageCode,
            string targetLanguageCode,
            string? domain = null,
            TranslateOptions? options = null,
            CancellationToken cancellationToken = default
        )
        {
            using var file = inputFileInfo.OpenRead();
            var fileName = inputFileInfo.Name;

            var handle = await TranslateDocumentAsync(
                    file,
                    fileName,
                    sourceLanguageCode,
                    targetLanguageCode,
                    domain,
                    options,
                    cancellationToken
                ).ConfigureAwait(false);

            return handle;
        }

        /// <summary>
        /// Start document translation process. This will not wait for translation to finish. 
        /// <br></br>
        /// To follow translation progress use <see cref="TranslateDocumentStatusAsync(DocumentHandle, CancellationToken)"/>
        /// <br></br>
        /// Or to wait for translation to finish use <see cref="TranslateDocumentWaitUntilDoneAsync(DocumentHandle, CancellationToken)"/>
        /// <br></br>
        /// And when translation is completed sucessfully you download result using <see cref="TranslateDocumentResultAsync(DocumentHandle, Stream, CancellationToken)"/> 
        /// </summary>
        /// <param name="file">File contents to translate</param>
        /// <param name="fileName">Name of <paramref name="file"/></param>
        /// <param name="sourceLanguageCode">Language code of input language, or null for auto-detection</param>
        /// <param name="targetLanguageCode">Language code of target language</param>
        /// <param name="domain">Translation domain</param>
        /// <param name="options">Additional options for translation request</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="DocumentHandle"/> which can be used to interact with document that is being translated</returns>
        /// <exception cref="TildeException"></exception>
        public async Task<DocumentHandle> TranslateDocumentAsync(
            Stream file,
            string fileName,
            string? sourceLanguageCode,
            string targetLanguageCode,
            string? domain = null,
            TranslateOptions? options = null,
            CancellationToken cancellationToken = default)
        {
            options ??= new();

            using var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(file);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "file",
                FileName = fileName
            };

            content.Add(fileContent);
            if (sourceLanguageCode != null)
            {
                content.Add(new StringContent(sourceLanguageCode), "srcLang");
            }
            content.Add(new StringContent(targetLanguageCode), "trgLang");
            if (domain != null)
            {
                content.Add(new StringContent(domain), "domain");
            }
            if (options.TermCollectionId != null)
            {
                content.Add(new StringContent(options.TermCollectionId), "termCollections[0]");
            }
            if (options.EngineId != null)
            {
                content.Add(new StringContent(options.EngineId), "engineId");
            }
            using var responseMessage = await _client.ApiPostAsync($"/api/translate/file", content, cancellationToken).ConfigureAwait(false);

            await ApiClient.EnsureSuccessStatusCodeAsync(responseMessage).ConfigureAwait(false);

            var json = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var result = await JsonSerializer.DeserializeAsync<Internal.Models.Document.Task>(json).ConfigureAwait(false);

            return new DocumentHandle(result!);
        }

        /// <summary>
        /// Get extended status of document translation
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Internal.Models.Document.Task"/> which can be used to check if translation was successfull</returns>
        /// <exception cref="TildeException"></exception>
        internal async Task<Internal.Models.Document.Task> TranslateDocumentExtendedStatusAsync(
            DocumentHandle handle,
            CancellationToken cancellationToken = default)
        {
            using var responseMessage = await _client.ApiGetAsync($"/api/translate/file/{handle.TaskId}", cancellationToken).ConfigureAwait(false);

            await ApiClient.EnsureSuccessStatusCodeAsync(responseMessage).ConfigureAwait(false);

            var json = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var result = await JsonSerializer.DeserializeAsync<Internal.Models.Document.Task>(json, cancellationToken: cancellationToken).ConfigureAwait(false);

            return result!;
        }

        /// <summary>
        /// Get status of document translation task
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="DocumentStatus"/> which can be used to check if translation was successfull</returns>
        /// <exception cref="TildeException"></exception>
        public async Task<DocumentStatus> TranslateDocumentStatusAsync(
            DocumentHandle handle,
            CancellationToken cancellationToken = default)
        {
            var extendedStatus = await TranslateDocumentExtendedStatusAsync(handle, cancellationToken).ConfigureAwait(false);

            return new DocumentStatus(extendedStatus!);
        }

        /// <summary>
        /// Wait for document translation task to finish
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="DocumentStatus"/> which can be used to check if translation was successfull</returns>
        /// <exception cref="TildeException"></exception>
        public async Task<DocumentStatus> TranslateDocumentWaitUntilDoneAsync(
            DocumentHandle handle,
            CancellationToken cancellationToken = default
        )
        {
            var documentStatus = await TranslateDocumentStatusAsync(handle, cancellationToken).ConfigureAwait(false);

            while (!documentStatus.Done)
            {
                await Task.Delay(DocumentStatusWaitPollInterval, cancellationToken).ConfigureAwait(false);

                documentStatus = await TranslateDocumentStatusAsync(handle, cancellationToken).ConfigureAwait(false);
            }

            return documentStatus;
        }

        /// <summary>
        /// Get document result. Result file can be with different extension and format than input file.
        /// <br></br>
        /// To see what file results are available use <see cref="DocumentStatus"/>
        /// <br></br>
        /// If specific document file is needed, use <see cref="TranslateDocumentResultAsync(DocumentHandle, Stream, Guid, CancellationToken)"/> providing file id
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="outputFile">Stream where to store translation file contents</param>
        /// <param name="cancellationToken"></param>
        /// <returns><see cref="Models.Document.File"/> File that was returned</returns>
        /// <exception cref="DocumentNotReadyException"></exception>
        /// <exception cref="DocumentTranslationException">Document translation was not successfull</exception>
        /// <exception cref="TildeException"></exception>
        public async Task<Models.Document.File> TranslateDocumentResultAsync(
            DocumentHandle handle,
            Stream outputFile,
            CancellationToken cancellationToken = default
        )
        {
            var documentStatus = await TranslateDocumentStatusAsync(handle, cancellationToken).ConfigureAwait(false);

            if (!documentStatus.Done)
            {
                throw new DocumentNotReadyException(documentStatus.Status);
            }

            if (documentStatus.Status == Enums.Document.TranslationStatus.Error)
            {
                throw new DocumentTranslationException(documentStatus.Substatus);
            }

            var sourceFile = documentStatus.Files
                .First(item => item.Category == Enums.Document.FileCategory.Source);

            var translatedFiles = documentStatus.Files
                .Where(item =>
                    item.Category == Enums.Document.FileCategory.Translated ||
                    item.Category == Enums.Document.FileCategory.TranslatedConverted
                );

            Models.Document.File? resultFile = null;

            // First of all, try the same extension file
            resultFile = translatedFiles
                .FirstOrDefault(item => item.Extension == sourceFile.Extension);

            if (resultFile == null)
            {
                // If same extension result is not found, then just return first result
                resultFile = translatedFiles.First();
            }

            await TranslateDocumentResultAsync(handle, outputFile, resultFile.Id, cancellationToken).ConfigureAwait(false);

            return resultFile;
        }

        /// <summary>
        /// Get document result
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="outputFile">Stream where to store translation file contents</param>
        /// <param name="resultFileId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="TildeException"></exception>
        public async Task TranslateDocumentResultAsync(
            DocumentHandle handle,
            Stream outputFile,
            Guid resultFileId,
            CancellationToken cancellationToken = default
        )
        {
            using var responseMessage = await _client.ApiGetAsync($"/api/translate/file/{handle.TaskId}/{resultFileId}", cancellationToken).ConfigureAwait(false);

            await ApiClient.EnsureSuccessStatusCodeAsync(responseMessage).ConfigureAwait(false);

            await responseMessage.Content.CopyToAsync(outputFile).ConfigureAwait(false);
        }

        /// <summary>
        /// Get all MT engines
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="TildeException"></exception>
        public async Task<IEnumerable<Models.Engine.Engine>> GetEnginesAsync(
            CancellationToken cancellationToken = default
        )
        {
            using var responseMessage = await _client.ApiGetAsync($"/api/engine", cancellationToken).ConfigureAwait(false);

            await ApiClient.EnsureSuccessStatusCodeAsync(responseMessage).ConfigureAwait(false);

            var json = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var result = await JsonSerializer.DeserializeAsync<Models.Engine.EngineList>(json, cancellationToken: cancellationToken).ConfigureAwait(false);

            return result!.Engines;
        }

        /// <summary>
        /// Get available language directions
        /// </summary>
        /// <param name="sourceLanguageCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<LanguageDirection> GetLanguageDirections(
            string? sourceLanguageCode = null,
            [EnumeratorCancellation] CancellationToken cancellationToken = default
        )
        {
            var engines = await GetEnginesAsync(cancellationToken);

            foreach (var engine in engines)
            {
                if (engine.Domains != null && engine.Domains.Count > 0)
                {
                    foreach (var domain in engine.Domains)
                    {
                        foreach (var sourceLanguage in domain.Value.Languages)
                        {
                            // Filter by source language
                            if (sourceLanguageCode != null && sourceLanguage.Key != sourceLanguageCode)
                            {
                                continue;
                            }

                            foreach (var targetLanguage in sourceLanguage.Value)
                            {
                                yield return new LanguageDirection()
                                {
                                    Domain = engine.Domain,
                                    EngineVendor = engine.EngineVendor,
                                    EngineId = engine.Id,
                                    EngineName = engine.Name,
                                    SupportsTermCollections = engine.SupportsTermCollections,
                                    SourceLanguage = sourceLanguage.Key,
                                    TargetLanguage = targetLanguage
                                };
                            }
                        }
                    }
                }
                else
                {
                    foreach (var sourceLanguage in engine.SourceLanguages)
                    {
                        // Filter by source language
                        if (sourceLanguageCode != null && sourceLanguage != sourceLanguageCode)
                        {
                            continue;
                        }

                        foreach (var targetLanguage in engine.TargetLanguages)
                        {
                            yield return new LanguageDirection()
                            {
                                Domain = engine.Domain,
                                EngineVendor = engine.EngineVendor,
                                EngineId = engine.Id,
                                EngineName = engine.Name,
                                SupportsTermCollections = engine.SupportsTermCollections,
                                SourceLanguage = sourceLanguage,
                                TargetLanguage = targetLanguage
                            };
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get library version
        /// </summary>
        /// <returns></returns>
        public static string Version()
        {
            var version = VersionProvider.GetVersion();

            return version;
        }

        /// <summary>
        /// Create user agent string from appInfo
        /// </summary>
        /// <param name="sendPlatformInfo"></param>
        /// <param name="appInfo"></param>
        /// <returns></returns>
        private static string ConstructUserAgentString(AppInfo? appInfo = null, bool sendPlatformInfo = true)
        {
            var platformInfoString = $"tilde-dotnet/{Version()}";
            if (sendPlatformInfo)
            {
                var osDescription = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
                var clrVersion = Environment.Version.ToString();
                platformInfoString += $" ({osDescription}) dotnet-clr/{clrVersion}";
            }
            if (appInfo != null)
            {
                platformInfoString += $" {appInfo.AppName}/{appInfo.AppVersion}";
            }
            return platformInfoString;
        }

        /// <summary>
        /// Dispose client
        /// </summary>
        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
