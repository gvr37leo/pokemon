using Raylib_cs;
using static Globals;

public class Frame {
    public int index;
    public bool xFlip;
    public bool yFlip;
}

public class Anim {
    public List<Frame> frames = new List<Frame>();
}

class NPC {

    public void Start() {
        Timer timer = new Timer();
        timer.ListenOnce(1f, () => {
            //set pos in random dir
            
            //set sprites animation state
        });

    }

    public void Update() {
        //have sprite follow position


    }
}


public static class Animdata {

    public static Anim moveLeftAnim = new Anim() {
        frames = new List<Frame> {
            new Frame() { index = 5 },
            new Frame() { index = 2 },
        },
    };
    public static Anim moveRightAnim = new Anim() {
        frames = new List<Frame> {
            new Frame() { index = 5, xFlip = true },
            new Frame() { index = 2, xFlip = true },
        },
    };
    public static Anim moveUpAnim = new Anim() {
        frames = new List<Frame> {
            new Frame() { index =  4},
            new Frame() { index =  1},
        },
    };
    //new TextureEx() { tex = playerFrontTex },
    public static Anim moveDownAnim = new Anim() {
        frames = new List<Frame> {
            new Frame() { index = 3},
            new Frame() { index = 0},
        },
    };

    public static List<Anim> moveDirAnims = new List<Anim> { moveRightAnim, moveLeftAnim, moveDownAnim, moveUpAnim};

    public static List<Anim> idleAnim = new List<Anim> {
        new Anim{ frames = new List<Frame> {new Frame() { index = 2, xFlip = true}}},
        new Anim{ frames = new List<Frame> {new Frame() { index = 2}}},
        new Anim{ frames = new List<Frame> {new Frame() { index = 0}}},
        new Anim{ frames = new List<Frame> {new Frame() { index = 1}}},
    };

}

