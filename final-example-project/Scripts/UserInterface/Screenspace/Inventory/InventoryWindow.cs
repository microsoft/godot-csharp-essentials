using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class InventoryWindow : Control
{
    private List<Control> _slotData = new List<Control>();

    [ExportGroup("References")]
    [Export] private Control ContentContainer;
    [Export] private Resource InventoryRow;

    public static InventoryWindow Instance { get; private set; }

    public override void _EnterTree()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            GD.PrintErr("Multiple InventoryUserInterface instances detected! Destroying object.");
            QueueFree();
            return;
        }
    }

    public override void _Ready()
    {
        Hide();

        var template = ResourceLoader.Load<PackedScene>(InventoryRow.ResourcePath);
        var containers = ContentContainer.GetChildren().ToList();

        foreach (var slot in containers)
        {
            var instance = template.Instantiate();
            slot.AddChild(instance);
            _slotData.Add((Control)instance);
        }

    }

    public void ShowInventory()
    {
        // Refresh inventory data before showing
        Refresh();
        Show();
    }

    public void HideInventory() => Hide();

    private void Refresh()
    {
        // Refresh logic for inventory slots
        for (int i = 0; i < _slotData.Count; i++)
        {
            if (i < Player.Instance.Inventory.AllItems.Count)
            {
                var itemData = Player.Instance.Inventory.AllItems[i];
                (_slotData[i] as InventorySlot)?.Configure(itemData.Data.Icon, itemData.Amount);
                _slotData[i].Show();
            }
            else
            {
                _slotData[i].Hide();
            }
        }
    }
}