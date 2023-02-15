using Raylib_cs;
using static Globals;

public class TextureEx {
    public Texture2D tex;
    public bool xFlip;
    public bool yFlip;
}

public class Anim {
    public List<TextureEx> textures;
}

public class Anim2 {
    public Action setup;
    public Action update;
    public Action end;
}


public static class animdata {

    public static Anim moveLeftAnim = new Anim() {
        textures = new List<TextureEx> {
            new TextureEx() { tex = playerSide2Tex },
            new TextureEx() { tex = playerSideTex },
        },
    };
    public static Anim moveRightAnim = new Anim() {
        textures = new List<TextureEx> {
            new TextureEx() { tex = playerSide2Tex, xFlip = true },
            new TextureEx() { tex = playerSideTex,xFlip = true },
        },
    };
    public static Anim moveFrontAnim = new Anim() {
        textures = new List<TextureEx> {
            new TextureEx() { tex = playerFront2Tex },
            new TextureEx() { tex = playerFrontTex },
            //new TextureEx() { tex = playerFront2Tex,xFlip = true },
            //new TextureEx() { tex = playerFrontTex },
        },
    };
    //new TextureEx() { tex = playerFrontTex },
    public static Anim moveBackAnim = new Anim() {
        textures = new List<TextureEx> {
            new TextureEx() { tex = playerBack2Tex },
            new TextureEx() { tex = playerBackTex },
            //new TextureEx() { tex = playerBack2Tex,xFlip = true },
            //new TextureEx() { tex = playerBackTex },
        },
    };

    public static List<Anim> moveDirAnims = new List<Anim> { moveRightAnim, moveLeftAnim, moveFrontAnim, moveBackAnim };
    
    public static List<TextureEx> idleTexts = new List<TextureEx> {
        new TextureEx() { tex = playerSideTex,xFlip = true } ,
        new TextureEx() { tex = playerSideTex },
        new TextureEx() { tex = playerFrontTex },
        new TextureEx() { tex = playerBackTex },
        new TextureEx() { tex = playerFrontTex },
    };

}

