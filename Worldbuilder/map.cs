using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine;
using System.Linq;
using System.Net;

namespace Worldbuilder
{
    [Serializable]
    public class env
    {
        public bool overGUI;
        public bool canend;
        public bool ended;

        public env()
        {
            canend = false;
            ended = false;
            overGUI = false;
        }
        void Update()
        {
            overGUI = false;
        }
    }

    [Serializable]
    public class map
    {


        public string mapName;
        public static string mapsFilesPath = Application.dataPath+@"\Textures\Maps\";
        public string mapPath;
        public string mapFilePath;
        public List<List<Zone>> zoneMap;
        public List<List<System.Drawing.Color>> zoneColorMap;
        public List<List<Tile>> tileMap;
        public List<Tile> TileMapRow;
        public env environement;
        public int ndx;
        public int nby;



        public map(string x, env y)
        {
            mapName = x;
            environement = y;
            zoneMap = new List<List<Zone>>();
            zoneColorMap = new List<List<System.Drawing.Color>>();
            tileMap = new List<List<Tile>>();
        }

        public string getmappath (string x)
        {
            mapName = x;
            mapPath = mapsFilesPath + mapName + @"\";
            if (Directory.Exists(mapPath))
            {
                mapFilePath = mapPath + mapName + ".png";
                if (File.Exists(mapFilePath))
                {
                    //Debug.Log("found repertory " + mapname);
                    return mapFilePath;
                }
                else
                {
                    Debug.Log("file not found");
                    return "error";
                }
            }
            else 
            {
                Debug.Log("repertory not found");
                return "error";
            }
        }

        public string getzonedeftxt(string x)
        {
            mapName = x;
            mapPath = mapsFilesPath + mapName + @"\";
            if (Directory.Exists(mapPath))
            {
                mapFilePath = mapPath + mapName + ".txt";
                if (File.Exists(mapFilePath))
                {
                    //Debug.Log("file found " + mapname + ".txt.");
                    return mapFilePath;
                }
                else
                {
                    Debug.Log("file not found");
                    return "error";
                }
            }
            else 
            {
                Debug.Log("repertory not found");
                return "error"; 
            }
        }

        public void generatemaptilemap()
        {
            foreach (List<Zone> list in this.zoneMap)
            {
                for (int i = 0; i < 16; i++)
                {
                    this.tileMap.Add(this.gettilemaprow(i, list));
                }
            }
        }

