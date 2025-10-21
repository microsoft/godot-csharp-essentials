using Godot;
using System;

public partial class WorkbenchUserInterface : Control
{

    [ExportGroup("Buttons")]
    [Export] private Button craftButton;
    [Export] private Button closeButton;

    [ExportGroup("Other References")]
    [Export] private Workbench _workbench;
    [Export] private Control ContentContainer;

    public override void _Ready()
    {
        Hide();
        
        //Setup the ingredients
        SetupIngredients();

        //Register callback
        _workbench.WorkbenchInteraction += OnWorkbenchInteracted;

        craftButton.Pressed += OnCraft_Pressed;
        closeButton.Pressed += OnClose_Pressed;

    }

    private void SetupIngredients()
    {
        var template = GD.Load<PackedScene>("res://UserInterface/ingredient.tscn");
        string[] ingredientNames = { "Milk", "Chicken" };

        foreach (string ingredientName in ingredientNames)
        {
            var ingredient = template.Instantiate<Ingredient>();
            ContentContainer.AddChild(ingredient);
            ingredient.Setup(ingredientName);
        }
    }

    private void OnWorkbenchInteracted() => Show();

    private void OnClose_Pressed() => Hide();

    private void OnCraft_Pressed() => GD.Print("Craft Button Clicked");
}
