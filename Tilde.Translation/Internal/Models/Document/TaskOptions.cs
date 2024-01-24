// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Text.Json.Serialization;

namespace Tilde.Translation.Internal.Models.Document
{
    /// <summary>
    /// Document translation additional options
    /// </summary>
    internal class TaskOptions
    {
        /// <summary>
        /// Break segments on br
        /// </summary>
        [JsonPropertyName("breakSegmentsOnBr")]
        public bool BreakSegmentsOnBr { get; set; }
    }
}
