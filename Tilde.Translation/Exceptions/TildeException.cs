// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

namespace Tilde.Translation.Exceptions
{
    /// <summary>
    /// Base class for all exceptions thrown by this library
    /// </summary>
    public class TildeException : Exception
    {
        internal TildeException(string message) : base(message)
        {

        }

        internal TildeException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
