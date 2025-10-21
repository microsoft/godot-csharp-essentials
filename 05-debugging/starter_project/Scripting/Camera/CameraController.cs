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
    
    /// <summary>
    /// Speed at which camera returns to center position when mouse is released.
    /// Measured in degrees per second.
    /// </summary>
    [Export] private float _returnSpeed = 3.0f;
    #endregion
    
    #region Private Fields
    /// <summary>
    /// Current Y-axis rotation of the camera in degrees.
    /// Positive values rotate clockwise, negative values rotate counter-clockwise.
    /// </summary>
    private float _currentRotation = 0.0f;
    
    /// <summary>
    /// Tracks whether the right mouse button is currently pressed.
    /// Used to determine when camera rotation should be active.
    /// </summary>
    private bool _isMousePressed = false;
    #endregion

    #region Godot Lifecycle
    /// <summary>
    /// Handles unprocessed input events, specifically mouse button and motion events.
    /// Processes right-click to activate camera rotation and mouse movement for rotation.
    /// </summary>
    /// <param name="event">The input event to process</param>
    public override void _UnhandledInput(InputEvent @event)
    {
        // Track right mouse button press/release for camera rotation activation
        if (@event is InputEventMouseButton mouseButton && mouseButton.ButtonIndex == MouseButton.Right)
        {
            _isMousePressed = mouseButton.Pressed;
        }

        // Handle mouse movement while right button is held down
        if (@event is InputEventMouseMotion mouseMotion && _isMousePressed)
        {
            // Convert mouse movement to rotation change
            float deltaX = mouseMotion.Relative.X * _sensitivity;
            _currentRotation -= deltaX; // Negative for natural camera movement
            
            // Apply rotation immediately for responsive camera control
            RotationDegrees = new Vector3(0, _currentRotation, 0);
        }
    }

    /// <summary>
    /// Called every frame to handle camera return-to-center behavior.
    /// Smoothly rotates camera back to default position when not actively controlled.
    /// </summary>
    /// <param name="delta">Time elapsed since last frame for smooth interpolation</param>
    public override void _Process(double delta)
    {
        // Smoothly return camera to center when mouse is released
        if (!_isMousePressed && _currentRotation != 0.0f)
        {
            // Move rotation toward 0 at specified return speed
            _currentRotation = Mathf.MoveToward(_currentRotation, 0.0f, (float)(_returnSpeed * 90.0 * delta));
            RotationDegrees = new Vector3(0, _currentRotation, 0);
        }
    }
    #endregion
}
