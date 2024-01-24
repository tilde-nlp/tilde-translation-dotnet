// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

namespace Tilde.Translation.Models
{
    /// <summary>
    /// Document translation handle that can be aquired when starting document translation
    /// and then can be used in requests to interact with document translation task
    /// </summary>
    public record DocumentHandle
    {
        /// <summary>
        /// Document translation task id
        /// </summary>
        public Guid TaskId { get; }

        internal DocumentHandle(Internal.Models.Document.Task task)
        {
            TaskId = task.Id;
        }
    }
}
