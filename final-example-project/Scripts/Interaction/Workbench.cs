using System.ComponentModel;
using Godot;

public partial class Workbench : Node3D, IInteractable
{
    [Export] private WorkbenchWindow _window;

    [Export] private ItemData[] _recipe;

    /// <summary>
    /// Initializes the workbench with recipe data and hides the UI.
    /// </summary>
    public override void _Ready()
    {
        //Populate the recipe data for test purposes
        _window.Configure(_recipe);

        //Make sure the UI is not visible unless the player wants it to be
        _window.Hide();
    }

    /// <summary>
    /// Hides the workbench UI when player stops interacting.
    /// </summary>
    public void Disengage() => _window.Hide();

    /// <summary>
    /// Toggles the workbench UI visibility.
    /// </summary>
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