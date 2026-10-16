# MuseDash.Mod.Template

[![NuGet](https://img.shields.io/nuget/v/MuseDash.Mod.Template)](https://www.nuget.org/packages/MuseDash.Mod.Template)
[![NuGet](https://img.shields.io/nuget/v/MuseDash.Mod.Sdk?label=sdk)](https://www.nuget.org/packages/MuseDash.Mod.Sdk)

A `dotnet new` template for Muse Dash mods, and `MuseDash.Mod.Sdk`, the MSBuild SDK that the mods it creates build with.

## Prerequisites

- [Muse Dash](https://store.steampowered.com/app/774171/Muse_Dash/)
- [MelonLoader](https://github.com/LavaGang/MelonLoader), with the game started once so that it has generated the game's assemblies
- [.NET 10 SDK](https://dotnet.microsoft.com/download), capable of targeting `net6.0`
- A C# IDE such as Visual Studio or Rider

## Game directory

The SDK finds Muse Dash in your Steam libraries. If it is installed somewhere else, set the `MD_DIRECTORY` environment
variable to the game's folder, or set `GameFolder` in a `<project>.csproj.user` file next to the mod's project (the
template's `.gitignore` keeps it out of source control):

```xml
<Project>
    <PropertyGroup>
        <GameFolder>D:\Games\Muse Dash</GameFolder>
    </PropertyGroup>
</Project>
```

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
    - [`ILRepack`](https://github.com/ravibpatel/ILRepack.Lib.MSBuild.Task): merges the other libraries into the mod's dll,
      except MuseDashMirror, which players install themselves. Without it, they have to be installed in the game's
      `UserLibs` folder.
    - [`UniTask`](https://github.com/Cysharp/UniTask)

Selected libraries are pinned to versions that [Renovate](https://docs.renovatebot.com) keeps up to date.

## MuseDash.Mod.Sdk

Mods created from the template build with `MuseDash.Mod.Sdk`. On every build, it finds the game, references MelonLoader
and the game's assemblies, generates `MelonInfo` and a `MelonBuildInfo` class from `ModName`, `Version`, `Authors` and
`Description`, and copies the mod to the game's `Mods` folder. `MelonInfo` points at the `Main` class; if you rename it,
set `MelonModType` to its full name.

Settings that are not about the game (the `artifacts` output folder, `LangVersion` and `ImplicitUsings`) are in the
template's `Directory.Build.props`. To share them between several mods in one repository, move it to the repository's
root.

### Move a project from an earlier version of the template

- Replace `<Project Sdk="Microsoft.NET.Sdk">` with `<Project Sdk="MuseDash.Mod.Sdk/{version}">`.
- Remove the `<Reference>` items and the `CopyToModsFolder` target, and remove the game's folders and the `<Reference>`
  defaults from `Directory.Build.props`.
- Move the values in `MelonBuildInfo.cs` to `ModName`, `Version`, `Authors` and `Description`, then delete
  `MelonBuildInfo.cs`, `Properties\AssemblyInfo.cs` and `<GenerateAssemblyInfo>false</GenerateAssemblyInfo>`.
- Replace `ILRepack.targets` with the template's current one, which also merges the NuGet dependencies.

## Build the packages

```powershell
dotnet build MuseDash.Mod.Template.slnx
```

Both packages are written under `artifacts\package\<configuration>`, with the same version from MinVer. Install a local build with:

```powershell
dotnet new install .\artifacts\package\debug\MuseDash.Mod.Template.{version}.nupkg
```

Mods created from a local build reference the SDK built with it, so add that folder as a package source, for example in a
`nuget.config` next to the mod:

```xml
<configuration>
    <packageSources>
        <add key="MuseDash.Mod.Local" value="path\to\MuseDash.Mod.Template\artifacts\package\debug"/>
    </packageSources>
</configuration>
```

NuGet caches the SDK by version: after rebuilding without a new commit, delete `musedash.mod.sdk` from
`%UserProfile%\.nuget\packages`.
