using Godot;
using System;

public partial class Player : CharacterBody3D
{

    public static Player Instance { get; private set; }

    public Inventory Inventory { get; private set; } = new Inventory();

    [ExportGroup("Movement")]
    [Export] private float _baseMovementSpeed = 5f;
    [Export] private float _rotationSpeed = 6f;
    [Export] private float _jumpVelocity = 5f;

    [ExportGroup("References")]
    [Export] private CameraController _cameraController;
    [Export] private Node3D _playerModel;
    [Export] private PlayerAnimation _animationController;

    [ExportGroup("Debug")]
    private Vector2 _inputDirection = Vector2.Zero;

    /// <summary>
    /// Called when the node enters the scene tree for the first time.
    /// Used here to implement the Singleton pattern - ensures only one Player instance exists.
    /// </summary>
    public override void _EnterTree()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            GD.PrintErr("Multiple Player instances of Player detected! Destroying object.");
            QueueFree(); // Remove this instance if another already exists
            return;
        }
    }


    /// <summary>
    /// Called every physics frame (e.g. 60 times per second).
    /// Used for physics-related updates like movement, rotation, and collision handling.
    /// </summary>
    public override void _PhysicsProcess(double delta)
    {
        HandleInput();
        HandleRotation();
        HandleMovement();
        HandleJump((float)delta);
        MoveAndSlide();
    }

    /// <summary>
    /// Captures player input for movement using the defined input actions
    /// </summary>
    private void HandleInput() => _inputDirection = Input.GetVector("move_left", "move_right", "move_forward", "move_back");

    /// <summary>
    /// Handles unhandled key input events
    /// Used for inventory toggle and grab animation triggers
    /// </summary>
    public override void _UnhandledKeyInput(InputEvent @event)
    {
        // Check if the event is a key press (not a release)
        if (@event is InputEventKey keyEvent && keyEvent.Pressed && !keyEvent.Echo)
        {
            // Handle inventory toggle
            if (@event.IsAction("inventory"))
            {
                if (InventoryWindow.Instance.Visible)
                {
                    InventoryWindow.Instance.HideInventory();
                }
                else
                {
                    InventoryWindow.Instance.ShowInventory();
                }
            }

            // Handle grab animation when E key is pressed
            else if (@event.IsAction("interact"))
            {
                _animationController.PlayPickup();
            }
            else if (@event.IsAction("Wave"))
            {
                _animationController.PlayWave();
            }
        }
    }



    /// <summary>
    /// Calculates movement speed based on direction - forward movement is full speed, backward is reduced
    /// </summary>
    /// <param name="direction">The movement direction value (positive = forward, negative = backward)</param>
    /// <returns>The calculated movement speed</returns>
    private float CalculateMovementSpeed(float direction) => direction > 0 ? _baseMovementSpeed : _baseMovementSpeed * 0.25f;

    /// <summary>
    /// Detects jump request from the player and handles moving upwards
    /// </summary>
    private void HandleJump(float delta)
    {
        // Check if the player is on the floor and attempting to jump
        if (IsOnFloor() && Input.IsActionPressed("jump"))
        {

            // Set upward velocity to launch the player into the air
            Velocity = Velocity.WithNewY(_jumpVelocity);

            //Trigger the animation
            _animationController.SetJumpState();
        }
        else if (!IsOnFloor())
        {
            // Apply gravity to pull the player back toward the ground by taking the current Y 
            // velocity and adding gravity force (negative) multiplied by delta time
            // This creates realistic falling acceleration that gets faster over time
            Velocity = Velocity.WithNewY(Velocity.Y + GetGravity().Y * delta);
        }

    }

    /// <summary>
    /// Handles player model rotation to align with camera direction
    /// Uses smooth interpolation to avoid jarring rotation changes
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
            _playerModel.Rotation = _playerModel.Rotation.WithNewY(smoothAngle);
        }
    }

    /// <summary>
    /// Handles player movement based on input direction and camera orientation
    /// Uses camera-relative movement so forward always moves where the camera is looking
    /// </summary>
    private void HandleMovement()
    {
        // If no input is detected, stop horizontal movement but keep vertical velocity (for gravity/jumping)
        if (_inputDirection == Vector2.Zero)
        {
            Velocity = Vector3.Zero.WithNewY(Velocity.Y);
            
            // Animation logic: stop walk animation if not moving
            _animationController.SetIdleState();
            
            return;
        }

        if (_cameraController == null || _playerModel == null)
        {
            GD.PrintErr("CameraController and/or PlayerModel is not set!");
            return;
        }

        // Get camera directions for movement calculation
        // Negative Z because Godot's camera looks down negative Z axis by default
        Vector3 cameraForward = _cameraController.GlobalBasis.Z;

        // Negative X to match typical right-hand coordinate system expectations
        Vector3 cameraRight = _cameraController.GlobalBasis.X;

        // Calculate movement speed based on input direction (forward vs backward)
        float moveSpeed = CalculateMovementSpeed(-_inputDirection.Y);

        // Create movement direction by combining forward/backward and left/right inputs
        // Normalized ensures diagonal movement isn't faster than cardinal movement
        Vector3 moveDirection = (cameraForward * _inputDirection.Y + cameraRight * _inputDirection.X).Normalized();

        // Apply movement to velocity while preserving Y component for gravity/jumping
        Velocity = new Vector3(moveDirection.X * moveSpeed, Velocity.Y, moveDirection.Z * moveSpeed);

        // Animation logic: play walk animation when moving
        _animationController.SetMovingState();
    }
}
