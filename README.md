# MuseDash.Mod.Template

[![NuGet](https://img.shields.io/nuget/v/MuseDash.Mod.Template)](https://www.nuget.org/packages/MuseDash.Mod.Template)

A `dotnet new` template for Muse Dash mods.

## Prerequisites

- [Muse Dash](https://store.steampowered.com/app/774171/Muse_Dash/)
- [MelonLoader](https://github.com/LavaGang/MelonLoader)
- [.NET 10 SDK](https://dotnet.microsoft.com/download), capable of targeting `net6.0`
- A C# IDE such as Visual Studio or Rider

## Configure the game directory

Set the `MD_DIRECTORY` environment variable to the Muse Dash installation directory, then restart the IDE and terminal.

## Install

```powershell
dotnet new install MuseDash.Mod.Template
```

Update an installed copy with:

```powershell
dotnet new update
```

## Create a mod

The template is available as `MuseDash Mod` in Visual Studio and Rider, or as `musedashmod` on the command line:

In Visual Studio, find it under `Create a new project -> MuseDash Mod`:

![Visual Studio](Intros/VS.png)

In Rider, find it under `New Solution -> Custom Templates -> MuseDash Mod`:

![Rider](Intros/Rider.png)

```powershell
dotnet new musedashmod -n MyMod
```

No optional package is selected by default. To add one or more useful libraries, repeat `--UsefulLibs`:

```powershell
dotnet new musedashmod -n MyMod `
    --UsefulLibs MuseDashMirror `
    --UsefulLibs UniTask
```

### Options

- `--Patching`: Generate a patch class. Defaults to `true`.
- `--Setting`: Select `None`, `MelonLoader`, or `Tomlet`. Defaults to `None`.
- `--UsefulLibs`: Select any combination of:
    - [`MuseDashMirror`](https://github.com/MDMods/MuseDashMirror)
    - [`ILRepack`](https://github.com/ravibpatel/ILRepack.Lib.MSBuild.Task)
    - [`UniTask`](https://github.com/Cysharp/UniTask)

Selected libraries use NuGet floating version `*`, so restore resolves the latest stable version. Prerelease versions are not selected.

## Build the template package

```powershell
dotnet build MuseDash.Mod.Template.slnx
```

The package is written under `artifacts\package\<configuration>`. Install a local build with:

```powershell
dotnet new install .\artifacts\package\debug\MuseDash.Mod.Template.{version}.nupkg
```
