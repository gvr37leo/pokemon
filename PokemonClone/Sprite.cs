using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//signs
//people
//buttons
//trainers?

//collectable items


public class WorldItem {
    public Item item;
    public bool hidden;
}

public class Item {
    public string name;
    public int price;
    public Action effect;
}

public class Sprite {
    //image
    public string name;
    public bool moves;
    public string dialogue;
    public Action onInteract;
    public Atlas atlas;
    public Vector2 gridpos;
    public Vector2 spritepos;


}
