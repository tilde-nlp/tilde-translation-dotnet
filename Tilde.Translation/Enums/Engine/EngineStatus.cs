// Copyright 2024 Tilde SIA (https://tilde.ai/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tilde.Translation.Enums.Engine
{
    /// <summary>
    /// Engine status custom Json converter to support adding new Engine status without breaking existing integrations
    /// </summary>
    public class EngineStatusStringJsonConverter : JsonConverter<EngineStatus>
    {
        public override EngineStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (Enum.TryParse(reader.GetString(), out EngineStatus val))
            {
                return val;
            }

            return EngineStatus.Unknown;
        }

        public override void Write(Utf8JsonWriter writer, EngineStatus value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }

    /// <summary>
    /// Engine status can be used to see if MT engine is ready for translation 
    /// </summary>
    [JsonConverter(typeof(EngineStatusStringJsonConverter))]
    public enum EngineStatus
    {
        /// <summary>
        /// Unknown. This may indicate that library is not up to date.
        /// </summary>
        Unknown,

        /// <summary>
        /// Import in progress
        /// </summary>
        Importing,

        /// <summary>
        /// Removing engine
        /// </summary>
        Deleting,

        /// <summary>
        /// Engine is in error state
        /// </summary>
        Error,

        /// <summary>
        /// Engine is running
        /// </summary>
        Running,

        /// <summary>
        /// Engine is starting
        /// </summary>
        Starting,

        /// <summary>
        /// Engine is on standby
        /// </summary>
        StandBy,

        /// <summary>
        /// Engine is off
        /// </summary>
        Off
    }
}
