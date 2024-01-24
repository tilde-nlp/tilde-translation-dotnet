// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Text.Json.Serialization;

namespace Tilde.Translation.Internal.Models.Text
{
    /// <summary>
    /// Text translation request model
    /// </summary>
    internal class TranslationRequest
    {
        /// <summary>
        /// Source language code
        /// </summary>
        [JsonPropertyName("srcLang")]
        public string? SourceLanguage { get; set; } = null;

        /// <summary>
        /// Target language code
        /// </summary>
        [JsonPropertyName("trgLang")]
        public string TargetLanguage { get; set; } = string.Empty;

        /// <summary>
        /// Translation domain
        /// </summary>
        [JsonPropertyName("domain")]
        public string? Domain { get; set; }

        /// <summary>
        /// Texts to translate
        /// </summary>
        [JsonPropertyName("text")]
        public IEnumerable<string> Text { get; set; } = [];

        /// <summary>
        /// Additional terminology collections to use for translation request
        /// </summary>
        [JsonPropertyName("termCollections")]
        public IEnumerable<Guid> TermCollections { get; set; } = [];

        /// <summary>
        /// Specific engine id to use
        /// <br></br>
        /// This will override <see cref="SourceLanguage"/> and <see cref="TargetLanguage"/> when used
        /// </summary>
        [JsonPropertyName("engineId")]
        public Guid? EngineId { get; set; }
    }
}
