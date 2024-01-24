// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

namespace Tilde.Translation.Exceptions
{
    /// <summary>
    /// Provided api key is not valid or is expired
    /// </summary>
    public sealed class AuthorizationException : TildeException
    {
        internal AuthorizationException() : base($"Authorization failed")
        {

        }
    }
}
