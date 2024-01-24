// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Text.Json.Serialization;
using Tilde.Translation.Enums.Engine;

namespace Tilde.Translation.Models.Engine
{
    /// <summary>
    /// Engine that can be used be used for translation.
    /// <br></br>
    /// If specific engine is needed for translation, use <see cref="Engine.Id"/> in translation requests
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// Engine id
        /// </summary>
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Engine name
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Source language codes
        /// </summary>
        [JsonPropertyName("sourceLanguages")]
        public IEnumerable<string> SourceLanguages { get; set; } = new List<string>();

        /// <summary>
        /// Target language codes
        /// </summary>
        [JsonPropertyName("targetLanguages")]
        public IEnumerable<string> TargetLanguages { get; set; } = new List<string>();

        /// <summary>
        /// Engine domain. If <see cref="Domains" />  are defined, this is preferred domain
        /// </summary>
        [JsonPropertyName("domain")]
        public string? Domain { get; set; } = null;

        /// <summary>
        /// Engine domains. Each language direction can support different domains for translation. 
        /// If `domains` are not available, then there is only one domain defined by `domain`
        /// </summary>
        [JsonPropertyName("domains")]
        public Dictionary<string, Domain>? Domains { get; set; }

        /// <summary>
        /// Engine status
        /// </summary>
        [JsonPropertyName("status")]
        public EngineStatus Status { get; set; }

        /// <summary>
        /// Supports term collections 
        /// </summary>
        [JsonPropertyName("supportsTermCollections")]
        public bool SupportsTermCollections { get; set; }

        /// <summary>
        /// Engine vendor
        /// </summary>
        [JsonPropertyName("vendor")]
        public Vendor EngineVendor { get; set; }
    }
}
