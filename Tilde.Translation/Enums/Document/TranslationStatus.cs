// Copyright 2024 Tilde SIA (https://tilde.ai/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tilde.Translation.Enums.Document
{
    /// <summary>
    /// Translation status custom Json converter to support adding new Translation status without breaking existing integrations
    /// </summary>
    public class TranslationStatusStringJsonConverter : JsonConverter<TranslationStatus>
    {
        public override TranslationStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (Enum.TryParse(reader.GetString(), out TranslationStatus val))
            {
                return val;
            }

            return TranslationStatus.Unknown;
        }

        public override void Write(Utf8JsonWriter writer, TranslationStatus value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    /// <summary>
    /// Document <see cref="TranslationStatus"/> which can be used to determine if document translation is finished
    /// </summary>
    [JsonConverter(typeof(TranslationStatusStringJsonConverter))]
    public enum TranslationStatus
    {
        /// <summary>
        /// Unknown. This may indicate that library is not up to date.
        /// </summary>
        Unknown,

        #region Initializing

        /// <summary>
        /// Translation is scheduled
        /// </summary>
        Queuing,

        #endregion

        #region Processing status:

        /// <summary>
        /// Translation is initializing
        /// </summary>
        Initializing,

        /// <summary>
        /// Converting file into translation compatable format
        /// </summary>
        Extracting,

        /// <summary>
        /// Waiting for MT system to become available
        /// </summary>
        Waiting,

        /// <summary>
        /// Translation of file
        /// </summary>
        Translating,

        /// <summary>
        /// Saving translation from compatable formats to user desired
        /// </summary>
        Saving,

        #endregion

        #region Finished status:

        /// <summary>
        /// File translation was sucessfull
        /// </summary>
        Completed,

        /// <summary>
        /// Translation failed
        /// </summary>
        /// 
        Error

        #endregion
    }
}
