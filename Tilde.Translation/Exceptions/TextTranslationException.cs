// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

namespace Tilde.Translation.Exceptions
{
    /// <summary>
    /// Text translation failed
    /// </summary>
    internal class TextTranslationException : TildeException
    {
        /// <summary>
        /// Error code from translation API which can be used to differentiate errors
        /// </summary>
        public int ErrorCode { get; }

        internal TextTranslationException(int errorCode, string errorMessage) :
            base($"Translation failed with code: {errorCode}. Error message: {errorMessage}")
        {
            ErrorCode = errorCode;
        }
    }
}
