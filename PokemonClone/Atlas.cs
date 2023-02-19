

using Raylib_cs;
using static Raylib_cs.Raylib;
using static Globals;

public class Atlas {
    public Texture2D texture;
    public Vector2 atlasSize;
    public Vector2 imagesize;
    public Vector2 padding;
    public Vector2 offset;
    public List<Tile> tiles = new List<Tile>();



    public void Draw(int index,Vector2 pos) {
        var location = new Vector2(index % (int)atlasSize.x, index / (int)atlasSize.x);
        var abspos = location * imagesize + location * padding + offset;
        
        var srcrect = new Rectangle(abspos.x,abspos.y,imagesize.x,imagesize.y);
        var dstrect = new Rectangle(pos.x, pos.y, imagesize.x * pixelscale, imagesize.y * pixelscale);
        if (xFlip) {
            srcrect.width *= -1;
        }
        DrawTexturePro(texture, srcrect, dstrect, new Vector2(0, 0), 0, Color.WHITE);
    }
}