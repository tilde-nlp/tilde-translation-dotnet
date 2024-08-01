// Copyright 2024 Tilde SIA (https://tilde.ai/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using Tilde.Translation.Enums.Document;

namespace Tilde.Translation.Exceptions
{
    /// <summary>
    /// Document translation failed with <see cref="TranslationStatus.Error"/>
    /// </summary>
    public sealed class DocumentTranslationException : TildeException
    {
        /// <summary>
        /// <see cref="TranslationSubstatus"/> of document translation
        /// </summary>
        public TranslationSubstatus Substatus { get; }

        internal DocumentTranslationException(TranslationSubstatus substatus) : base($"Document translation failed with error: {substatus}")
        {
            Substatus = substatus;
        }
    }
}
