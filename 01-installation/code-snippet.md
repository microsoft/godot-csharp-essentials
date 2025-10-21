# Code Snippet - VS Code Launch & Task

The following code snippet is needed for the VS Code Launch and Tasks step within the video.

**In Visual Studio Code:**

Create `.vscode/launch.json`:
```json
{
    "version": "0.2.0",
    "configurations": [
        {
            "name": "Play",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "build",
            "program": "${env:GODOT4}",
            "args": [],
            "cwd": "${workspaceFolder}",
            "stopAtEntry": false
        }
    ]
}
```
**Important:** Replace the path in the `program` line with the full path to your Godot executable. Use double backslashes (`\\`) in the path for proper JSON formatting. For example: `"C:\\Games\\Godot\\Godot_v4.4-stable_mono_win64.exe"`

Create `.vscode/tasks.json`:
```json
{
    "version": "2.0.0",
    "tasks": [
        {
            "label": "build",
            "command": "dotnet",
            "type": "process",
            "args": [
                "build"
            ],
            "problemMatcher": "$msCompile"
        }
    ]
}
```
