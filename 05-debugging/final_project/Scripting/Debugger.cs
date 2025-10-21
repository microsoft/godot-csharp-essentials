using Godot;
using System;

public partial class Debugger : Node3D
{
    private float _printMessage = 1;

    public override void _Process(double delta)
    {
        _printMessage++;
        GD.Print(_printMessage);

        if (_printMessage == 10)
        {
            PrintMessages();
        }
    }


    public void PrintMessages()
    {
        GD.Print("This is information");
        GD.PushWarning("This is a warning");
        GD.PushError("This is an error");
    }

}
