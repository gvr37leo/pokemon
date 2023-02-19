using static Raylib_cs.Raylib;

[Serializable]
public class Player {
    public Vector2 oldPos;
    public Vector2 spritePos;
    public Vector2 pos;
    public Vector2 dir;
    public List<PokemonInstance> pokemonParty;

    public int spriteIndex = 0;
    public Anim currentAnim;

}

