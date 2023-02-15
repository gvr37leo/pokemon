using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static Raylib_cs.KeyboardKey;
using static Utils;
using Raylib_cs;
using static moveType;
using static Globals;
using static animdata;
using System.Text;
using System;



// https://www.raylib.com/examples.html
// https://www.raylib.com/cheatsheet/cheatsheet.html
// https://www.spriters-resource.com/game_boy_gbc/pokemongoldsilver/

//todo
//npc's
//trainers
//implement some battle moves in a generic way
//map editor
//turn without moving
//move data to seperate data files and util functions to util class
//save game,load game
//lvling
//only draw sprites visible to camera(premature)
tilesize = new Vector2(16, 16) * pixelscale;
screensize = new Vector2(12, 8) * tilesize;//768-512
InitWindow((int)screensize.x, (int)screensize.y, "PokeMon");
InitAudioDevice();


currentPlayerTex = new TextureEx() { tex = playerFrontTex };
Globals.Init();
pd = new ProgramData();
currentZone = pd.testzone;

//var shader = LoadShader("./shaders/testvertexshader.vs", "./shaders/testfragmentshader.fs");
var shader = LoadShader(null, "./shaders/testfragmentshader.fs");




Camera2D overworldCamera = new Camera2D();
var eventManager = new EventManager();
SetRandomSeed(0);


eventManager.listen("keypressed", (keyo) => {
    KeyboardKey key = (KeyboardKey)keyo;
    if(pd.messageQueue.Count > 0) {
        if (key == KEY_ENTER) {
            pd.messageQueue.RemoveAt(0);
        }
    }else if (menuStack.Count > 0) {
        var current = menuStack.Last();
        if (key == KEY_BACKSPACE) {
            menuStack.RemoveAt(menuStack.Count - 1);
        }
        if (key == KEY_ENTER) {
            if (current.getSelectedItem().disabled) {
                return;
            }
            if (current.getSelectedItem().children.Count > 0) {
                current.getSelectedItem().setCursor(0);
                menuStack.Add(current.getSelectedItem());
            } else {
                menuStack.Clear();
                var item = current.getSelectedItem();
                item.cb(item);
                //eventManager.trigger("menu", item);
            }
        }
        if (key == KEY_LEFT) {
            current.setCursor(current.cursor - 1);
        }
        if (key == KEY_UP) {
            current.setCursor((int)(current.cursor - current.size.x));
        }
        if (key == KEY_DOWN) {
            current.setCursor((int)(current.cursor + current.size.x));
        }
        if (key == KEY_RIGHT) {
            current.setCursor(current.cursor + 1);
        }
        return;
    }
});


eventManager.listen("menu", item => {
    var menuitem = (MenuItem)item;
    if (menuitem.id.Contains("pkmn") == false) {
        return;
    }

    int pkmnsloti = 0;
    int.TryParse(menuitem.id[5].ToString(), out pkmnsloti);
    pkmnsloti--;

    pd.currentPokemon = pd.player.pokemonParty[pkmnsloti];
    menuStack.Add(pd.combatmenu);
});



eventManager.listen("pokemonswitch", item => {
    //update menuitem names
});

pd.battlescreen.setup = () => {
    pd.currentPokemon = pd.player.pokemonParty[0];
    pd.updateMenuMoves();
    pd.updateMenuPokemon();
    PlaySound(battlestartsfx);
    SetSoundVolume(battlestartsfx, 0.1f);

    battlestartTimestamp = GetTimeF();

    pd.combatmenu.menuposition = screensize - pd.combatmenu.getAbsSize();
    menuStack.Add(pd.combatmenu);

    battlestartseq = new Sequencer();
    battlestartseq.Show(1, "trainerReveal").Wait();
    battlestartseq.Listen(() => {
        //change shader
    });
    battlestartseq.Show(1, "UISlideIn").Wait();
    battlestartseq.Show(1, "trainerSlideOut").Wait();
    battlestartseq.Show(1, "pokeballthrow").Wait();
    battlestartseq.Show(1, "pokemonAppear").Wait();
    //could also listen for updates
};

