using Raylib_cs;

public struct Stats {
    public int maxhp;
    public int attack;
    public int defense;
    public int speed;
    public int specialattack;
    public int specialdefense;
}

public class Pokemon {
    public string name;
    public string evolve;
    public int evolveLvl;
    public List<moveType> types;
    public List<MoveLearn> moveLearnSet = new List<MoveLearn>();
    public int basexpValue;

    //stats
    public Stats stats;

    public int id { get; internal set; }
}

public class MoveLearn {
    public string move;
    public int lvl;
}

[Serializable]
public class PokemonInstance {
    public string nickname;
    public string pokemon;
    public List<MoveInstance> moves = new List<MoveInstance>();
    public int hp;
    public int lvl;
    public int xp;
    public int maxxp;

    //stats
    public Stats stats;
}


