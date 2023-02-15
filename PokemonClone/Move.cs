public class Move {
    public string name;
    public int power;
    public float hitchance;
    public int maxpp;
    public Action cb;
    public moveType type;
}

[Serializable]
public class MoveInstance {
    public string move;
    public int pp;
}