        public List<Tile> gettilemaprow(int index, List<Zone> list)
        {
            List<Tile> temp = new List<Tile>();
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    temp.Add(list[i].tilemap[index][j]);
                    //if (list[i].tilemap[index][j].type != "empty")
                    //{
                    //    //Debug.Log("adding tile to list " + list[i].tilemap[index][j].type);
                    //}
                }
            }
            return temp;

        }

        public void tileCheckForEmptyToWall ()
        {
            for (int i = 0; i < tileMap.Count; i++)
            {
                for (int j = 0; j < tileMap[i].Count; j++)
                {
                    if (tileMap[i][j].type == "openfloorTile")
                    {
                        List<Tile> temp = tileMap[i][j].getsouroundingTiles();
                        for (int k = 0; k < temp.Count; k++)
                        {
                            if (temp[k].type == "empty")
                            {
                                temp[k].tilehandle.SendMessage("changetowall");
                            }
                            else if (temp[k].type == "border")
                            {
                                temp[k].tilehandle.SendMessage("changetowallborder");
                            }
                        }
                    }
                }
            }
        }

        public List<Pion> pionAdjacent(Pion pion)
        {
            int x = Pion.getPosX();
            int y = Pion.getPosY();
            List<Pion> pionAdjacent = new List<Pion>();
            Tuile tuileNord = tuileMap[y - 1][x];
            Tuile tuileSud = tuileMap[y + 1][x];
            Tuile tuileEst = tuileMap[y][x + 1];
            Tuile tuileOuest = tuileMap[y][x - 1];
            string typeNord = tuileNord.getType();
            string typeSud = tuileSud.getType();
            string typeEst = tuileEst.getType();
            string typeOuest = tuileOuest.getType();
            /*
            Console.WriteLine("Au nord il y a : une tuile type " + typeNord);
            Console.WriteLine("Au sud il y a : une tuile type " + typeSud);
            Console.WriteLine("A l'est il y a : une tuile type " + typeEst);
            Console.WriteLine("A l'ouest il y a : une tuile type " + typeOuest);
             * */
            if (typeNord == "pion")
            {
                pionAdjacent.Add(tuileNord.getPion());
            }
            if (typeSud == "pion")
            {
                pionAdjacent.Add(tuileSud.getPion());
            }
            if (typeEst == "pion")
            {
                pionAdjacent.Add(tuileEst.getPion());
            }
            if (typeOuest == "pion")
            {
                pionAdjacent.Add(tuileOuest.getPion());
            }

            Console.WriteLine("Pion adjacent : " + pionAdjacent.Count());

            return pionAdjacent;

        }//FIN ADJACENT

        public void attaquer(int direction, Pion pion)
        {
            int x = pion.getPosX();
            int y = pion.getPosY();
            Console.WriteLine("x : " + x + " y : " + y);
            if (direction == 1)
            {
                Console.WriteLine("A l'attaque ! nord " + tuileMap[y - 1][x].getType());
                tuileMap[y - 1][x] = new Tuile(x, y - 1, "empty");
            }
            else if (direction == 2)
            {
                Console.WriteLine("A l'attaque ! sud " + tuileMap[y + 1][x].getType());
                tuileMap[y + 1][x] = new Tuile(x, y + 1, "empty");
            }
            else if (direction == 3)
            {
                Console.WriteLine("A l'attaque ! est " + tuileMap[y][x + 1].getType());
                tuileMap[y][x + 1] = new Tuile(x + 1, y, "empty");
            }
            else if (direction == 4)
            {
                Console.WriteLine("A l'attaque ! ouest " + tuileMap[y][x - 1].getType());
                tuileMap[y][x - 1] = new Tuile(x - 1, y, "empty");
            }

            Console.WriteLine("fin de l'attaque");
            afficherMap();

        }//FIN ATTAQUER


    }

    [Serializable]
    public class Zone
    {
        public string type;
        public List<List<Tile>> tilemap;
        public GameObject zonepoint;
        public map map;

        public Zone(string x, GameObject y, map m)
        {
            type = x;
            zonepoint = y;
            tilemap = new List<List<Tile>>();
            map = m;
        }

        

        public string getzoneimagepath(string x, string y, string z)
        {
            string path = x;
            string zonename = y;
            string mapname = z;
            string fullpath = path + mapname + @"tile\";
            return fullpath;
        }
    }

    [Serializable]
    public class Tile
    {
        public GameObject tilehandle;
        public Renderer tilerenderer;
        public string type;
        public Zone zone;
        public Vector3 relpos;
        public bool interactable;
        public bool reinforced;
        public int hp;
        public int armor;
        public Vector3 worldpos;
        public Pion Pion;
        public ZoneEffet zoneEffet;

        public Tile(string typetile, Zone z, GameObject hand, float i, float y, float x)
        {
            
            type = typetile;
            zone = z;
            tilehandle = hand;
            relpos = new Vector3(i, y, x);
            tilehandle.transform.parent = zone.zonepoint.transform;
            tilehandle.transform.localPosition = relpos;
            tilerenderer = tilehandle.GetComponent<Renderer>();
        }

        public List<Tile> getsouroundingTiles()
        {
            List<Tile> temp = new List<Tile>();
            for (int x = (int)this.worldpos.x - 1; x <= (int)this.worldpos.x + 1; x++) 
            {
                    for (int y = (int)this.worldpos.z-1; y <= (int)this.worldpos.z+1; y++) 
                    {
                        if (x >= 0 && y >= 0 && x < zone.map.tileMap.Count && y < zone.map.tileMap[1].Count) 
                        {
                            if(x!=(int)this.worldpos.x || y!=(int)this.worldpos.z)
                            {
                                temp.Add(zone.map.tileMap[x][y]);
                            }
                        }
                    }
            }
            return temp;
        }

        void Awake()
        {
            
        }
    }

    [Serializable]
    public class Pion
    {
        private int posX;
        private int posY;
        private bool estInvoque;
        private bool estVisible;
        private int sens;
        private Creature creature;
        private bool estHero = false;
        public Vector2 worldpos;
        public string type;

        public Pion pions(string t, bool estVisible, int x, int y)
        {
            estInvoque = true;
            this.estVisible = estVisible;
            type = t;
            posX = x;
            posY = y;
            return this;
        }

        public void setEstHero(bool estHero)
        {
            this.estHero = estHero;
        }

        public bool getEstHero()
        {
            return this.estHero;
        }

        public void setPosX(int x)
        {
            this.posX = x;
        }

        public int getPosX()
        {
            return this.posX;
        }

        public void setPosY(int y)
        {
            this.posY = y;
        }

        public int getPosY()
        {
            return this.posY;
        }

        public void setCreature(Creature creature)
        {
            this.creature = creature;
        }

        public Creature getCreature()
        {
            return this.creature;
        }

        public void setEstInvoque(bool b)
        {
            this.estInvoque = b;
        }

        public void setEstVisible(bool b)
        {
            this.estVisible = b;
        }

        public bool getEstVisible()
        {
            return this.estVisible;
        }

        public bool getEstInvoque()
        {
            return this.estInvoque;
        }

        public void setSens(int sens)
        {
            this.sens = sens;
        }

        public int getSens()
        {
            return this.sens;
        }

        
        
    }
}