pd.battlescreen.update = () => {
    battlestartseq.Update(GetFrameTime());
    if (IsSoundPlaying(battlestartsfx) == false) {
        PlaySound(battlestartsfx);
    }
};

pd.battlescreen.draw = () => {
    var time = to(battlestartTimestamp,GetTimeF());
    //start black and white
    //pokemons/trainers move in from left to right and right to left, color palette changes at the end
    //UI slides in, pokeballs come in quickly 1 by 1 with sound effect
    //trainer slides out and does throwing animation
    //pokeball gets thrown in while rotating
    //pokeball flashes open with sound effect
    //pokemon grows into battle


    
    var opponentPok = pokemons.Find(p => p.name == pd.battleOpponent.pokemon);
    int enemyposx = 50;
    int enemyposy = 50;
    DrawText(pd.battleOpponent.nickname, enemyposx, enemyposy, 40, BLACK);
    DrawText(pd.battleOpponent.lvl.ToString(), enemyposx, enemyposy + 40, 40, BLACK);
    DrawHealthBar(enemyposx, enemyposy + 80, pd.battleOpponent);

    var oppPos = new Vector2(-200,50).lerp(new Vector2(400, 50), battlestartseq.Seek(time, "pokemonAppear"));
    pokemonAtlas.Draw(opponentPok.id, oppPos);
    
    
    

    var ownpokemon = pokemons.Find(p => p.name == pd.currentPokemon.pokemon);
    int selfposx = 420;
    int selfposy = 300;
    DrawText(pd.currentPokemon.nickname, selfposx, selfposy, 40, BLACK);
    DrawText(pd.currentPokemon.lvl.ToString(), selfposx, selfposy + 40, 40, BLACK);
    DrawHealthBar(selfposx, selfposy + 80, pd.currentPokemon);
    DrawRectangle(selfposx, selfposy + 88, (int)map(pd.currentPokemon.xp, 0, pd.currentPokemon.maxxp, 0, 200), pixelscale, BLUE);

    pokemonAtlasBack.Draw(ownpokemon.id, new Vector2(800, 200).lerp(new Vector2(50, 200), battlestartseq.Seek(time, "pokemonAppear")));

};

pd.battlescreen.unload = () => {
    menuStack.Clear();
    StopSound(battlestartsfx);
};




pd.overworld.setup = () => {

    eventManager.listen("keypressed", (keyo) => {
        KeyboardKey key = (KeyboardKey)keyo;

        if (key == KEY_Q) {
            pd.mainmenu.setCursor(0);
            menuStack.Add(pd.mainmenu);
        }
    },"overworld");

    //every few seconds move the walking sprites in the zones
};


//every gameupdate

