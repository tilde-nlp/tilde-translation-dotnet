// Copyright 2024 Tilde SIA (https://tilde.ai/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

namespace Tilde.Translation.Exceptions
{
    /// <summary>
    /// Too many requests in a given amount of time
    /// </summary>
    public sealed class TooManyRequestsException : TildeException
    {
        internal TooManyRequestsException() : base("Too many requests")
        {

        }
    }
}
