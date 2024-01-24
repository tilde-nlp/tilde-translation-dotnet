// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Text.Json.Serialization;

namespace Tilde.Translation.Models.Errors
{
    /// <summary>
    /// Generic API error model
    /// </summary>
    internal class ApiError
    {
        [JsonPropertyName("error")]
        public Error Error { get; set; } = new Error();
    }
}
