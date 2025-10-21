using System.Numerics;
using Godot;
using Vector2 = Godot.Vector2;
using Vector3 = Godot.Vector3;

/// <summary>
/// Player controller for a 3D character with camera-relative movement and smooth rotation.
/// Handles input processing, movement with gravity, and character model rotation.
/// Implements singleton pattern for easy access from other scripts.
/// </summary>
public partial class Player : CharacterBody3D
{
    #region Singleton
    /// <summary>
    /// Singleton instance for easy access to the player from other scripts
    /// </summary>
    public static Player Instance { get; private set; }
    #endregion

    #region Movement Settings
    [ExportGroup("Movement")]
    [Export] private float _baseMovementSpeed = 5f;
    [Export] private float _rotationSpeed = 6f;

    #endregion

    #region References
    [ExportGroup("References")]
    [Export] private Node3D _playerModel;
    [Export] private CameraController _cameraController;

    #endregion

    #region Private Fields
    /// <summary>
    /// Current input direction from WASD keys, normalized between -1 and 1
    /// </summary>
    private Vector2 _inputDirection = Vector2.Zero;

    /// <summary>
    /// Gravity value retrieved from project settings to match other physics objects
    /// </summary>
    private float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    #endregion

    #region Godot Lifecycle
    /// <summary>
    /// Called when the node enters the scene tree for the first time.
    /// Initializes the singleton instance and confirms script is working.
    /// </summary>
    public override void _Ready() => Instance = this;

    /// <summary>
    /// Called every physics frame (usually 60 FPS).
    /// Handles input, rotation, movement, and applies physics in the correct order.
    /// </summary>
    /// <param name="delta">Time elapsed since the last physics frame</param>
    public override void _PhysicsProcess(double delta)
    {
        HandleInput();
        HandleRotation();
        HandleMovement();
        MoveAndSlide();
    }

    #endregion

    #region Input Handling
    /// <summary>
    /// Processes input from WASD keys and converts to normalized direction vector.
    /// Uses Godot's Input.GetVector for automatic deadzone handling and normalization.
    /// </summary>
    private void HandleInput()
    {
        _inputDirection = Input.GetVector("move_right", "move_left", "move_back", "move_forward");
    }
    #endregion


    #region Movement and Physics

    /// <summary>
    /// Handles player movement based on input direction and camera orientation
    /// Uses camera-relative movement so forward always moves where the camera is looking
    /// </summary>
    private void HandleMovement()
    {
        // If no input is detected, stop horizontal movement but keep vertical velocity (for gravity/jumping)
        if (_inputDirection == Vector2.Zero)
        {
            Vector3 newVelocity = new Vector3(0, Velocity.Y, 0);
            Velocity = newVelocity;

            //TODO: Add animation logic
            return;
        }

        if (_cameraController == null || _playerModel == null)
        {
            GD.PrintErr("CameraController and/or PlayerModel is not set!");
            return;
        }

        // Get camera directions for movement calculation
        // Negative Z because Godot's camera looks down negative Z axis by default
        Vector3 cameraForward = -_cameraController.GlobalBasis.Z;

        // Negative X to match typical right-hand coordinate system expectations
        Vector3 cameraRight = -_cameraController.GlobalBasis.X;

        // Calculate movement speed based on input direction (forward vs backward)
        float moveSpeed = CalculateMovementSpeed(-_inputDirection.Y);

        // Create movement direction by combining forward/backward and left/right inputs
        // Normalized ensures diagonal movement isn't faster than cardinal movement
        Vector3 moveDirection = (cameraForward * _inputDirection.Y + cameraRight * _inputDirection.X).Normalized();

        // Apply movement to velocity while preserving Y component for gravity/jumping
        Velocity = new Vector3(moveDirection.X * moveSpeed, Velocity.Y, moveDirection.Z * moveSpeed);

        //Animation stuff
        //Vector2 locomotionDirection = new Vector2(_inputDirection.X, -_inputDirection.Y);
        //TODO: call set locomotion
    }
    #endregion

    #region Rotation
    /// <summary>
    /// Handles smooth rotation of the player model to face movement direction.
    /// Creates smooth turning animation instead of instant snapping to new directions.
    /// Only rotates when there is input and a player model is assigned.
    /// </summary>
    private void HandleRotation()
    {
        if (_cameraController == null || _playerModel == null)
        {
            GD.PrintErr("CameraController and/or PlayerModel is not set!");
            return;
        }

        // Get the forward direction vectors for both camera and player model
        Vector3 cameraForward = _cameraController.GlobalBasis.Z;
        Vector3 playerForward = _playerModel.Basis.Z;

        // Calculate how aligned the camera and player are (1.0 = perfectly aligned)
        float alignment = cameraForward.Dot(playerForward);

        // Only rotate if not already closely aligned (avoid unnecessary calculations)
        if (alignment < 0.99f)
        {

            // Calculate the target angle based on camera's forward direction
            // Atan2 converts X and Z coordinates into a rotation angle (like finding which way to point)
            // Example: if camera looks right, it tells the player "turn to face right too!"
            float targetAngle = Mathf.Atan2(cameraForward.X, cameraForward.Z);

            // Smoothly interpolate from current rotation to target rotation
            // LerpAngle gradually blends between two angles to create smooth rotation - the third parameter (weight) controls how fast it turns
            // Example: if you're facing north and want to face east, it slowly turns you instead of snapping East instantly
            float smoothAngle = Mathf.LerpAngle(_playerModel.Rotation.Y, targetAngle, (float)(_rotationSpeed * GetProcessDeltaTime()));

            // Apply the smooth rotation to the player model
            // Try: Replace "smoothAngle" with targetAngle to see an example of "instant snapping"
            Vector3 newRotation = new Vector3(_playerModel.Rotation.X, smoothAngle, _playerModel.Rotation.Z);
            _playerModel.Rotation = newRotation;
        }
    }
    
        /// <summary>
    /// Calculates movement speed based on direction - forward movement is full speed, backward is reduced
    /// </summary>
    /// <param name="direction">The movement direction value (positive = forward, negative = backward)</param>
    /// <returns>The calculated movement speed</returns>
    private float CalculateMovementSpeed(float direction) => direction < 0 ? _baseMovementSpeed : _baseMovementSpeed * 0.25f;
    #endregion
}
