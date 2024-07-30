// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tilde.Translation.Exceptions;

namespace Tilde.Translation.Example.Lib
{
    public class TranslateTester
    {
        private readonly Translator translator;

        public TranslateTester(string apiKey, string serverUrl = "https://translate.tilde.ai")
        {
            var options = new TranslatorOptions()
            {
                AppInfo = new AppInfo()
                {
                    AppName = "my-app",
                    AppVersion = "1.0.0"
                },
                ServerUrl = serverUrl
            };

            translator = new Translator(apiKey, options);
        }

        public async Task TranslateTextAsync()
        {
            try
            {
                var translation = await translator.TranslateTextAsync("First sentence", "en", "lv");

                Console.WriteLine(translation.DetectedLanguage);
                Console.WriteLine(translation.Domain);
                Console.WriteLine(translation.Translations[0].Translation);
            }
            catch (TildeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task TranslateDocumentAsync(string sourceDocumentPath, string targetDocumentPath)
        {
            // Specify file to translate 
            var sourceDocument = new FileInfo(sourceDocumentPath);

            // Start document translation
            var documentHandle = await translator.TranslateDocumentAsync(sourceDocument, "en", "lv", "GENERAL");

            // Wait for document translation to finish
            var documentStatus = await translator.TranslateDocumentWaitUntilDoneAsync(documentHandle);

            Console.WriteLine(documentStatus.Status); // Completed
            Console.WriteLine(documentStatus.Substatus); // Unspecified

            // Save file 
            var fileStream = File.Create(targetDocumentPath);
            await translator.TranslateDocumentResultAsync(documentHandle, fileStream);
            fileStream.Close();
        }

        public async Task GetEnginesAsync()
        {
            var engines = await translator.GetEnginesAsync();

            Console.WriteLine(engines.First().SourceLanguages.First()); // "en"
            Console.WriteLine(engines.First().TargetLanguages.First()); // "lv"
        }
    }
}
