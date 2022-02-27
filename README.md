# Leatherback
A comprehensive inventory kit for Unity3D.
**By Harroo**

## How to set up
Simply copy the [Leatherback/](https://github.com/harroo/Leatherback/tree/main/Leatherback) folder into your Unity-Project's Assets directory.

It's recommended that you put it under a folder named "Plugins" or "Dependencies" or something of the like.

### Next
- 1: Add `InventoryPrefix` to a Game-Object, this is where you can configure the way the UI looks.
- 2: Then write and add your "InventoryManager" Script to an Object in the Canvas, this will be the Parent of the Inventory UI. See the Information section for more on this.
- 3: Setup the Canvas. To do this you should create a second Camera that is Orthographic and is used only for an "Inventory Canvas", which the Canvas *must* be named for Leatherback to find it.

It's recommended that you set up the Game-Objects like so:

![scrot0](https://raw.githubusercontent.com/harroo/Leatherback/main/Images/image1.png)
![scrot1](https://raw.githubusercontent.com/harroo/Leatherback/main/Images/image2.png)
![scrot2](https://raw.githubusercontent.com/harroo/Leatherback/main/Images/image3.png)
![scrot3](https://raw.githubusercontent.com/harroo/Leatherback/main/Images/image4.png)


## Information
You can see the [Examples/](https://github.com/harroo/Leatherback/tree/main/Examples) for some ideas on how exactly to use this Kit.

## Documentation
### To Enable sounds:
Simple open `Leatherback/InventorySlot.cs` uncomment the the indicated lines.
### InventoryManager
```cs
// Called when a slot is clicked.
public virtual void OnSlotClicked ( InventorySlot slot , bool leftClicked ) { }

// You can use it like such:
public override void OnSlotClicked ( InventorySlot slot , bool leftClicked ) {

    /* .. Your logic here .. */

}

// If it is left unchanged than it will act as usual.
```
See [Examples/ExampleInventory.cs](https://github.com/harroo/Leatherback/tree/main/Examples) for more in this.
### InventorySlot
```cs

```


---

Spelling and Orthography correction: [Kieralia](https://github.com/kieralia)
