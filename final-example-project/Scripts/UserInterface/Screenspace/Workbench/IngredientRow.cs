using Godot;

public partial class IngredientRow : Control
{
    [Export] private Label _name;
    [Export] private TextureRect _icon;

    /// <summary>
    /// Sets up the ingredient row with the provided item data.
    /// </summary>
    public void Configure(ItemData itemData)
    {
        _name.Text = itemData.Name;
        _icon.Texture = itemData.Icon;
    }
}