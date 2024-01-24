// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Text.Json.Serialization;

namespace Tilde.Translation.Enums.Document
{
    /// <summary>
    /// Translation <see cref="TranslationSubstatus"/> which further describes translation <see cref="TranslationStatus"/>.
    /// <br></br>
    /// Usually used when errors are encountered and <see cref="TranslationStatus"/> is <see cref="TranslationStatus.Error"/>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TranslationSubstatus
    {
        /// <summary>
        /// Status sub code not specified
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Bad file contents or format 
        /// </summary>
        BadFileError = 1,

        /// <summary>
        /// File type not implemented
        /// </summary>
        UnknownFileTypeError = 2,

        /// <summary>
        /// Track changes are enabled, disable to translate
        /// </summary>
        TrackChangesEnabledError = 3,

        /// <summary>
        /// No translatable text found
        /// </summary>
        NoTextExtractedError = 4,

        /// <summary>
        /// Translation timed out
        /// </summary>
        [Obsolete("Use `TranslationTimeout` instead")]
        Timeout = 5,

        /// <summary>
        /// Translation timed out
        /// </summary>
        TranslationTimeout = 5,

        /// <summary>
        /// Translation failed
        /// </summary>
        TranslationError = 6
    }
}
