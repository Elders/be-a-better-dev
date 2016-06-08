# Building nuget package#

## Project structure ##
```
{Solution-folder}
- src (folder)
    - {Solution-file} (.sln file)
    - {Project-folder-1}
        - RELEASE_NOTES.md
        - {Nuget-spec-file-1} (.nuspec)
    - {Project-folder-N}
        - RELEASE_NOTES.md
        - {Nuget-spec-file-N} (.nuspec)
- .editorconfig (file)
- .gitattributes (file)
- .gitignore (file)
- build.cmd (file)
- LICENSE (file)
- release-local.cmd (file)
- release.cmd (file)
```

### Example project structure ###
```
Hystrix.NET.Contrib.ServoPublisher
- src
    - Hystrix.NET.Contrib.ServoPublisher.sln
    - Hystrix.NET.Contrib.ServoPublisher
        - RELEASE_NOTES.md
        - Hystrix.NET.Contrib.ServoPublisher.nuspec
- .editorconfig (file)
- .gitattributes (file)
- .gitignore (file)
- build.cmd (file)
- LICENSE (file)
- release-local.cmd (file)
- release.cmd (file)
```


### build.cmd file ###
```
@echo off

SETLOCAL

SET NUGET=%LocalAppData%\NuGet\NuGet.exe
@echo off

SETLOCAL

SET NUGET=%LocalAppData%\NuGet\NuGet.exe
SET FAKE=%LocalAppData%\FAKE\tools\Fake.exe
SET NYX=%LocalAppData%\Nyx\tools\build.fsx
SET GITVERSION=%LocalAppData%\GitVersion.CommandLine\tools\GitVersion.exe
SET MSBUILD14_TOOLS_PATH="%ProgramFiles(x86)%\MSBuild\14.0\bin\MSBuild.exe"
SET MSBUILD12_TOOLS_PATH="%ProgramFiles(x86)%\MSBuild\12.0\bin\MSBuild.exe"
SET BUILD_TOOLS_PATH=%MSBUILD14_TOOLS_PATH%

IF NOT EXIST %MSBUILD14_TOOLS_PATH% (
  echo In order to run this tool you need either Visual Studio 2015 or
  echo Microsoft Build Tools 2015 tools installed.
  echo.
  echo Visit this page to download either:
  echo.
  echo http://www.visualstudio.com/en-us/downloads/visual-studio-2015-downloads-vs
  echo.
  echo Attempting to fall back to MSBuild 12 for building only
  echo.
  IF NOT EXIST %MSBUILD12_TOOLS_PATH% (
    echo Could not find MSBuild 12.  Please install build tools ^(See above^)
    exit /b 1
  ) else (
    set BUILD_TOOLS_PATH=%MSBUILD12_TOOLS_PATH%
  )
)

echo Downloading latest version of NuGet.exe...
IF NOT EXIST %LocalAppData%\NuGet md %LocalAppData%\NuGet
@powershell -NoProfile -ExecutionPolicy unrestricted -Command "$ProgressPreference = 'SilentlyContinue'; Invoke-WebRequest 'https://www.nuget.org/nuget.exe' -OutFile '%NUGET%'"

echo Downloading latest version of FAKE...
IF NOT EXIST %LocalAppData%\FAKE %NUGET% "install" "FAKE" "-OutputDirectory" "%LocalAppData%" "-ExcludeVersion" "-Version" "4.4.4"

echo Downloading latest version of NuGet.Core...
IF NOT EXIST %LocalAppData%\NuGet.Core %NUGET% "install" "NuGet.Core" "-OutputDirectory" "%LocalAppData%" "-ExcludeVersion" "-Version" "2.8.6"

echo Downloading latest version of GitVersion.CommandLine...
IF NOT EXIST %LocalAppData%\GitVersion.CommandLine %NUGET% "install" "GitVersion.CommandLine" "-OutputDirectory" "%LocalAppData%" "-ExcludeVersion" "-Version" "3.3.0"

echo Downloading latest version of Nyx...
%NUGET% "install" "Nyx" "-OutputDirectory" "%LocalAppData%" "-ExcludeVersion" "-PreRelease"

SET TARGET="Build"

IF NOT [%1]==[] (set TARGET="%1")

SET SUMMARY="{SUMMARY}"
SET DESCRIPTION="{DESCRIPTION}"

%FAKE% %NYX% "target=%TARGET%" appName={appName} appSummary=%SUMMARY% appDescription=%DESCRIPTION% nugetPackageName={nugetPackageName}
```

