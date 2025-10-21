using Godot;

public partial class InteractableArea : Area3D
{
    public bool IsPlayerInArea { get; private set; }

    private IInteractable _interactable;

    /// <summary>
    /// Called when the node is ready. Initializes the interactable reference.
    /// </summary>
    public override void _Ready() => _interactable = GetParent() as IInteractable;

    /// <summary>
    /// Called when the node enters the scene tree. Connects signals for body entered and exited events.
    /// </summary>
    public override void _EnterTree()
    {
        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExited;
    }

    /// <summary>
    /// Called when the node exits the scene tree. Disconnects signals for body entered and exited events.
    /// </summary>
    public override void _ExitTree()
    {
        BodyEntered -= OnBodyEntered;
        BodyExited -= OnBodyExited;

    }

    /// <summary>
    /// Handles unhandled key input. Triggers interaction if the "interact" action is pressed and the player is in the area.
    /// </summary>
    /// <param name="@event">The input event to process.</param>
    public override void _UnhandledKeyInput(InputEvent @event)
    {
        if (Input.IsActionJustPressed("interact") && IsPlayerInArea)
        {
            _interactable?.Interact();
        }
    }

    /// <summary>
    /// Called when a body exits the area. Updates the player presence flag.
    /// </summary>
    /// <param name="body">The body that exited the area.</param>
    private void OnBodyExited(Node3D body)
    {
        if (body is Player player)
        {
            IsPlayerInArea = false;
            _interactable?.Disengage();
        }
    }

    /// <summary>
    /// Called when a body enters the area. Updates the player presence flag.
    /// </summary>
    /// <param name="body">The body that entered the area.</param>
    private void OnBodyEntered(Node3D body)
    {
        if (body is Player player)
        {
            IsPlayerInArea = true;
        }
    }
}