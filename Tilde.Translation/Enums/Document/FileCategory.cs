// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Text.Json.Serialization;

namespace Tilde.Translation.Enums.Document
{
    /// <summary>
    /// Type of file that is available for Document translation
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FileCategory
    {
        /// <summary>
        /// Original source file
        /// </summary>
        Source,
        /// <summary>
        /// Source converted to editable format.
        /// <br></br>
        /// For example if source file is PDF or image, then this could be docx which can be edited
        /// </summary>
        SourceConverted,
        /// <summary>
        /// Translated file
        /// </summary>
        Translated,
        /// <summary>
        /// Translated file in original file format
        /// </summary>
        TranslatedConverted
    }
}