> Replace {SUMMARY}, {DESCRIPTION}, {appName}, {nugetPackageName} with specific information

Example:
```
SET SUMMARY="Hystrix.NET.Contrib.ServoPublisher"
SET DESCRIPTION="Hystrix.NET.Contrib.ServoPublisher"

%FAKE% %NYX% "target=%TARGET%" appName=Hystrix.NET.Contrib.ServoPublisher appSummary=%SUMMARY% appDescription=%DESCRIPTION% nugetPackageName=Hystrix.NET.Contrib.ServoPublisher
```

### release-local.cmd file ###
```
@echo off

build.cmd ReleaseLocal
```

### release.cmd file ###
This file is copy of build.cmd file with following differences
`SET TARGET="Release"` instead of `SET TARGET="Build"`
and
`%FAKE% %NYX% "target=%TARGET%" appName={appName} appSummary=%SUMMARY% appDescription=%DESCRIPTION% nugetPackageName={nugetPackageName} nugetKey={YOUR-NUGET-KEY}` nugetKey is present

### Nuget-spec-file file ###
```
<?xml version="1.0" encoding="utf-8"?>
<package xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <metadata xmlns="http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd">
    <description>@description@</description>
    <id>@project@</id>
    <version>@build.number@</version>
    <authors>@authors@</authors>
    <language>en-US</language>
    <summary>@summary@</summary>
    <licenseUrl>{LICENSE-URK}</licenseUrl>
    <projectUrl>{PROJECT-URL}</projectUrl>
    <releaseNotes>@releaseNotes@</releaseNotes>
    <tags>{TAGS}</tags>
    @dependencies@
  </metadata>
</package>
```

### Nuget-spec-file example file ###
```
<?xml version="1.0" encoding="utf-8"?>
<package xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <metadata xmlns="http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd">
    <description>@description@</description>
    <id>@project@</id>
    <version>@build.number@</version>
    <authors>@authors@</authors>
    <language>en-US</language>
    <summary>@summary@</summary>
    <licenseUrl>https://github.com/Elders/Hystrix.NET.Contrib.ServoPublisher/blob/master/LICENSE</licenseUrl>
    <projectUrl>https://github.com/Elders/Hystrix.NET.Contrib.ServoPublisher</projectUrl>
    <releaseNotes>@releaseNotes@</releaseNotes>
    <tags>hystrix</tags>
    @dependencies@
  </metadata>
</package>
```

### RELEASE_NOTES.md ###
```
#### {VERSION} - {DATE}
* {MESSAGE}
```

### Example for beta version RELEASE_NOTES.md ###
```
#### 1.0.0-beta0001 - 08.06.2016
* Moved from Hystrix.NET
```

### Example for production version RELEASE_NOTES.md ###
```
#### 0.1.0 - 08.06.2016
* Moved from Hystrix.NET
```

## Github repository structure ##
Executing release.cmd in branch release-X.Y.Z will produce nuget package with beta version (pre release)
Executing release.cmd in branch master will produce nuget package with production ready version

## Example Workflow ##
1. Create proper project structure
2. Create github repository
3. Create release-1.0.0 branch
4. Add release note in RELEASE_NOTES.md with 1.0.0-beta0001 (this is the initial version)
5. Commit changes to release-1.0.0 branch. Usually commit message is same as release note
6. Execute release-local.cmd to make sure everything is allright
7. Execute release.cmd