// Copyright 2024 Tilde SIA (https://tilde.ai/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tilde.Translation.Enums.Engine
{
    /// <summary>
    /// Engine Vendor custom Json converter to support adding new Vendors without breaking existing integrations
    /// </summary>
    public class EngineVendorStringJsonConverter : JsonConverter<Vendor>
    {
        public override Vendor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (Enum.TryParse(reader.GetString(), out Vendor val))
            {
                return val;
            }

            return Vendor.Unknown;
        }

        public override void Write(Utf8JsonWriter writer, Vendor value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    /// <summary>
    /// <see cref="Vendor"/> describes which translation provider will be used for specific <see cref="Engine"/>
    /// </summary>
    [JsonConverter(typeof(EngineVendorStringJsonConverter))]
    public enum Vendor
    {
        /// <summary>
        /// Vendor is unknown. This may indicate that library is not up to date.
        /// </summary>
        Unknown,

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
        Google,

        /// <summary>
        /// Microsoft
        /// </summary>
        Microsoft,

        /// <summary>
        /// Open AI
        /// </summary>
        OpenAi
    }
}
