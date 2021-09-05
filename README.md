# Xeption
A Better Exception for .NET

This simple library is engineered to expose a new API for the standard .NET `Exception` model to allow upserting a list of values against a key in the `Data` property of that very exception.

## The Purpose
The purpose of this library is to allow .NET engineers to collect errors for any given flow or attribute before throwing the exception.

This new API simplifies the process of appending and being able to access the `Data` aspect of any exception to collect errors at any point in time.


## How to Use
To use Xeption; all you need to do is to inherit the `Xeption` model to your local Exception models as follows:

### Setup
```csharp
public class MyLocalException: Xeption
{
	// ...
}
```

### Adding Values
To add values to your `Xeption` inheriting class, you will need to call `.UpsertDataList(key, value)` API to add more values to the dictionary as follows:

```csharp
var myLocalException = new MyLocalException();
myLocalException.UpsertDataList(key: "MyKey", value: "MyValue");
```



