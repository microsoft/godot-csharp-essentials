using Godot;
using System;

public partial class DemoInstructions : Node3D
{
    public override void _Ready()
    {
        GD.PrintRich(
            "[b]Welcome to FlyByFood![/b]\n" +
            "\n" +
            "This demo highlights some advanced techniques beyond the basics covered in the tutorial series.\n" +
            "\n" +
            "You can:\n" +
            "\n" +
            "• Press [color=yellow]Space[/color] to jump.\n" +
            "• Press [color=yellow]WASD[/color] to move.\n" +
            "• Press [color=yellow]I[/color] to open your Inventory.\n" +
            "• Press [color=yellow]E[/color] to Interact with objects.\n" +
            "• Visit the Workbench to view the required ingredients.\n" +
            "• Collect ingredients from the stations on the sides.\n" +
            "\n" +
            "Get cooking and have fun!");
    }
}
