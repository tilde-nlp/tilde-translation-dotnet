// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Text.Json.Serialization;

namespace Tilde.Translation.Models.Text
{
    /// <summary>
    /// Single text translation result
    /// </summary>
    public sealed class TextResult
    {
        /// <summary>
        /// Translated text
        /// </summary>
        [JsonPropertyName("translation")]
        public string Translation { get; set; } = string.Empty;
    }
}
