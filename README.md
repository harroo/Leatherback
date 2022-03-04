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
### InventoryObject
It is advised that you derive an `BaseItem`, or an `ItemBase` etcetera, and set the values you wish all items to have in it, then derive from it.

Like so:
```cs
// Base Item Class.
public class ItemBase : InventoryObject {

    // Extra member attributes.
    public int myNumber ;
    public string myName ;

    /* .. And so on and so forth. .. */

        // Constructor to set the InventoryObject attributes
        // that you'd like to use as defaults for your items.
        public ItemBase ( ) {

            // See the InventoryObject.cs file to see all base attributes.

            isTexture = false ;
            typeId = 0 ;
            stackable = true ;

            maxStackSize = 43 ;

        }

    // Also your base class can, but doesn't need to, define some important functions ..
            // These virtual methods are as unnecessary as they are optional.

        // Should return true if the Object's are compatible and there's room.
        public override bool CanMerge ( InventoryObject other ) { .. }

        // Should merge the objects unto each other.
        public override InventoryObject Merge ( InventoryObject other ) { .. }

        // Should revoke the specified amount from the given Object and award them to this one.
        public override InventoryObject TakeFrom ( InventoryObject other , int amountToTake ) { .. }

        // Notes that these do not and should not work when MetaData is of issue.
        // For Example; If the Item is say some sort of chest then there should be no stacking.
        // In which case CanMerge ( .. ) ; should return false.

        // These functions are predefined in the base InventoryObject, and do not need
        // to be overridden unless you've good reason to do as such.


    // **However** !
    // This method *should* be overridden by your BaseItem or ItemBase or what-have-you.
            // Overriding of this virtual method is quite necessary.

        // Copies values from this instance to the "target" instance, with optional amount ignorance.
        public override void CopyValuesTo ( InventoryObject targetObject , bool copyAmount = false ) {

            targetObject.isTexture = this.isTexture;
            targetObject.typeId = this.typeId;
            targetObject.stackable = this.stackable;
            targetObject.maxStackSize = this.maxStackSize;

            if (copyAmount) targetObject.amount = this.amount;
            targetObject.percent = this.percent;

            targetObject.metaData = this.metaData;

            /* And now your implemented attributes. */

            targetObject.myNumber = this.myNumber ;
            targetObject.myName = this.myName ;

        }

}

// Example of other Classes.
public class Sword : ItemBase {

    // Optional overruling Constructor to set custom
    // Defaults for this Item. Also changing some from the ItemBase.
    public Sword ( ) {

        isTexture = true ;
        typeId = 1 ;
        stackable = false ;

        myNumber = 0;
        myName = "Mein Schwert" ;

    }

    // This class *can* override some of the virtual methods, but only if necessary.
    // I can't, off the top of my head, think of a situation where this would be appropriate.

}
```
Furthermore there's the `MetaDataBuffer` that can be loaded with *any* data types.
This is useful for things like Backpacks, carry-able chests and such.

Here's how to use it:
```cs
// Set it ..
myItem.SetMetaData ( myBuffer ) ;

// Get it ..
byte [ ] myBuffer = myItem.GetMetaData ( ) ;

// Append to it ..
myItem.metaData.Append ( "string value" ) ; // String value.
myItem.metaData.Append ( 137 ) ; // Integer value.
myItem.metaData.Append ( 137.0f ) ; // Floating-Point value.

myItem.metaData.AppendT ( Anything ) ; // Any object, as in System.Object. So *anything*!


// Extract Information from it.

string myText = myItem.metaData.GetString ( ) ; // String value.
int myNumber = myItem.metaData.GetInt32 ( ) ; // Integer value.
float myFloat = myItem.metaData.GetSingle ( ) ; // Floating-Point value.

// Any object, as in System.Object. So *anything*!
MyClass anything = ( MyClass ) myItem.metaData.GetObject ( ) ;


```
**Note.**
`MetaDataBuffer`s MUST be deconstructed in the exact same order that they were constructed. Else the information may become obfuscated.

You can Reset the counter with `MetaDataBuffer.Reset ( ) ;`. This will allow you to start the deconstruction process once again.


---

Spelling and Orthography correction: [Kieralia](https://github.com/kieralia)
