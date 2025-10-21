using Godot;
using System;

/// <summary>
/// Represents an item in the inventory with its data and amount
/// </summary>
public partial class Item
{
    /// <summary>
    /// The item data resource containing item information
    /// </summary>
    public ItemData Data { get; private set; }

    /// <summary>
    /// The amount/quantity of this item in the inventory
    /// </summary>
    public int Amount { get; private set; }

    /// <summary>
    /// Constructor with parameters
    /// </summary>
    /// <param name="data">The item data</param>
    /// <param name="amount">The initial amount</param>
    public Item(ItemData data, int amount)
    {
        Data = data;
        Amount = amount;
    }

    /// <summary>
    /// Increments the amount of the item by a specified value.
    /// </summary>
    /// <param name="value">The value to increment by</param>
    public void IncrementAmount(int value) => Amount += value;

    /// <summary>
    /// Decrements the amount of the item by a specified value, ensuring it doesn't go below 0.
    /// </summary>
    /// <param name="value">The value to decrement by</param>
    public void DecrementAmount(int value) => Amount = Math.Max(0, Amount - value);
}
