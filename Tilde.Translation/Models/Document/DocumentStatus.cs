// Copyright 2024 Tilde SIA (https://tilde.ai/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using Tilde.Translation.Enums.Document;

namespace Tilde.Translation.Models.Document
{
    /// <summary>
    /// Status of document translation
    /// </summary>
    public class DocumentStatus
    {
        /// <summary>
        /// Name of a source file
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Status of the file translation task
        /// </summary>
        public TranslationStatus Status { get; }

        /// <summary>
        /// Substatus code that further describes <see cref="Status"/> 
        /// </summary>
        public TranslationSubstatus Substatus { get; }

        /// <summary>
        /// All files asociated with translation task
        /// </summary>
        public List<File> Files { get; }

        /// <summary>
        /// Document translation task is finished.<br></br>
        /// It could be either in <see cref="TranslationStatus.Error"/> or <see cref="TranslationStatus.Completed"/> state
        /// </summary>
        public bool Done => Status == TranslationStatus.Completed || Status == TranslationStatus.Error;

        internal DocumentStatus(Internal.Models.Document.Task task)
        {
            FileName = task.FileName;
            Status = task.TranslationStatus;
            Substatus = task.TranslationSubstatus;
            Files = task.Files;
        }
    }
}
