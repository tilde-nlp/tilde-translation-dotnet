// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Text.Json.Serialization;

namespace Tilde.Translation.Enums.Engine
{
    /// <summary>
    /// Engine status can be used to see if MT engine is ready for translation 
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EngineStatus
    {
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
