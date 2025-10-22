using System;
using Godot;
using System.Linq;

public partial class WorkbenchWindow : Control
{
    [ExportGroup("Buttons")]

    [Export] private Button craftButton;
    [Export] private Button closeButton;

    [ExportGroup("References")]
    [Export] private Control ContentContainer;
    [Export] private Resource IngredientRow;
    [Export] private ItemData CookedChickenData;

    private ItemData[] _ingredients;

    /// <summary>
    /// Initializes the workbench window and connects button events.
    /// </summary>
    public override void _Ready()
    {
        craftButton.Pressed += OnCraft_Pressed;
        closeButton.Pressed += OnClose_Pressed;
    }

    /// <summary>
    /// Sets up the workbench with the required ingredients and creates UI rows.
    /// </summary>
    public void Configure(ItemData[] ingredients)
    {
        _ingredients = ingredients;

        var template = ResourceLoader.Load<PackedScene>(IngredientRow.ResourcePath);

        if (template == null)
        {
            GD.PrintErr($"Failed to load scene at path: {IngredientRow.ResourcePath}");
            return;
        }
        
        foreach (var data in ingredients)
        {
            // Instantiate the ingredient row from the template and add it to the scene
            var ingredient = template.Instantiate();
            ContentContainer.AddChild(ingredient);

            //Set the data
            (ingredient as IngredientRow)?.Configure(data);

        }
    }

    /// <summary>
    /// Handles the craft button press to create an item if ingredients are available.
    /// </summary>
    private void OnCraft_Pressed()
    {
        //Check if player has required 
        var hasItems = _ingredients.All(ingredient => Player.Instance.Inventory.AllItems.Any(item => item.Data == ingredient));

        if (!hasItems)
        {
            GD.Print("Player does not have all the ingredients for this item.");
            return;
        }

        //Remove them from the inventory
        foreach (var ingredient in _ingredients)
        {
            Player.Instance.Inventory.RemoveItem(ingredient, 1);
        }

        //Create an item referenced. In a real scenario this would be tied to a 'recipe' that the user would create.
        Item item = new Item(CookedChickenData, 1);

        //Add new item into inventory
        Player.Instance.Inventory.AddItem(item);
    }

    /// <summary>
    /// Handles the close button press to hide the workbench window.
    /// </summary>
    private void OnClose_Pressed()
    {
        // Logic to handle closing the workbench UI
        Hide();
    }

}