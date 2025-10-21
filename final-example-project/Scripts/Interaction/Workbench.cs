using System.ComponentModel;
using Godot;

public partial class Workbench : Node3D, IInteractable
{
    [Export] private WorkbenchWindow _window;

    [Export] private ItemData[] _recipe;

    public override void _Ready()
    {
        //Populate the recipe data for test purposes
        _window.Configure(_recipe);

        //Make sure the UI is not visible unless the player wants it to be
        _window.Hide();
    }

    public void Disengage() => _window.Hide();

    public void Interact()
    {
        if (_window.Visible)
        {
            _window.Hide();
        }
        else
        {
            _window.Show();
        }
    }
}