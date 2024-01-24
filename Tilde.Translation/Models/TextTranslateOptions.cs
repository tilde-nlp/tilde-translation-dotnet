// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

namespace Tilde.Translation.Models
{
    /// <summary>
    /// Options for document translation task
    /// </summary>
    public sealed class TextTranslateOptions
    {
        /// <summary>
        /// Additional term collection that will be used to existing default collections for this document translation task
        /// </summary>
        public string? TermCollectionId { get; set; } = null;

        /// <summary>
        /// Specific Engine id to use when translating document. This will override settings like sourceLanguageCode, targetLanguageCode...
        /// </summary>
        public string? EngineId { get; set; } = null;
    }
}
