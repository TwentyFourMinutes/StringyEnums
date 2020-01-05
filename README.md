# StringyEnums

<img alt="GitHub issues" src="https://img.shields.io/github/issues-raw/TwentyFourMinutes/StringyEnums?style=flat-square"> <img alt="GitHub release (latest by date including pre-releases)" src="https://img.shields.io/github/v/release/TwentyFourMinutes/StringyEnums?include_prereleases&style=flat-square"> ![GitHub](https://img.shields.io/github/license/TwentyFourMinutes/StringyEnums?style=flat-square)

Provides a slim and fast way for mapping enums to strings and vice versa in C#. StringyEnums targets .Net Standard so it's available to you for .Net Core and .Net Framework.

## About

This package brings a neat way to bind your enum members to a given string and vice versa. You can define it's value with simple attributes and query them by extension methods. This package prefers the runtime performance rather than memory consumption, anyhow it is _optimized at its best_ ™. 

## Installation

You can either get this package by downloading it from the NuGet Package Manager built in Visual Studio, in the [releases](https://github.com/TwentyFourMinutes/StringyEnums/releases) tab or from the official [nuget.org](https://www.nuget.org/packages/StringyEnums) website. 

Also you can install it via the **P**ackage **M**anager **C**onsole:

```
Install-Package StringyEnums
```

### Basic example

```c#
[Flags]
public enum FooEnum
{
    [StringRepresentation("tofu")]
    Foo = 1,
    [StringRepresentation("strand bar")]
    Bar = 2,
    [StringRepresentation("tofu strand bar")]
    FooBar = 4
}

EnumCore.Init(); // Put this at the start of your project

var firstResult = FooEnum.Foo.GetRepresentation();
Console.WriteLine(firstResult); // tofu

var secondResult = (FooEnum.Foo | FooEnum.Bar).GetFlagRepresentation();
foreach (var flag in secondResult)
{
    Console.WriteLine(flag); // tofu, strand bar
}

var input = "strand bar";
var thridResult = input.GetEnumFromRepresentation<FooEnum>();
Console.WriteLine(thridResult); // FooEnum.Bar
```

## How it works

StringyEnums is build upon a caching system under the hood, which is initialized at the start of the program, by calling `EnumCore.Init()`. This method adds all enums with a `StringRepresentation` to the cache which are in the same assembly that `EnumCore.Init()` called. Altough you can specify individual assemblies and enums which should be added to the internal cache.

Also note that this package is build on runtime performance on the `Release` build, which means that some methods might take for way longer than they do in reality. This is intended then the `Debug` build is not performing to much optimizing.

## Features

- C# 8.0 ready
- build upon runtime performance
- clean and simple to use
- thread save™

## Notes

If you feel like something is not working as intended or you are experiencing issues, feel free to create an issue. Also for feature requests just create an issue. For further information feel free to send me a [mail](mailto:office@twenty-four.dev) to `office@twenty-four.dev` or message me on Discord `24_minutes#7496`.





