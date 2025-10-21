using Godot;
using System;

/// <summary>
/// Controls player animations through Godot's AnimationTree system.
/// This class manages transitions between idle, walking, jump, and pickup animations,
/// providing a simplified interface for the player controller to trigger animation states.
/// The AnimationTree uses a StateMachine for locomotion and OneShot nodes for jump and pickup.
/// </summary>
public partial class PlayerAnimation : AnimationTree
{
    // Locomotion StateMachine condition parameters (for Idle/Walk transitions)
    private const string PARAM_IDLE     = "parameters/Locomotion/conditions/idle";
    private const string PARAM_WALKING  = "parameters/Locomotion/conditions/walking";

    // StateMachine playback controller
    private const string PARAM_PLAYBACK = "parameters/Locomotion/playback";

    // OneShot parameters for triggering jump and pickup animations
    private const string PARAM_JUMP_ONESHOT   = "parameters/JumpOS/request";
    private const string PARAM_PICKUP_ONESHOT = "parameters/PickupOS/request";
    private const string PARAM_WAVE_ONESHOT = "parameters/WaveOS/request";

    private AnimationNodeStateMachinePlayback _smPlayback;

    /// <summary>
    /// Called when the node enters the scene tree.
    /// Gets the AnimationNodeStateMachinePlayback object from the AnimationTree
    /// and calculates the base jump animation length for time scaling.
    /// </summary>
    public override void _Ready()
    {
        // Get the StateMachine playback controller for locomotion
        // This allows us to programmatically control which animation state to transition to
        _smPlayback = (AnimationNodeStateMachinePlayback)Get(PARAM_PLAYBACK);

        // Start in idle state
        SetIdleState();
    }

    // === Public API for Player controller ===

    /// <summary>
    /// Sets the player to idle animation state.
    /// Turns on idle condition and turns off walking condition in the StateMachine.
    /// </summary>
    public void SetIdleState()
    {
        Set(PARAM_IDLE,    true);
        Set(PARAM_WALKING, false);
    }

    /// <summary>
    /// Sets the player to walking animation state.
    /// Turns off idle condition and turns on walking condition in the StateMachine.
    /// </summary>
    public void SetMovingState()
    {
        Set(PARAM_IDLE,    false);
        Set(PARAM_WALKING, true);
    }

    /// <summary>
    /// Triggers the jump animation using a OneShot node.
    /// </summary>
    public void SetJumpState() => Set(PARAM_JUMP_ONESHOT, (int)AnimationNodeOneShot.OneShotRequest.Fire);


    /// <summary>
    /// Triggers the pickup animation using a OneShot node.
    /// </summary>
    public void PlayPickup() => Set(PARAM_PICKUP_ONESHOT, (int)AnimationNodeOneShot.OneShotRequest.Fire);
    
    /// <summary>
    /// Triggers the wave animation using a OneShot node.
    /// </summary>
    public void PlayWave() => Set(PARAM_WAVE_ONESHOT, (int)AnimationNodeOneShot.OneShotRequest.Fire);

}
