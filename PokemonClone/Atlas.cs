

using Raylib_cs;
using static Raylib_cs.Raylib;

public struct Atlas {
    public Vector2 atlasSize;
    public Texture2D texture;
    public Vector2 imagesize;
    public Vector2 padding;
    public Vector2 offset;

    public void Draw(int index,Vector2 pos) {
        var location = new Vector2(index % (int)atlasSize.x, index / (int)atlasSize.x);
        var abspos = location * imagesize + location * padding + offset;
        
        var srcrect = new Rectangle(abspos.x,abspos.y,imagesize.x,imagesize.y);
        var dstrect = new Rectangle(pos.x, pos.y, imagesize.x * 4, imagesize.y * 4);
        
        DrawTexturePro(texture, srcrect, dstrect, new Vector2(0, 0), 0, Color.WHITE);
    }
}