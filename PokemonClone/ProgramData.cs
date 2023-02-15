using Raylib_cs;
using System.Diagnostics.Metrics;
using static Globals;
using static System.Net.Mime.MediaTypeNames;
using static moveType;
using static Raylib_cs.Raylib;

public class ProgramData {
    public PokemonInstance battleOpponent = null;
    public PokemonInstance currentPokemon = null;
    
    public Screen startScreen = new Screen();
    public Screen overworld = new Screen();
    public Screen battlescreen = new Screen();
    public TypeEffecttivenessTable typeEffecttivenessTable = new TypeEffecttivenessTable();
    public List<string> messageQueue = new List<string>();
    public List<int> animationQueue = new List<int>();
    public Screen currentScreen = null;
    public Player player;
    public MenuItem combatmenu;
    public MenuItem movemenu;
    public MenuItem pkmnmenu;
    public MenuItem mainmenu;
    public Zone testzone;
    public Zone zone2;
    public List<Zone> zones;

    public ProgramData() {


        wallbumpsfx = LoadSound("./res/wallbump.mp3");
        battlestartsfx = LoadSound("./res/battlestart.mp3");

        groundtex = LoadTexture("./res/ground.png");
        treetex = LoadTexture("./res/tree.png");
        tallgrasstex = LoadTexture("./res/tallgrass.png");

        pokemons = new List<Pokemon>() {
            new Pokemon(){
                name = "charmander",
                evolve = "charmeleon",
                evolveLvl = 16,
                types = new List<moveType>(){fire },
                moveLearnSet = new List<MoveLearn>() {
                    new MoveLearn(){lvl = 1, move = "scratch" },
                    new MoveLearn(){lvl = 1, move = "growl" },
                    new MoveLearn(){lvl = 5, move = "ember" },
                }
            },
            new Pokemon(){
                name = "charmeleon",
                evolve = "charizard",
                evolveLvl = 30,
                types = new List<moveType>(){fire },
                moveLearnSet = new List<MoveLearn>() {
                    new MoveLearn(){lvl = 1, move = "scratch" },
                    new MoveLearn(){lvl = 1, move = "growl" },
                    new MoveLearn(){lvl = 5, move = "ember" },
                }
            },
            new Pokemon(){
                name = "charizard",
                evolve = "",
                evolveLvl = 16,
                types = new List<moveType>(){fire },
                moveLearnSet = new List<MoveLearn>() {
                    new MoveLearn(){lvl = 1, move = "scratch" },
                    new MoveLearn(){lvl = 1, move = "growl" },
                    new MoveLearn(){lvl = 5, move = "ember" },
                }
            },

            new Pokemon(){
                name = "squirtle",
                evolve = "wartortle",
                types = new List<moveType>(){water },
                moveLearnSet = new List<MoveLearn>() {
                    new MoveLearn(){lvl = 1, move = "scratch" },
                    new MoveLearn(){lvl = 1, move = "growl" },
                    new MoveLearn(){lvl = 5, move = "ember" },
                }
            },
            new Pokemon(){
                name = "wartortle",
                evolve = "blastoise",
                types = new List<moveType>(){water },
                moveLearnSet = new List<MoveLearn>() {
                    new MoveLearn(){lvl = 1, move = "scratch" },
                    new MoveLearn(){lvl = 1, move = "growl" },
                    new MoveLearn(){lvl = 5, move = "ember" },
                }
            },
            new Pokemon(){
                name = "blastoise",
                evolve = "",
                types = new List<moveType>(){water },
                moveLearnSet = new List<MoveLearn>() {
                    new MoveLearn(){lvl = 1, move = "scratch" },
                    new MoveLearn(){lvl = 1, move = "growl" },
                    new MoveLearn(){lvl = 5, move = "ember" },
                }
            },
            new Pokemon(){
                name = "bulbasaur",
                evolve = "ivysaur",
                types = new List<moveType>(){water },
                moveLearnSet = new List<MoveLearn>() {
                    new MoveLearn(){lvl = 1, move = "scratch" },
                    new MoveLearn(){lvl = 1, move = "growl" },
                    new MoveLearn(){lvl = 5, move = "vine whip" },
                }
            },
            new Pokemon(){
                name = "ivysaur",
                evolve = "venosaur",
                types = new List<moveType>(){water },
                moveLearnSet = new List<MoveLearn>() {
                    new MoveLearn(){lvl = 1, move = "scratch" },
                    new MoveLearn(){lvl = 1, move = "growl" },
                    new MoveLearn(){lvl = 5, move = "vine whip" },
                }
            },
            new Pokemon(){
                name = "venosaur",
                evolve = "",
                types = new List<moveType>(){water },
                moveLearnSet = new List<MoveLearn>() {
                    new MoveLearn(){lvl = 1, move = "scratch" },
                    new MoveLearn(){lvl = 1, move = "growl" },
                    new MoveLearn(){lvl = 5, move = "vine whip" },
                }
            },
        };
        for (int i = 0; i < pokemons.Count; i++) {
            pokemons[i].id = i;
        }



        player = new Player() {
            pos = new Vector2(1, 1),
            dir = new Vector2(0, 1),
            pokemonParty = new List<PokemonInstance>() {
                new PokemonInstance() {
                    hp = 100,
                    stats = new Stats() {
                        maxhp = 100,
                    },
                    lvl = 5,
                    xp = 50,
                    maxxp = 100,
                    nickname = "flamy",
                    pokemon = "charmander",
                    moves = new List<MoveInstance>() {
                        new MoveInstance() {
                            move = "scratch",
                        },
                        new MoveInstance(){
                            move = "growl",
                        },
                        new MoveInstance(){
                            move = "ember",
                        }
                    }
                },

                new PokemonInstance() {
                    hp = 100,
                    stats = new Stats() {
                        maxhp = 100,
                    },
                    lvl = 5,
                    xp = 50,
                    maxxp = 100,
                    nickname = "shelly",
                    pokemon = "squirtle",
                    moves = new List<MoveInstance>() {
                        new MoveInstance() {
                            move = "scratch",
                        },
                        new MoveInstance(){
                            move = "growl",
                        },
                        new MoveInstance(){
                            move = "bubble",
                        }
                    }
                },
                GeneratePokemonInstance(pokemons.Find(p => p.name == "bulbasaur"), 5)
            }
        };


        combatmenu = new MenuItem().setSize(2, 2);
        movemenu = combatmenu.touch("fight").setSize(2, 2);
        pkmnmenu = combatmenu.touch("pokemon").setSize(1, 6);
        combatmenu.touch("bag");
        combatmenu.touch("run").onClick((item) => {
            changeScreen(overworld);
        });

        Action<MenuItem> moveselect = (item) => {
            ApplyMove(currentPokemon, battleOpponent, currentPokemon.moves[item.data]);

            if (pd.battleOpponent.hp <= 0) {
                currentPokemon.xp += FindPokemon(pd.battleOpponent.pokemon).basexpValue;
                while(currentPokemon.xp > currentPokemon.maxxp) {
                    currentPokemon.lvl++;
                    currentPokemon.xp = (int)Utils.to(currentPokemon.maxxp, currentPokemon.xp);
                    //increase stats
                }
                if(currentPokemon.lvl >= FindPokemon(currentPokemon.pokemon).evolveLvl) {
                    //evolve pokemon
                    currentPokemon.pokemon = FindPokemon(currentPokemon.pokemon).evolve;
                }
                changeScreen(pd.overworld);
                return;
                //wild pokemon faints
                //gain exp
                //move back to overworld
            } else {
                var enemymove = Utils.chooseRandom(pd.battleOpponent.moves);
                ApplyMove(pd.battleOpponent, pd.currentPokemon, enemymove);

                if (pd.currentPokemon.hp <= 0) {
                    //force switch pokemon or faint if none left
                }
            }
            menuStack.Add(pd.combatmenu);

        };
        movemenu.touch("move 1").Data(0).onClick(moveselect);
        movemenu.touch("move 2").Data(1).onClick(moveselect);
        movemenu.touch("move 3").Data(2).onClick(moveselect);
        movemenu.touch("move 4").Data(3).onClick(moveselect);


        Action<MenuItem> pkmnchange = (item) => {
            pd.currentPokemon = pd.player.pokemonParty[item.data];
            updateMenuMoves();
            menuStack.Add(pd.combatmenu);

        };
        pkmnmenu.touch("pkmn 1").Data(0).onClick(pkmnchange);
        pkmnmenu.touch("pkmn 2").Data(1).onClick(pkmnchange); ;
        pkmnmenu.touch("pkmn 3").Data(2).onClick(pkmnchange); ;
        pkmnmenu.touch("pkmn 4").Data(3).onClick(pkmnchange); ;
        pkmnmenu.touch("pkmn 5").Data(4).onClick(pkmnchange); ;
        pkmnmenu.touch("pkmn 6").Data(5).onClick(pkmnchange); ;

        mainmenu = new MenuItem().setSize(1, 4);
        mainmenu.touch("player");
        var bagmenu = mainmenu.touch("bag");
        mainmenu.touch("party");
        mainmenu.touch("map").onClick((item) => {
            messageQueue.Add("test text is good   supacalifragilisticexpilalidocious");
        }); ;
        mainmenu.touch("save").onClick((item) => {
            SaveGame();
        });
        mainmenu.touch("load").onClick((item) => {
            LoadGame();
        });


        bagmenu.touch("pokeball");
        bagmenu.touch("repel");



        testzone = new Zone() {
            name = "testzone",
            encounterChances = new List<EncounterChance> {
                new EncounterChance() {
                    pokemon = pokemons.Find(p => p.name == "squirtle"),
                    minlvl = 3,
                    maxlvl = 5,
                    weight = 1,
                }
            },
            tilemap = convertString2Zone(@"
                xxxxxxxxxxx
                x         x
                x  x      x
                xgggg     x
                x         x
                x         x
                x         x
                x        1x
                xxxxxxxxxxx"
            ),
            sprites = new List<Sprite>() {
                new Sprite() {
                    name = "woman",
                    dialogue = "Hello, how are you doing?",
                    gridpos = new Vector2(5,5),
                    atlas = womanAtlas,
                    moves = true,
                    onInteract= () => {

                    },
                }
            }
        };

        zone2 = new Zone() {
            name = "zone2",
            encounterChances = new List<EncounterChance> {
                new EncounterChance() {
                    pokemon = pokemons.Find(p => p.name == "charmander"),
                    minlvl = 6,
                    maxlvl = 8,
                    weight = 1,
                }
            },
            tilemap = convertString2Zone(@"
                xxxxxxxxxxx
                x0    ggx x
                x  x  ggg x
                x     ggx x
                x  x      x
                x  gggggx x
                x  x      x
                x         x
                xxxxxxxxxxx"
            ),
        };
        zones = new List<Zone>() {
            testzone,
            zone2,
        };



    }

