// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Reflection;

namespace Tilde.Translation.Internal
{
    internal static class VersionProvider
    {
        /// <summary>
        /// Build time version.
        /// <remarks>This is autogenerated value at the build time. Don't change it manually</remarks>
        /// </summary>
        private const string compiledVersion = "1.0.7.0";

        public static string GetVersion()
        {
            var assemblyVersion = Assembly.GetExecutingAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion;

            return assemblyVersion ?? compiledVersion;
        }
    }
}
