# Fly By Cafe Sample Project

Fly By Cafe is a complete sample project built with Godot and C#. It demonstrates how to combine core engine features, scripting, and user interface elements into a cohesive game experience. This project expands on the tutorial series by introducing several advanced techniques that game developers commonly use in production projects.

![](../images/flybyCafe.png)

## Getting started

You can open this project in Godot and explore how everything connects together. Try collecting ingredients from the stations, opening your inventory with the I key, and using the workbench to craft items. Look at the scripts in the `Scripts/` folder to see how each system is implemented and explore the different scene objects.

## What's included

This project builds on everything you learned in the tutorial series and adds new game systems you'll encounter in real game development:

### Gravity and jump mechanics

The player movement system extends what you learned in the scripting tutorial by adding jump mechanics with gravity. The [CharacterBody3D](https://docs.godotengine.org/en/stable/classes/class_characterbody3d.html) detects when you're on the ground using `IsOnFloor()` and applies gravity when you're in the air, creating responsive platforming-style movement.

### Character animation with AnimationTree

While the tutorial series covered basic scripting and movement, this project introduces Godot's [AnimationTree](https://docs.godotengine.org/en/stable/classes/class_animationtree.html) system for managing complex character animations. The player character has multiple animation states—idle, walking, jumping, and picking up items—that smoothly blend together. The AnimationTree uses a [StateMachine](https://docs.godotengine.org/en/stable/classes/class_animationnodestatemachine.html) for locomotion transitions and [OneShot](https://docs.godotengine.org/en/stable/classes/class_animationnodeoneshot.html) nodes to layer animations on top of each other, letting the player pick up items while walking.

### Singleton pattern for global access

The Player and InventoryWindow classes use the singleton pattern to ensure only one instance exists and provide global access throughout your game. This is implemented in the `_EnterTree()` method, which checks if an instance already exists and removes duplicates if needed.

### Custom resource system

Items in this project use Godot's [Resource](https://docs.godotengine.org/en/stable/classes/class_resource.html) system to create reusable data assets. Each item is defined as a `.tres` file that stores its name and icon. This separates your game data from your code, making it easy to add new items without writing new scripts.

### Interaction system with Area3D detection

The tutorial series showed you how to detect when a player enters an area and display a debug message. This project extends that concept by using C# [interfaces](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/interfaces) to create a reusable interaction framework. Different objects can implement the same interface but respond differently—collectables add items to your inventory, while workbenches open crafting interfaces. This polymorphic approach makes it easy to add new interactive objects without duplicating code.

### Environmental animation with AnimationPlayer

The door in this project uses Godot's [AnimationPlayer](https://docs.godotengine.org/en/stable/classes/class_animationplayer.html) node to open and close automatically when you approach it. This demonstrates when to use AnimationPlayer (for simpler, object-specific animations) versus AnimationTree (for complex character state management). The door detects your presence using Area3D and triggers the animation in response.

### Inventory system

This project implements an inventory system that tracks items you collect from ingredient stations. Items automatically stack when you collect multiple of the same type, and the system manages adding and removing items as you craft. The inventory uses a singleton pattern so any part of your game can access it.

### Crafting and recipe system

The workbench lets you combine ingredients to create new items using a recipe system. When you interact with the workbench, it checks if you have the required ingredients, removes them from your inventory, and creates the finished product. This demonstrates how to validate requirements and transform game state based on player actions.

### World-space UI with SubViewport

The ingredient stations and workbench display interactive prompts that exist in 3D space rather than on your screen. These prompts use Godot's [SubViewport](https://docs.godotengine.org/en/stable/classes/class_subviewport.html) node to render 2D UI content that's then displayed on a Sprite3D in the game world. This technique creates UI elements that feel integrated into the environment rather than floating on top of it.


