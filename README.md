# Tilde translation .NET library

[Tilde MT](https://tilde.com) is a translation platform for instant, fluent, and secure text, document. 

This .NET library provides a robust and user-friendly interface for integrating machine translation capabilities into your .NET applications. Powered by advanced neural machine translation models, it enables seamless translation of text between multiple languages.

## Installation

Using the .NET Core command-line interface (CLI) tools:

```
dotnet add package Tilde.Translation
```

Using the NuGet Command Line Interface (CLI):

```
nuget install Tilde.Translation
```

## Supported frameworks
- .NET Standard 2.0
- .NET 6.0

## Usage

Initialize Translator using apiKey. 
```c#
using Tilde.Translation;
using Tilde.Translation.Exceptions;

var apiKey = "00000000-0000-0000-0000-000000000000"; 
var translator = new Translator(authKey);
```

When implementing this library in your application, please provide information about application where this library will be used:

```c#
var options = new TranslatorOptions()
{
    AppInfo = new AppInfo()
    {
        AppName = "my-app",
        AppVersion = "1.0.0",
    }
};
var translator = new Translator("00000000-0000-0000-0000-000000000000", options);
```

### Translating text

```c#
var translation = await translator.TranslateTextAsync("First sentence", "en", "lv");

Console.WriteLine(translation.DetectedLanguage); // null | "en" | ...
Console.WriteLine(translation.Domain); // "finance"
Console.WriteLine(translation.Translations[0].Translation); // "Pirmais teikums"
```

### Translating documents

```c#
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
```

### Get available engines

```c#
var engines = await translator.GetEnginesAsync();

Console.WriteLine(engines.First().SourceLanguages.First()); // "en"
Console.WriteLine(engines.First().TargetLanguages.First()); // "lv"
```

# Contributing
We welcome contributions from the community! If you encounter issues, have suggestions, or want to add new features, please submit a pull request or open an issue on the GitHub repository.

# License
This library is licensed under the **MIT** License, allowing flexibility for both personal and commercial use.