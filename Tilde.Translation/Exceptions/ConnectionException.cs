// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

namespace Tilde.Translation.Exceptions
{
    /// <summary>
    /// Connection error occoured while trying to connect to Tilde API
    /// </summary>
    public sealed class ConnectionException : TildeException
    {
        internal ConnectionException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
