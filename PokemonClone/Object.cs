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

public class Object {
    //image
    public string name;
    public string type;
    public Vector2 pos;
    public bool collides;

    //person/sign
    public bool moves;
    public string dialogue;
    public Action onInteract;

    //warp
    public string dstmap;
    public string dstpoint;
    
    public Vector2 spritepos;

}
