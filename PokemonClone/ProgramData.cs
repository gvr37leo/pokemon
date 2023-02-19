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
    public Zone zone1;
    public Zone zone2;
    public List<Zone> zones;

    public ProgramData() {


        wallbumpsfx = LoadSound("./res/wallbump.mp3");
        battlestartsfx = LoadSound("./res/battlestart.mp3");


        pokemons = new List<Pokemon>() {
            new Pokemon(){
                name = "bulbasaur",
                evolve = "ivysaur",
                types = new List<moveType>(){grass },
                moveLearnSet = new List<MoveLearn>() {
                    new MoveLearn(){lvl = 1, move = "scratch" },
                    new MoveLearn(){lvl = 1, move = "growl" },
                    new MoveLearn(){lvl = 5, move = "vine whip" },
                }
            },
            new Pokemon(){
                name = "ivysaur",
                evolve = "venosaur",
                types = new List<moveType>(){grass },
                moveLearnSet = new List<MoveLearn>() {
                    new MoveLearn(){lvl = 1, move = "scratch" },
                    new MoveLearn(){lvl = 1, move = "growl" },
                    new MoveLearn(){lvl = 5, move = "vine whip" },
                }
            },
            new Pokemon(){
                name = "venosaur",
                evolve = "",
                types = new List<moveType>(){grass },
                moveLearnSet = new List<MoveLearn>() {
                    new MoveLearn(){lvl = 1, move = "scratch" },
                    new MoveLearn(){lvl = 1, move = "growl" },
                    new MoveLearn(){lvl = 5, move = "vine whip" },
                }
            },
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
                name = "pidgey",
                id = 16,
                evolve = "",
                types = new List<moveType>(){flying },
                moveLearnSet = new List<MoveLearn>() {
                    new MoveLearn(){lvl = 1, move = "scratch" },
                    new MoveLearn(){lvl = 1, move = "growl" },
                }
            },

            new Pokemon(){
                id = 19,
                name = "rattata",
                evolve = "",
                types = new List<moveType>(){normal },
                moveLearnSet = new List<MoveLearn>() {
                    new MoveLearn(){lvl = 1, move = "scratch" },
                    new MoveLearn(){lvl = 1, move = "growl" },
                }
            },

            new Pokemon(){
                id = 25,
                name = "pikachu",
                evolve = "",
                types = new List<moveType>(){electric },
                moveLearnSet = new List<MoveLearn>() {
                    new MoveLearn(){lvl = 1, move = "scratch" },
                    new MoveLearn(){lvl = 1, move = "growl" },
                }
            },
        };
        for (int i = 0; i < pokemons.Count; i++) {
            if (pokemons[i].id == 0) {
                pokemons[i].id = i + 1;
            }
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
                //GeneratePokemonInstance(pokemons.Find(p => p.name == "bulbasaur"), 5)
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
        var bagmenu = mainmenu.touch("bag");
        bagmenu.touch("pokeball");
        bagmenu.touch("repel");





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

    public static Rect GenerateRect(Vector2 anchormin, Vector2 anchormax, Vector2 offsetmin, Vector2 offsetmax) {
        Vector2 absmin = anchormin * screensize + offsetmin;
        Vector2 absmax = anchormax * screensize + offsetmax;
        Rect rect = new Rect(absmin, absmin.to(absmax));
        return rect;
    }
}
