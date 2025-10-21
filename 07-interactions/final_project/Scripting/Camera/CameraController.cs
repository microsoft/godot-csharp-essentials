using Godot;

/// <summary>
/// Third-person camera controller that rotates around the player with mouse input.
/// Handles right-click and drag camera rotation with smooth return to center.
/// Designed to work with Player character for third-person gameplay.
/// </summary>
public partial class CameraController : Node3D
{
    #region Export Settings
    /// <summary>
    /// Mouse sensitivity for camera rotation. Higher values = faster rotation.
    /// </summary>
    [Export] private float _sensitivity = 0.2f;
    
    #endregion

    #region Godot Lifecycle
    /// <summary>
    /// Handles unprocessed input events, specifically mouse button and motion events.
    /// Processes right-click to activate camera rotation and mouse movement for rotation.
    /// </summary>
    /// <param name="event">The input event to process</param>
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
    #endregion
}
