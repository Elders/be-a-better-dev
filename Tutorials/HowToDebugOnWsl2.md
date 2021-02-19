# How to Debug in WSL 2 with VS and VS Code

## Debug WSL 2 with Visual Studio
1. Install WSL 2.0 
2. Install a distribution on it
3. Download the version of .NET Core you would like to run on it. => https://docs.microsoft.com/en-us/dotnet/core/install/linux
4. Download Visual Studio Extension for debugging WSL 2 => https://marketplace.visualstudio.com/items?itemName=ms-azuretools.Dot-Net-Core-Debugging-With-Wsl2
5. Create a profile for the WSL in Visual Studio

- Here is an example

```json
{
    "profiles": {
        "WSL 2": {
            "commandName": "WSL2",
            "launchBrowser": true,
            "launchUrl": "https://localhost:5001",
            "environmentVariables": {
                "ASPNETCORE_URLS": "https://localhost:5001;http://localhost:5000",
                "ASPNETCORE_ENVIRONMENT": "Development"
            },
            "distributionName": ""
        }
    }
}
```
6. Note that the ASPNETCORE_ENVIRONMENT is set to Development, but it is not working when running it on WSL 2. Currently is loading appsettings, and the last one loaded will be the one used.

For example we have:
- appsettings.json
- appsetings.Development.json
- appsettings.Int.json
- appsettings.Production.json

The appsettings.production.json will be used because it is the last loaded

---

## Debug .NET Core application in WSL 2 using VS Code
1. Download VS Code
2. Install Remote Development extension 
3. Install c# extension as well
4. Click F1 and then write `Remote-WSL: New Window` and VSCode will open under your linux distribution directory
5. In ur linux terminal give access to the folder with the project to the user who will build it. My project is in `/home/stambeto/work` and the user i am building it with is user: `stambeto`

`sudo chown -R stambeto /home/stambeto/work` would be the command

6. Now when you try to f5 ur project in VS Code probably launch.json and tasks.json will be created, if they are not then we have to create them manually. If they created automatically go to step 8, and if continue with the steps.

7. Copy `launch.json` into ur project folder, next to .vs folder create .vscode and inside put this json file

```json
{
    // Use IntelliSense to learn about possible attributes.
    // Hover to view descriptions of existing attributes.
    // For more information, visit: https://go.microsoft.com/fwlink/?linkid=830387
    "version": "0.2.0",
    "configurations": [
        {
            "name": ".NET Core Launch (console)",
            "type": "coreclr",
            "request": "launch",
            "WARNING01": "*********************************************************************************",
            "WARNING02": "The C# extension was unable to automatically decode projects in the current",
            "WARNING03": "workspace to create a runnable launch.json file. A template launch.json file has",
            "WARNING04": "been created as a placeholder.",
            "WARNING05": "",
            "WARNING06": "If OmniSharp is currently unable to load your project, you can attempt to resolve",
            "WARNING07": "this by restoring any missing project dependencies (example: run 'dotnet restore')",
            "WARNING08": "and by fixing any reported errors from building the projects in your workspace.",
            "WARNING09": "If this allows OmniSharp to now load your project then --",
            "WARNING10": "  * Delete this file",
            "WARNING11": "  * Open the Visual Studio Code command palette (View->Command Palette)",
            "WARNING12": "  * run the command: '.NET: Generate Assets for Build and Debug'.",
            "WARNING13": "",
            "WARNING14": "If your project requires a more complex launch configuration, you may wish to delete",
            "WARNING15": "this configuration and pick a different template using the 'Add Configuration...'",
            "WARNING16": "button at the bottom of this file.",
            "WARNING17": "*********************************************************************************",
            "preLaunchTask": "build",
            "program": "${workspaceFolder}/My.Service.Location/bin/Debug/netcoreapp3.1/UniCom.Dmo.Service.dll",
            "args": [],
            "cwd": "${workspaceFolder}/My.Service.Location/bin/Debug/netcoreapp3.1",
            "console": "internalConsole",
            "stopAtEntry": false
        },
        {
            "name": ".NET Core Attach",
            "type": "coreclr",
            "request": "attach",
            "processId": "${command:pickProcess}"
        }
    ]
}
```

8. Create `tasks.json`

```json
{
    "version": "2.0.0",
    "tasks": [
        {
            "label": "build",
            "command": "dotnet",
            "type": "process",
            "args": [ "build" ],
            "options": {
                "cwd": "${workspaceRoot}"
            },
            "problemMatcher": "$tsc"
        }
    ]
}
```

9. Now edit the `program` and `cwd` in this json and put the path to the service/api.
10. Now you can put breakpoint wherever you need (F9) and start debugging your application(F5)

