using Godot;
using System;

public partial class Workbench : Area3D
{
    [Signal]
    public delegate void WorkbenchInteractionEventHandler();
    private bool _playerInArea = false;

    public override void _Ready()
    {
        BodyEntered += OnPlayerEntered;
        BodyExited += OnPlayerExited;
    }

    private void Interact() => EmitSignal(SignalName.WorkbenchInteraction);
    
    public override void _UnhandledInput(InputEvent @event)
    {
        // Check for interaction input while player is in area
        if (_playerInArea && Input.IsActionJustPressed("interact"))
        {
            Interact();
        }
    }
    
    private void OnPlayerEntered(Node3D body) => _playerInArea = true;
    
    private void OnPlayerExited(Node3D body) => _playerInArea = false;
}
