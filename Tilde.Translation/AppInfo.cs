﻿// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

namespace Tilde.Translation
{
    /// <summary>
    /// Information about application where <see cref="Tilde.Translation"/> library is integrated in
    /// </summary>
    public class AppInfo
    {
        /// <summary>
        /// Application name where this library will be used
        /// </summary>
        public string? AppName { get; set; }

        /// <summary>
        /// Version of application where this library will be used
        /// </summary>
        public string? AppVersion { get; set; }
    }
}