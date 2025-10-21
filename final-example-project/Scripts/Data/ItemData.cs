using Godot;

/// <summary>
/// Base class for item data resources
/// </summary>
[GlobalClass]
public partial class ItemData : Resource
{
    /// <summary>
    /// The name of the item
    /// </summary>
    [Export] public string Name { get; set; } = "";

    /// <summary>
    /// Icon texture for the item
    /// </summary>
    [Export] public Texture2D Icon { get; set; }

}