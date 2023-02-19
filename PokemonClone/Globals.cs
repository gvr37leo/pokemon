using Raylib_cs;
using static Raylib_cs.Raylib;
using static moveType;
using static Raylib_cs.Color;
using static Utils;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class Globals {

    public static ProgramData pd;
    //https://www.pokencyclopedia.info/en/index.php?id=sprites/gen2/spr_gold


    public static Atlas overworldAtlas;
    public static Atlas pokemonAtlas;
    public static Atlas pokemonAtlasBack;
    public static Atlas englishAtlas;
    public static Atlas tilesetAtlas;

    public static int pixelscale = 4;
    public static Vector2 tilesize;
    public static Vector2 screensize;

    //https://downloads.khinsider.com/search?search=pokemon&type=album&sort=relevance&album_type=&album_year=&album_category=3
    public static Sound battlestartsfx;
    public static Sound wallbumpsfx;
    public static List<MenuItem> menuStack = new List<MenuItem>();
    public static Frame currentPlayerTex;
    public static Zone currentZone;
    public static Sequencer battlestartseq;
    public static float battlestartTimestamp;
    public static bool xFlip;



    public static List<Move> moves;

    public static List<Pokemon> pokemons;

    public static void Init() {

        moves = new List<Move>() {
            new Move() {
                name = "scratch",
                power = 30,
                hitchance = 0.95f,
                maxpp = 40,
                type = normal,
            },
            new Move() {
                name = "growl",
                power = 0,
                hitchance = 1,
                maxpp = 40,
                type = normal,
                cb = () => {
                    //lower attack opponent
                },
            },
            new Move() {
                name = "ember",
                power = 50,
                hitchance = 1,
                maxpp = 25,
                type = fire,
                cb = () => {

                },
            },
            new Move() {
                name = "bubble",
                power = 40,
                hitchance = 1,
                maxpp = 25,
                type = fire,
                cb = () => {

                },
            },
            new Move() {
                name = "vine whip",
                power = 40,
                hitchance = 1,
                maxpp = 25,
                type = grass,
                cb = () => {

                },
            },
        };


        tilesetAtlas = new Atlas() {
            atlasSize = new Vector2(8, 100),
            imagesize = new Vector2(16, 16),
            padding = new Vector2(0,0),
            offset = new Vector2(0, 0),
            texture = LoadTexture("./res/tileset2.png"),
        };

        overworldAtlas = new Atlas() {
            atlasSize = new Vector2(48, 100),
            imagesize = new Vector2(16, 16),
            padding = new Vector2(0,0),
            offset = new Vector2(0, 0),
            texture = LoadTexture("./res/overworldAtlas2.png"),
        };

        pokemonAtlas = new Atlas() {
            atlasSize = new Vector2(10, 26),
            imagesize = new Vector2(56, 56),
            padding = new Vector2(115, 188),
            offset = new Vector2(1, 18),
            texture = LoadTexture("./res/pokemonatlas.png"),
        };

        pokemonAtlasBack = new Atlas() {
            atlasSize = new Vector2(10, 26),
            imagesize = new Vector2(48, 48),
            padding = new Vector2(123, 140),
            offset = new Vector2(9, 140),
            texture = LoadTexture("./res/pokemonatlas.png"),
        };

        englishAtlas = new Atlas() {
            atlasSize = new Vector2(16,8),
            imagesize = new Vector2(8, 8),
            padding = new Vector2(1, 1),
            texture = LoadTexture("./res/englishfont.png"),
        };

        pd.zone1 = LoadTiledMap("./maps/map1.json","./maps/tileset1.tsj");
        pd.zone2 = LoadTiledMap("./maps/map2.json", "./maps/tileset1.tsj");
        pd.zones = new List<Zone>() {pd.zone1, pd.zone2 };
    }

    public static void ApplyMove(PokemonInstance attacker, PokemonInstance defender, MoveInstance move) {
        var attTemp = pokemons.Find(p => p.name == attacker.pokemon);
        var defTemp = pokemons.Find(p => p.name == defender.pokemon);
        var moveTemp = moves.Find(m => m.name == move.move);

        var attackerTemplate = pokemons.Find(p => p.name == attacker.pokemon);
        float STAB = attackerTemplate.types.Contains(moveTemp.type) ? 1.25f : 1;
        
        pd.messageQueue.Add($"{attacker.nickname} used {moveTemp.name}");
        var effectiveness = pd.typeEffecttivenessTable.get(moveTemp.type, defTemp.types);
        defender.hp -= (int)(moveTemp.power * effectiveness * STAB);
        move.pp--;

        pd.animationQueue.Add(0);
        pd.messageQueue.Add($"move was {effectiveness}x effective");
    }

    public static void DrawHealthBar(int x, int y, PokemonInstance pokemonInst) {
        float healthratio = (float)pokemonInst.hp / pokemonInst.stats.maxhp;
        Color color;
        if (healthratio > 0.8) {
            color = GREEN;
        } else if (healthratio > 0.3) {
            color = ORANGE;
        } else {
            color = RED;
        }
        int hpbarwidth = 200;
        DrawRectangle(x, y, (int)map(pokemonInst.hp, 0, pokemonInst.stats.maxhp, 0, hpbarwidth), pixelscale, color);
        DrawRectangle(x, y + pixelscale, hpbarwidth, pixelscale, BLACK);
    }

    public static PokemonInstance GeneratePokemonInstance(Pokemon pokemon, int lvl) {
        
        var eligibleMoves = pokemon.moveLearnSet.FindAll(x => x.lvl <= lvl).Select(x => moves.Find(m => m.name == x.move)).ToList();
        var pkmninstance = new PokemonInstance() {
            nickname = pokemon.name,
            pokemon = pokemon.name,
            hp = 100,
            lvl = lvl,
            xp = 0,
            stats = new Stats() {
                maxhp = 100,
            }
        };
        while (eligibleMoves.Any() && pkmninstance.moves.Count < 4) {
            var x = chooseRandom(eligibleMoves);
            eligibleMoves.Remove(x);
            pkmninstance.moves.Add(new MoveInstance() {
                move = x.name,
                pp = x.maxpp,
            });
        }

        return pkmninstance;
    }

    //right,left,down,up
    public static int ConvertVec2Dir(Vector2 v) {
        if (v.x == 1) {
            return 0;
        } else if (v.x == -1) {
            return 1;
        } else if (v.y == 1) {
            return 2;
        } else if (v.y == -1) {
            return 3;
        } else {
            return 4;
        }
    }

    public static void SaveGame() {
        //player
        //party
        //map
        //location
        //pokemoninstances
        string contents = "hello";
        Directory.CreateDirectory("./saves");
        File.WriteAllText("./saves/save1.txt",contents);
        var stream = new FileStream("./saves/save1.bin",FileMode.Create);
        stream.WriteByte((byte)'H');
        stream.WriteByte((byte)'e');
        stream.WriteByte((byte)'l');
        stream.WriteByte((byte)'l');
        stream.WriteByte((byte)'0');
        stream.WriteByte(0);
        //stream.Close();
        stream.Dispose();

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream strm = new FileStream("./saves/save1.bin", FileMode.Create)) {
            using(BinaryWriter writer = new BinaryWriter(strm)) {
                //writer.Write("");
                //writer.Write(true);
                //writer.Write(1);
                //writer.Write(21f);
                formatter.Serialize(strm, pd.player);
            }
        }

    }

    public static void LoadGame() {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream strm = new FileStream("./saves/save1.bin", FileMode.Open)) {
            using (BinaryReader writer = new BinaryReader(strm)) {
                //writer.ReadString();
                //writer.ReadBoolean();
                //writer.ReadInt32();
                //writer.ReadSingle();

                Player player = (Player)formatter.Deserialize(strm);
                pd.player = player;
            }
        }
    }


    public static EncounterChance pickEncounter(Zone zone) {
        List<int> encounter = new List<int>();
        for (int i = 0; i < currentZone.encounterChances.Count; i++) {
            var cur = currentZone.encounterChances[i];
            encounter.AddRange(Enumerable.Repeat(i, cur.weight));
        }
        return zone.encounterChances[chooseRandom(encounter)];
    }

    public static void changeScreen(Screen screen) {
        if (pd.currentScreen?.unload != null) {
            pd.currentScreen.unload();
        }
        pd.currentScreen = screen;
        if (pd.currentScreen.setup != null) {
            pd.currentScreen.setup();
        }
    }

    public static Pokemon FindPokemon(string name) {
        return pokemons.Find(p => p.name == name);
    }

    public static Move FindMove(string name) {
        return moves.Find(m => m.name == name);
    }

    public static Vector2 index2Vector(int index, int width) {
        return new Vector2(index % width,index / width);
    }

    public static Zone LoadTiledMap(string mapfile,string tilesetfile) {
        //load zone
        var zone = new Zone();
        

        var tilesettext = File.ReadAllText(tilesetfile);
        JObject tileset = JObject.Parse(tilesettext);
        zone.tiles.Add(new Tile() { name = "nulltile" });
        foreach (var tile in tileset.Value<JArray>("tiles")) {
            var tileobj = new Tile();
            zone.tiles.Add(tileobj);
            //tileobj.name = tile.Value<string>("class");
            tileobj.id = tile.Value<int>("id");

            if(tile.Value<JArray>("properties") == null) {
                continue;
            }
            foreach (var prop in tile.Value<JArray>("properties")) {
                string name = prop.Value<string>("name");
                prop.Value<string>("type");
                if(name == "collides") {
                    tileobj.collides = prop.Value<bool>("value");
                }
                if(name == "name") {
                    tileobj.name = prop.Value<string>("value");
                }
            }

        }


        var text = File.ReadAllText(mapfile);
        JObject map = JObject.Parse(text);
        zone.name = map.Value<string>("name");
        zone.size.x = map.Value<int>("width");
        zone.size.y = map.Value<int>("height");

        var mapprops = map.Value<JArray>("properties");
        foreach (var prop in mapprops) {
            var propname = prop.Value<string>("name");

            if(propname == "name") {
                zone.name = prop.Value<string>("value");
            }
        }
        
        JArray layers = map.Value<JArray>("layers");
        foreach (var layer in layers) {
            if(layer.Value<string>("type") == "tilelayer") {
                zone.tilemap = layer.Value<JArray>("data").Values<int>().ToList();
            }else if (layer.Value<string>("type") == "objectgroup") {

                foreach (var tiledobj in layer.Value<JArray>("objects")) {
                    var obj = new Object();
                    zone.objects.Add(obj);
                    obj.name = tiledobj.Value<string>("name");
                    obj.pos.x = tiledobj.Value<float>("x") / 16;
                    obj.pos.y = tiledobj.Value<float>("y") / 16;

                    //trigger warp sign object
                    obj.type = tiledobj.Value<string>("class");

                    var properties = tiledobj.Value<JArray>("properties") ?? new JArray();

                    if(obj.type == "Object" || obj.type == "Warp") {
                        foreach (JObject prop in properties) {
                            var propname = prop.Value<string>("name");

                            if (propname == "collides") {
                                obj.collides = prop.Value<bool>("value");
                            }
                            if (propname == "dstmap") {
                                obj.dstmap = prop.Value<string>("value");
                            }
                            if (propname == "dstpoint") {
                                obj.dstpoint = prop.Value<string>("value");
                            }
                            if (propname == "dialogue") {
                                obj.dialogue = prop.Value<string>("value");
                            }
                            if (propname == "walks") {
                                obj.moves = prop.Value<bool>("value");
                            }
                        }
                    }

                    if(obj.type == "Encounter") {
                        var encounterChane = new EncounterChance();
                        foreach (JObject prop in properties) {
                            var propname = prop.Value<string>("name");

                            if (propname == "lvlmin") {
                                encounterChane.minlvl = prop.Value<int>("value");
                            }
                            if (propname == "lvlmax") {
                                encounterChane.maxlvl = prop.Value<int>("value");
                            }
                            if (propname == "weight") {
                                encounterChane.weight = prop.Value<int>("value");
                            }
                            if (propname == "pokemon") {
                                encounterChane.pokemon = FindPokemon(prop.Value<string>("value"));
                            }
                        }

                        zone.encounterChances.Add(encounterChane);
                    }
                }
            }
        }
        return zone;

    }
}

public class MoveAnim {

}