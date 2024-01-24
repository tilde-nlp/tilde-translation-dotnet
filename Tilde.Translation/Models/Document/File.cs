// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Text.Json.Serialization;
using Tilde.Translation.Enums.Document;

namespace Tilde.Translation.Models.Document
{
    /// <summary>
    /// File that is linked to document translation <see cref="Document"/> which can be downloaded
    /// </summary>
    public class File
    {
        /// <summary>
        /// Identifier of the file
        /// </summary>
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// File type extention
        /// </summary>
        [JsonPropertyName("extension")]
        public string Extension { get; set; } = string.Empty;

        /// <summary>
        /// File category
        /// </summary>
        [JsonPropertyName("category")]
        public FileCategory Category { get; set; }

        /// <summary>
        /// Size of a file in bytes
        /// </summary>
        [JsonPropertyName("size")]
        public long Size { get; set; }
    }
}
