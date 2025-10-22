using Godot;

public partial class InventorySlot : Control
{
    [Export] private Label _amount;
    [Export] private TextureRect _icon;

    /// <summary>
    /// Sets up the inventory slot with the provided icon and amount.
    /// </summary>
    public void Configure(Texture2D icon, int amount)
    {
        _amount.Text = amount == 1 ? "" : $"x{amount}";
        _icon.Texture = icon;
    }
}