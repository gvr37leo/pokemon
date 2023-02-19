using Raylib_cs;

public class Zone {
    public string name;
    public List<int> tilemap;
    public int defaultTile = 0;
    public Vector2 size;
    public List<EncounterChance> encounterChances = new List<EncounterChance>();
    public List<Object> objects = new List<Object>();
    public List<Tile> tiles = new List<Tile>();



    public Tile getTile(Vector2 pos) {
        return tiles[tilemap[(int)(pos.y * size.x + pos.x)]];
    }
}

public class Tile {
    public int id;
    public string name;
    public bool collides;

    public override string ToString() {
        return name;
    }
}

public struct EncounterChance {
    public Pokemon pokemon;
    public int minlvl;
    public int maxlvl;
    public int weight;
}
