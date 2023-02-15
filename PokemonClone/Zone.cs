public class Zone {
    public string name;
    public Tile[,] tilemap;
    public List<EncounterChance> encounterChances;
    public List<Sprite> sprites = new List<Sprite>();

    public Vector2 getSize() {
        return new Vector2(tilemap.GetLength(0), tilemap.GetLength(1));
    }

    public Tile getTile(Vector2 pos) {
        return tilemap[(int)pos.x,(int)pos.y];
    }
}

public struct EncounterChance {
    public Pokemon pokemon;
    public int minlvl;
    public int maxlvl;
    public int weight;
}
