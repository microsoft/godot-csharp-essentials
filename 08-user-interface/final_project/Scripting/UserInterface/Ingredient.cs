using Godot;
using System;
using System.Collections.Generic;

public partial class Ingredient : MarginContainer
{
    [Export] private Label _name;
    [Export] private TextureRect _icon;

    public Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();

    public override void _Ready()
    {
        Textures.Add("milk", GD.Load<Texture2D>("res://Sprites/Milk.png"));
        Textures.Add("chicken", GD.Load<Texture2D>("res://Sprites/Chicken_Uncooked.png"));

    }


    public void Setup(string name)
    {
        var texture = Textures[name.ToLower()];

        _name.Text = name;
        _icon.Texture = texture;
    }

}
