using Godot;
using System;
using System.Diagnostics;

/// <summary>
/// Handles the animation logic for when the player moves into the doors Area3D node range
/// </summary>
public partial class Door : Area3D
{
    [ExportGroup("References")]
    [Export] private AnimationPlayer _animPlayer;

    private const string OPEN_ANIMATION_NAME = "Open";
    private const string CLOSE_ANIMATION_NAME = "Close";

    /// <summary>
    /// Initializes the door and connects body enter/exit events.
    /// </summary>
    public override void _Ready()
    {
        if (_animPlayer == null)
        {
            GD.PrintErr("No Animation Player detected on Door");
        }

        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExited;
    }

    /// <summary>
    /// Opens the door when a body enters the area.
    /// </summary>
    private void OnBodyEntered(Node3D body) => Open();

    /// <summary>
    /// Closes the door when a body exits the area.
    /// </summary>
    private void OnBodyExited(Node3D body) => Close();

    /// <summary>
    /// Plays the door opening animation.
    /// </summary>
    private void Open()
    {
        if (_animPlayer == null)
        {
            return;
        }

        //Try changing this value and seeing what happens when you open the door!
        _animPlayer.SpeedScale = 1; 

        _animPlayer.Play(OPEN_ANIMATION_NAME);
    }

    /// <summary>
    /// Plays the door closing animation.
    /// </summary>
    private void Close()
    {
        if (_animPlayer == null)
        {
            return;
        }

        _animPlayer.Play(CLOSE_ANIMATION_NAME);
    }
}
