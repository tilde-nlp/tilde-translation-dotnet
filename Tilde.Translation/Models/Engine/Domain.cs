// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Text.Json.Serialization;

namespace Tilde.Translation.Models.Engine
{
    /// <summary>
    /// Available source and target languages for specific domain
    /// </summary>
    public class Domain
    {
        /// <summary>
        /// Languages. 
        ///     Keys: Source language codes 
        ///     Values: Target language codes 
        /// </summary>
        [JsonPropertyName("languages")]
        public Dictionary<string, HashSet<string>> Languages { get; set; } = [];
    }
}
