using Godot;

public partial class InteractUI : SubViewport
{
    [Export] private Label _label;

    public override void _Ready()
    {
        Size = (Vector2I)_label.Size;
    }


}
