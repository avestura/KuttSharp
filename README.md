# KuttSharp
.NET Package for kutt.it url shortener

## Installation
[![NuGet](https://img.shields.io/nuget/v/kuttsharp.svg)](https://www.nuget.org/packages/KuttSharp)
[![NuGet Downloads](https://img.shields.io/nuget/dt/KuttSharp.svg)](https://www.nuget.org/packages/KuttSharp)

Simply add `KuttSharp` nuget package to your project

## API
First create a new instance of `KuttApi` then use examples below:
```csharp
var api = new KuttApi("apiKey");
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
