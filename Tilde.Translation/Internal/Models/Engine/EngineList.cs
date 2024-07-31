// Copyright 2024 Tilde SIA (https://tilde.ai/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Text.Json.Serialization;

namespace Tilde.Translation.Models.Engine
{
    /// <summary>
    /// Available engines
    /// </summary>
    internal class EngineList
    {
        [JsonPropertyName("engines")]
        public List<Engine> Engines { get; set; } = new();
    }
}
