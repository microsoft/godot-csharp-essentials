using System;
using Godot;

public partial class Collectable : Node3D, IInteractable
{
    [Export] private ItemData _itemData;
    [Export] private int _total = 10;
    [Export] private int _remainingAmount;

    [ExportCategory("Debug")]
    [Export] private Area3D _area3D;

    private bool _isPlayerInArea = false;

    public override void _Ready()
    {
        if (_itemData == null)
        {
            GD.PrintErr($"ItemData is not set on {Name}!");
            return;
        }

        // Initialize remaining amount to total when the collectable is ready
        if (_total == 0)
        {
            GD.Print($"{_itemData.Name} collectable has no total set, setting to 5.");
            _total = 5;
        }

        _remainingAmount = _total;
    }

    public void Disengage() { }

    public void Interact()
    {
        if (_remainingAmount > 0)
        {
            _remainingAmount = Math.Max(0, _remainingAmount - 1);

            Player.Instance.Inventory.AddItem(new Item(_itemData, 1));
        }
        else
        {
            GD.Print($"{_itemData.Name} collectable is empty.");
        }
    }

}