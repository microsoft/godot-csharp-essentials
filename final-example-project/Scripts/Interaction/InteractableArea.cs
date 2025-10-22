using Godot;

public partial class InteractableArea : Area3D
{
    public bool IsPlayerInArea { get; private set; }

    private IInteractable _interactable;

    /// <summary>
    /// Initializes the interactable reference.
    /// </summary>
    public override void _Ready() => _interactable = GetParent() as IInteractable;

    /// <summary>
    /// Connects body enter/exit event signals.
    /// </summary>
    public override void _EnterTree()
    {
        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExited;
    }

    /// <summary>
    /// Disconnects body enter/exit event signals.
    /// </summary>
    public override void _ExitTree()
    {
        BodyEntered -= OnBodyEntered;
        BodyExited -= OnBodyExited;

    }

    /// <summary>
    /// Handles interaction input when player is in area.
    /// </summary>
    public override void _UnhandledKeyInput(InputEvent @event)
    {
        if (Input.IsActionJustPressed("interact") && IsPlayerInArea)
        {
            _interactable?.Interact();
        }
    }

    /// <summary>
    /// Handles player exiting the interaction area.
    /// </summary>
    private void OnBodyExited(Node3D body)
    {
        if (body is Player player)
        {
            IsPlayerInArea = false;
            _interactable?.Disengage();
        }
    }

    /// <summary>
    /// Handles player entering the interaction area.
    /// </summary>
    private void OnBodyEntered(Node3D body)
    {
        if (body is Player player)
        {
            IsPlayerInArea = true;
        }
    }
}