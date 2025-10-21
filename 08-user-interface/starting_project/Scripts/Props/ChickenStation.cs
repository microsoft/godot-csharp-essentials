using Godot;

public partial class ChickenStation : Area3D
{
    [Signal]
    public delegate void ChickenCollectedEventHandler(int amountCollected);

    [Export] private int _totalChicken = 5;
    private int _remainingChicken;

    private bool _playerInArea = false;

    public override void _Ready()
    {
        _remainingChicken = _totalChicken;

        // Connect to the Area3D's built-in detection signals
        BodyEntered += OnPlayerEntered;
        BodyExited += OnPlayerExited;
    }
    
    private void Interact()
    {
        if (_remainingChicken > 0)
        {
            _remainingChicken--;
            
            // Emit your custom signal
            EmitSignal(SignalName.ChickenCollected, 1);
            
            GD.Print($"Collected raw chicken! {_remainingChicken} remaining.");
        }
    }
    
    public override void _UnhandledInput(InputEvent @event)
    {
        // Check for interaction input while player is in area
        if (_playerInArea && Input.IsActionJustPressed("interact"))
        {
            Interact();
        }
    }
    
    private void OnPlayerEntered(Node3D body)
    {
        // The collision mask ensures only the player can trigger this
        GD.Print("Player has entered the area.");
        _playerInArea = true;
    }
    
    private void OnPlayerExited(Node3D body)
    {
        GD.Print("Player has exited the area.");
        _playerInArea = false;
    }

}