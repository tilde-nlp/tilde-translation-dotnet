// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Net.Http;
using Tilde.Translation.Factories;

namespace Tilde.Translation
{
    /// <summary>
    /// Translation options that can be customized to 
    /// </summary>
    public sealed class TranslatorOptions
    {
        /// <summary>
        /// Default headers that will be used for all API requests
        /// </summary>
        public Dictionary<string, string?> Headers { get; set; } = [];

        /// <summary>
        /// Tilde MT API server url.
        /// <br></br>
        /// Usually used only for testing purposes
        /// </summary>
        public string ServerUrl { get; set; } = "https://mtnova-dev.tilde.lv";

        /// <summary>
        /// Http client factory that will be used for all API calls
        /// </summary>
        public IHttpClientFactory ClientFactory { get; set; } = new DefaultClientHttpFactory();

        /// <summary>
        /// Send information about platform (.NET version, OS version)
        /// </summary>
        public bool SendPlatformInfo { get; set; } = true;

        /// <summary>
        /// Information about application where <see cref="Tilde.Translation"/> library is integrated in
        /// </summary>
        public AppInfo? AppInfo { get; set; }
    }
}