    public void updateMenuMoves() {

        for (int i = 0; i < 4; i++) {
            movemenu.touch($"move {i + 1}").text = "--";
            movemenu.touch($"move {i + 1}").disabled = true;
        }

        for (int i = 0; i < 4 && i < currentPokemon.moves.Count; i++) {

            movemenu.touch($"move {i + 1}").text = currentPokemon.moves[i].move;
            movemenu.touch($"move {i + 1}").disabled = false;
        }


        movemenu.menuposition = screensize - combatmenu.getAbsSize();
    }

    public void updateMenuPokemon() {
        for (int i = 0; i < 6; i++) {
            pkmnmenu.touch($"pkmn {i + 1}").text = "--";
            pkmnmenu.touch($"pkmn {i + 1}").disabled = true;
        }

        for (int i = 0; i < 6 && i < player.pokemonParty.Count; i++) {
            pkmnmenu.touch($"pkmn {i + 1}").text = player.pokemonParty[i].nickname;
            pkmnmenu.touch($"pkmn {i + 1}").disabled = false;
        }
    }

    public Tile[,] convertString2Zone(string map) {
        map = map.Trim();
        var lines = map.Split("\r\n");
        var result = new Tile[lines[0].Length, lines.Length];

        for (int y = 0; y < lines.Length; y++) {
            var line = lines[y].Trim();
            for (int x = 0; x < line.Length; x++) {
                var ch = line[x];
                if (ch == ' ') {
                    result[x, y] = new Tile() {
                        name = "ground",
                        texture = groundtex,
                    };
                } else if (ch == 'x') {
                    result[x, y] = new Tile() {
                        name = "tree",
                        texture = treetex,
                        isBlocking = true,
                    };
                } else if (ch == 'g') {
                    result[x, y] = new Tile() {
                        name = "tallgrass",
                        texture = tallgrasstex,
                    };
                } else if (int.TryParse(ch.ToString(), out int res)) {
                    result[x, y] = new Tile() {
                        name = "portal",
                        texture = tallgrasstex,
                        isPortal = true,
                        dstZone = res,
                    };
                };
            }
        }

        return result;
    }

    public static Rect GenerateRect(Vector2 anchormin, Vector2 anchormax, Vector2 offsetmin, Vector2 offsetmax) {
        Vector2 absmin = anchormin * screensize + offsetmin;
        Vector2 absmax = anchormax * screensize + offsetmax;
        Rect rect = new Rect(absmin, absmin.to(absmax));
        return rect;
    }
}
