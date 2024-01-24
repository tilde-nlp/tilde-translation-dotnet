// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Text.Json.Serialization;

namespace Tilde.Translation.Models.Text
{
    /// <summary>
    /// Text translation result
    /// </summary>
    public class Translation
    {
        /// <summary>
        /// The text domain of the translation system used to produce the translation.
        /// This property contain automatically detected domain if not specified within the request.
        /// </summary>
        [JsonPropertyName("domain")]
        public string Domain { get; set; } = string.Empty;

        /// <summary>
        /// Translation results
        /// </summary>
        [JsonPropertyName("translations")]
        public List<TextResult> Translations { get; set; } = new List<TextResult>();

        /// <summary>
        /// Detected language from source text 
        /// </summary>
        [JsonPropertyName("detectedLanguage")]
        public string DetectedLanguage { get; set; } = string.Empty;
    }
}
