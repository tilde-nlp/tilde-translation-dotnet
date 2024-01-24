// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Text.Json.Serialization;

namespace Tilde.Translation.Enums.Engine
{
    /// <summary>
    /// <see cref="Vendor"/> describes which translation provider will be used for specific <see cref="Engine"/>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Vendor
    {
        /// <summary>
        /// Tilde MT
        /// </summary>
        Tilde,

        /// <summary>
        /// CEF eTranslation https://language-tools.ec.europa.eu/
        /// </summary>
        ETranslation,

        /// <summary>
        /// DeepL https://www.deepl.com/
        /// </summary>
        DeepL,

        /// <summary>
        /// Google https://translate.google.com/
        /// </summary>
        Google
    }
}
