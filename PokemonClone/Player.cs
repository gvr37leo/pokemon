using static Raylib_cs.Raylib;

[Serializable]
public class Player {
    public Vector2 oldPos;
    public Vector2 spritePos;
    public Vector2 pos;
    public Vector2 dir;
    public List<PokemonInstance> pokemonParty;

    public float squaretravelTime = 0.5f;
    public float moveStartTimeStamp = 0;
    public bool oldIsMoving;
    public bool isMoving;
    public float moveProgression;
    public bool stoppedMoving;
    public bool startedMoving;

    public void update() {
        oldIsMoving = isMoving;
        isMoving = isMovingF();
        moveProgression = moveProgressionF();

        stoppedMoving = oldIsMoving == true && isMoving == false;
        startedMoving = isMoving == true && oldIsMoving == false;
    }

    public bool isMovingF() {
        return Utils.inRange(moveProgressionF(), 0, 1); 
    }

    public float moveProgressionF() {
        return Utils.map((float)GetTime() + GetFrameTime(), moveStartTimeStamp, moveStartTimeStamp + squaretravelTime, 0, 1);
    }

}
