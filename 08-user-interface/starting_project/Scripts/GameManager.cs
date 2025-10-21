using Godot;
using System;

public partial class GameManager : Node
{
    [Export] private ChickenStation _chickenStation;

    public override void _Ready() => _chickenStation.ChickenCollected += OnChickenCollected;
    
    private void OnChickenCollected(int amount) => GD.Print($"Game Manager: Player collected {amount} chicken!");
}
