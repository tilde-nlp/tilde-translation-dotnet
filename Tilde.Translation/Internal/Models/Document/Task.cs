// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Text.Json.Serialization;
using Tilde.Translation.Enums.Document;

namespace Tilde.Translation.Internal.Models.Document
{
    /// <summary>
    /// Document translation task
    /// </summary>
    internal class Task
    {
        /// <summary>
        /// Identifier of the file translation task which can be used to interact with translated document
        /// </summary>
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("userId")]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// Language code of the source file
        /// </summary>
        [JsonPropertyName("srcLang")]
        public string SourceLanguage { get; set; } = string.Empty;

        /// <summary>
        /// Language code of the translation
        /// </summary>
        [JsonPropertyName("trgLang")]
        public string TargetLanguage { get; set; } = string.Empty;

        /// <summary>
        /// Translation system domain to use for the file translation. Domain is detected automatically if not provided.
        /// </summary>
        [JsonPropertyName("domain")]
        public string Domain { get; set; } = string.Empty;

        /// <summary>
        /// Engine id to translate with
        /// </summary>
        [JsonPropertyName("engineId")]
        public Guid? EngineId { get; set; }

        /// <summary>
        /// Name of a source file
        /// </summary>
        [JsonPropertyName("fileName")]
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// Date and time of file upload
        /// </summary>
        [JsonPropertyName("createdAt")]
        public DateTime DbCreatedAt { get; set; }

        /// <summary>
        /// Status of the file translation task
        /// </summary>
        [JsonPropertyName("status")]
        public TranslationStatus TranslationStatus { get; set; }

        /// <summary>
        /// Substatus code that describes 'status' code
        /// </summary>
        [JsonPropertyName("substatus")]
        public TranslationSubstatus TranslationSubstatus { get; set; }

        /// <summary>
        /// Count of text segments in the source file
        /// </summary>
        [JsonPropertyName("segments")]
        public long Segments { get; set; }

        /// <summary>
        /// Count of translated text segmets for translation progress tracking
        /// </summary>
        [JsonPropertyName("translatedSegments")]
        public long SegmentsTranslated { get; set; }

        /// <summary>
        /// Files asociated with translation task
        /// </summary>
        [JsonPropertyName("files")]
        public List<Tilde.Translation.Models.Document.File> Files { get; set; } = [];

        /// <summary>
        /// Additional configuration for file translation
        /// </summary>
        [JsonPropertyName("options")]
        public TaskOptions Options { get; set; } = new();
    }
}
