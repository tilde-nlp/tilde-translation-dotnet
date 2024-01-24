// Copyright 2024 Tilde SIA (https://tilde.com/)
// Use of this source code is governed by an MIT
// license that can be found in the LICENSE file.

using Tilde.Translation;
using Tilde.Translation.Exceptions;

var options = new TranslatorOptions()
{
    AppInfo = new AppInfo()
    {
        AppName = "my-app",
        AppVersion = "1.0.0",
    }
};

string apiKey = "";
var translator = new Translator(apiKey, options);

async Task TranslateTextAsync()
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

async Task TranslateDocumentAsync()
{
    // Specify file to translate 
    var sourceDocument = new FileInfo(@"./Document/ExampleDocument.txt");

    // Start document translation
    var documentHandle = await translator.TranslateDocumentAsync(sourceDocument, "en", "lv");

    // Wait for document translation to finish
    var documentStatus = await translator.TranslateDocumentWaitUntilDoneAsync(documentHandle);

    Console.WriteLine(documentStatus.Status); // Completed
    Console.WriteLine(documentStatus.Substatus); // Unspecified

    // Save file 
    var fileStream = File.Create(@"./Document/ExampleDocumentResult.txt");
    await translator.TranslateDocumentResultAsync(documentHandle, fileStream);
    fileStream.Close();
}

async Task GetEnginesAsync()
{
    var engines = await translator.GetEnginesAsync();

    Console.WriteLine(engines.First().SourceLanguages.First()); // "en"
    Console.WriteLine(engines.First().TargetLanguages.First()); // "lv"
}

await TranslateTextAsync();
await TranslateDocumentAsync();
await GetEnginesAsync();
