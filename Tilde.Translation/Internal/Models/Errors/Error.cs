// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Text.Json.Serialization;

namespace Tilde.Translation.Models.Errors
{
    internal class Error
    {
        /// <summary>
        /// Error code
        /// </summary>
        /// <example>404007</example>
        [JsonPropertyName("code")]
        public int Code { get; set; }

        /// <summary>
        /// Textual message of error
        /// </summary>
        /// <example>Error message</example>
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
