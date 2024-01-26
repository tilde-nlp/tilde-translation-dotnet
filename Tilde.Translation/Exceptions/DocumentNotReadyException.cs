// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using Tilde.Translation.Enums.Document;

namespace Tilde.Translation.Exceptions
{
    /// <summary>
    /// Document is still processing
    /// </summary>
    public sealed class DocumentNotReadyException : TildeException
    {
        public TranslationStatus Status { get; }

        internal DocumentNotReadyException(TranslationStatus status) : base($"Document is still processing and is in status: {status}")
        {
            Status = status;
        }
    }
}
