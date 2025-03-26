// Copyright 2024 Tilde SIA (https://tilde.ai/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

namespace Tilde.Translation.Internal
{
    internal class AuthenticationProvider
    {
        private readonly string _apiKey;
        private readonly Func<string> _getToken;

        internal AuthenticationProvider(string apiKey)
        {
            _apiKey = apiKey;
        }

        internal AuthenticationProvider(Func<string> getToken)
        {
            _getToken = getToken;
        }

        /// <summary>
        /// Get authentication header
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<string, string> GetAuthenticationHeader()
        {
            if (!string.IsNullOrEmpty(_apiKey))
            {
                return new KeyValuePair<string, string>("x-api-key", _apiKey);
            }
            else
            {
                return new KeyValuePair<string, string>("authorization", $"Bearer {_getToken()}");
            }
        }
    }
}
