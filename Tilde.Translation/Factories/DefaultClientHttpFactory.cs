// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Net.Http;

namespace Tilde.Translation.Factories
{
    /// <summary>
    /// Default HttpClient factory which creates HttpClient's
    /// </summary>
    internal class DefaultClientHttpFactory : IHttpClientFactory
    {
        public HttpClient CreateClient()
        {
            return new HttpClient();
        }

        public HttpClient CreateClient(string name)
        {
            return new HttpClient();
        }
    }
}
