using Raylib_cs;

public struct Tile {
    public string name;
    public Texture2D texture;
    public bool isBlocking;
    public bool isPortal;
    public int dstZone;
    public Vector2 dstPos;
    public Vector2 dstDir;

    public override string ToString() {
        return name;
    }
}
