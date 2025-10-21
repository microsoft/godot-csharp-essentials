using Godot;
using System;

public partial class CameraController : Node3D
{
    [Export] private float _sensitivity = 0.2f;

    public override void _UnhandledInput(InputEvent @event)
    {
        if (GetViewport().GuiGetFocusOwner() != null)
        {
            return;
        }

        if (@event is InputEventMouseMotion mouseMotion && Input.IsMouseButtonPressed(MouseButton.Right))
        {
            float deltaX = mouseMotion.Relative.X * _sensitivity;

            RotateY(Mathf.DegToRad(-deltaX));
        }
    }


}
