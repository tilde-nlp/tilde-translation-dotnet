// Copyright 2024 Tilde SIA (https://tilde.ai/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

var apiKey = "";
var tester = new Tilde.Translation.Example.Lib.TranslateTester(apiKey);

await tester.TranslateTextAsync();
await tester.TranslateDocumentAsync("./Document/ExampleDocument.txt", "./Document/ExampleDocumentResult.txt");
await tester.GetEnginesAsync();await tester.GetLanguageDirectionsAsync();