pd.overworld.update = () => {
    float dt = GetFrameTime();
    Vector2 userinp = getInput();

    eventManager.process();


    if (pd.player.isMoving == false && menuStack.Count == 0 && pd.messageQueue.Count == 0){
        //movement code
        pd.player.spritePos = pd.player.pos;
        if (userinp.length() > 0) {
            pd.player.dir = userinp;
            pd.player.oldPos = pd.player.pos;
            pd.player.pos += userinp;
            //calculate the progression that should happen over 1 frame start moveprogression at that point
            pd.player.moveStartTimeStamp = GetTimeF();
            //this causes stutter because player doesnt immediatly start walking
            //but stands still 1 frame at the start of animation

        }
        int dirint = ConvertVec2Dir(pd.player.dir);
        currentPlayerTex = idleTexts[dirint];
    }

    if (pd.player.isMoving) {
        //animation code
        pd.player.spritePos = pd.player.oldPos.lerp(pd.player.pos, pd.player.moveProgression);
        var curMoveAnim = moveDirAnims[ConvertVec2Dir(pd.player.dir)];
        int animindex = (int)lerp(0, curMoveAnim.textures.Count - 0.0001f, pd.player.moveProgression);
        currentPlayerTex = curMoveAnim.textures[animindex];
    }
    pd.player.update();
    overworldCamera.offset = (screensize / 2).convert();
    overworldCamera.target = (pd.player.spritePos * tilesize + tilesize / 2).convert();
    overworldCamera.zoom = 1;
    overworldCamera.rotation = 0;


    //bump into wall reset
    var currentTile = currentZone.getTile(pd.player.pos);
    if (pd.player.startedMoving) {
        
        if (currentTile.isBlocking || currentZone.sprites.Any(s => s.gridpos == pd.player.pos)) {
            pd.player.pos = pd.player.oldPos;
            PlaySound(wallbumpsfx);
        }
    }

    //stoppedmoving event
    if(pd.player.stoppedMoving) {
        if (currentTile.name == "tallgrass") {
            if (GetRandomValue(0, 100) > 50) {
                var encounter = pickEncounter(currentZone);
                pd.battleOpponent = GeneratePokemonInstance(encounter.pokemon, GetRandomValue(encounter.minlvl, encounter.maxlvl));
                changeScreen(pd.battlescreen);
            }
        }else if(currentTile.name == "portal") {
            //unload oldzone, load new zone
            currentZone = pd.zones[currentTile.dstZone];
        }

        //animation flipping
        moveFrontAnim.textures[0].xFlip = !moveFrontAnim.textures[0].xFlip;
        moveBackAnim.textures[0].xFlip = !moveBackAnim.textures[0].xFlip;
    }
};

pd.overworld.draw = () => {

    BeginMode2D(overworldCamera);
    Vector2 zonesize = currentZone.getSize();
    for (int x = 0; x < zonesize.x; x++) {
        for (int y = 0; y < zonesize.y; y++) {
            DrawTextureEx(currentZone.tilemap[x, y].texture, new Vector2(x, y).mul(tilesize), 0, pixelscale, WHITE);
        }
    }

    foreach (var sprite in currentZone.sprites) {
        var abspos = sprite.gridpos * tilesize;
        sprite.atlas.Draw(0,abspos);
        //DrawTextureEx(sprite.image, abspos,0,pixelscale, WHITE);
    }
    
    DrawTexturePro(currentPlayerTex.tex, new Rectangle(0, 0, currentPlayerTex.xFlip ? -16 : 16, currentPlayerTex.yFlip ? -16 : 16), new Rectangle(pd.player.spritePos.x * tilesize.x, pd.player.spritePos.y * tilesize.y, tilesize.x, tilesize.y), zero.convert(), 0, WHITE);
    EndMode2D();
};

pd.overworld.unload = () => {
    eventManager.unlisten("overworld");
};





changeScreen(pd.overworld);
SetTargetFPS(60);

while (!WindowShouldClose()) {
    BeginDrawing();
    ClearBackground(RAYWHITE);
    if (pd.currentScreen.update != null) {
        pd.currentScreen.update();
    }


    int key = GetKeyPressed();
    while(key != 0) {
        eventManager.trigger("keypressed", key);
        key = GetKeyPressed();
    }
    
    eventManager.process();
    if (pd.currentScreen.draw != null) {
        //BeginShaderMode(shader);
            pd.currentScreen.draw();
        //EndShaderMode();
    }
    if (menuStack.Count > 0) {
        menuStack.Last().Draw();
    }
    if(pd.messageQueue.Count > 0) {
        var msg = pd.messageQueue.First();
        var textRect = ProgramData.GenerateRect(new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, -80), new Vector2(400,0));
        var tl = textRect.edge(zero);
        var size = textRect.size();
        DrawRectangleV(tl, size, WHITE);
        DrawBorder(tl, size,BLACK);
        DrawTextMultiLine(msg, tl, 40,(int)size.x, BLACK);
    }
    EndDrawing();
}
CloseWindow();






