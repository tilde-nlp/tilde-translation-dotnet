// Copyright 2024 Tilde SIA (https://tilde.ai/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using System.Threading.Tasks;

namespace Tilde.Translation.Example.Framework
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var apiKey = "";
            var tester = new Lib.TranslateTester(apiKey);

            await tester.TranslateTextAsync();
            await tester.TranslateDocumentAsync("./Document/ExampleDocument.txt", "./Document/ExampleDocumentResult.txt");
            await tester.GetEnginesAsync();
            await tester.GetLanguageDirectionsAsync();
        }
    }
}
