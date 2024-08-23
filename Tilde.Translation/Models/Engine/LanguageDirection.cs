// Copyright 2024 Tilde SIA (https://tilde.ai/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.
using Tilde.Translation.Enums.Engine;

namespace Tilde.Translation.Models.Engine
{
    public class LanguageDirection
    {
        /// <summary>
        /// Engine id
        /// </summary>
        public Guid EngineId { get; internal set; }

        /// <summary>
        /// Engine name
        /// </summary>
        public string EngineName { get; internal set; }

        /// <summary>
        /// Source language code
        /// </summary>
        public string SourceLanguage { get; internal set; }

        /// <summary>
        /// Target language code
        /// </summary>
        public string TargetLanguage { get; internal set; }

        /// <summary>
        /// Domain
        /// </summary>
        public string? Domain { get; internal set; }

        /// <summary>
        /// Supports term collections 
        /// </summary>
        public bool SupportsTermCollections { get; internal set; }

        /// <summary>
        /// Engine vendor
        /// </summary>
        public Vendor EngineVendor { get; internal set; }
    }
}
