using Godot;
using System;
using System.Collections.Generic;


public partial class Inventory : Node3D
{
    // Collection of all items in the inventory

    public List<Item> AllItems { get; private set; } = new List<Item>();

    /// <summary>
    /// Adds an item to the inventory with stacking support
    /// </summary>
    /// <param name="item">The item data to add</param>
    /// <param name="_amount">The amount to add</param>
    public void AddItem(Item item)
    {
        // Check if item already exists in inventory
        var existingItem = AllItems.Find(i => i.Data == item.Data);

        if (existingItem != null)
        {
            // Stack with existing item by increasing amount
            existingItem.IncrementAmount(item.Amount);
        }
        else
        {
            // Add new item to inventory
            AllItems.Add(item);
        }

        PrintInventory();
    }

    /// <summary>
    /// Removes an item from the inventory
    /// </summary>
    /// <param name="item">The item data to remove</param>
    /// <param name="_amount">The amount to remove</param>
    public void RemoveItem(ItemData item, int _amount)
    {
        // Find the item in inventory
        var existingItem = AllItems.Find(i => i.Data == item);

        if (existingItem != null)
        {
            // Decrease the amount using the DecrementAmount method
            existingItem.DecrementAmount(_amount);

            // Remove item completely if amount reaches 0
            if (existingItem.Amount == 0)
            {
                AllItems.Remove(existingItem);
            }
        }
    }

    /// <summary>
    /// Prints the current inventory to the console in a formatted and styled manner.
    /// Displays a message if the inventory is empty, otherwise lists all items with their names and quantities.
    /// </summary>
    public void PrintInventory()
    {
        if (AllItems.Count == 0)
        {
            GD.PrintRich("[color=yellow]📦 Inventory is empty 📦[/color]");
            return;
        }

        GD.PrintRich("[color=cyan]📦 === INVENTORY === 📦[/color]");
        
        for (int i = 0; i < AllItems.Count; i++)
        {
            var item = AllItems[i];
            GD.PrintRich($"[color=white]   {i + 1}.[/color] [color=green]{item.Data.Name}[/color] [color=gray]x[/color][color=orange]{item.Amount}[/color]");
        }
        
    }

    /// <summary>
    /// Populates the inventory with sample items for testing
    /// </summary>
    public void PopulateInventory()
    {
        // Load item resources from the data folder
        var chickenData = GD.Load<ItemData>("res://Data/Chicken.tres");
        var milkData = GD.Load<ItemData>("res://Data/Milk.tres");

        // Add items to inventory with some default amounts
        if (chickenData != null)
        {
            AddItem(new Item(chickenData, 5));
        }
        
        if (milkData != null)
        {
            AddItem(new Item(milkData, 3));
        }

        GD.PrintRich("[color=green]✅ Inventory populated with sample items![/color]");
    }
}
