// Copyright 2024 Tilde SIA (https://tilde.ai/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tilde.Translation.Enums.Document
{
    /// <summary>
    /// File category custom Json converter to support adding new File categories without breaking existing integrations
    /// </summary>
    public class FileCategoryStringJsonConverter : JsonConverter<FileCategory>
    {
        public override FileCategory Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (Enum.TryParse(reader.GetString(), out FileCategory val))
            {
                return val;
            }

            return FileCategory.Unknown;
        }

        public override void Write(Utf8JsonWriter writer, FileCategory value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    /// <summary>
    /// Type of file that is available for Document translation
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FileCategory
    {
        /// <summary>
        /// Unknown. This may indicate that library is not up to date.
        /// </summary>
        Unknown,

        /// <summary>
        /// Original source file
        /// </summary>
        Source,

        /// <summary>
        /// Source converted to editable format.
        /// <br></br>
        /// For example if source file is PDF or image, then this could be docx which can be edited
        /// </summary>
        SourceConverted,

        /// <summary>
        /// Translated file
        /// </summary>
        Translated,

        /// <summary>
        /// Translated file in original file format
        /// </summary>
        TranslatedConverted
    }
}
