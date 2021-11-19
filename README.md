# KuttSharp
.NET Package for kutt.it url shortener

⚠️ WARNING: KuttSharp currently only supports v1 of the Kutt API. I have no plans to support v2 currently, therefore you might want to look for an alternative.

## Installation
[![NuGet](https://img.shields.io/nuget/v/kuttsharp.svg)](https://www.nuget.org/packages/KuttSharp)
[![NuGet Downloads](https://img.shields.io/nuget/dt/KuttSharp.svg)](https://www.nuget.org/packages/KuttSharp)
[![Build status](https://dev.azure.com/Avestura/KuttSharp/_apis/build/status/KuttSharp-.NET%20Desktop-CI)](https://dev.azure.com/Avestura/KuttSharp/_build/latest?definitionId=8)

Simply add `KuttSharp` nuget package to your project

## API
First create a new instance of `KuttApi` then use examples below:
```csharp
// Use defualt Kutt server
var api = new KuttApi("apiKey");

// Use a self-hosted Kutt server as string or System.Uri
var api = new KuttApi("apiKey", "https://MyOwnSelfHostedKutt.it")
```
#### Submit
```csharp
var submitedItem = await api.SubmitAsync(
      target: "https://example.com",
      customUrl: "customUrl",
      password: "password",
      reuse: true
    );

// Now you can use properties of created item
if (submitedItem.IsPasswordRequired)
{
    Console.WriteLine(submitedItem.CreatedAt.Year);
}
```

#### Delete
```csharp
await api.DeleteAsync(id: "url_id");
```

#### GetUrls
```csharp
var list = await api.GetUrlsAsync();
Console.WriteLine($"First item visits count: {list[0].Visits}");
```

#### GetStats
```csharp
var stats = await api.GetStatsAsync(id: "url_id");

Console.WriteLine(stats.LastWeek.ClientStats.Referrer[0].Name);
```

## Error handling
```csharp
var api = new KuttApi("apiKey");

try
{
    var result = await api.SubmitAsync("https://example.com", reuse: true);
}
catch (KuttException kex) when (kex.Message == "No id has been provided.")
{
    // Handle custom kutt exception
}
catch (KuttException)
{
    // Handle all other kutt exceptions
}
catch
{
    // Handle other errors
}
```
