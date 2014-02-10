using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine;
using System.Linq;
using System.Net;
using System.Text;

namespace Worldbuilder
{
    [Serializable]
    public class Environnement
    {
        private bool overGUI;
        private bool canend;
        private bool ended;

        public Environnement()
        {
            canend = false;
            ended = false;
            overGUI = false;
        }
        void Update()
        {
            overGUI = false;
        }

        public bool OverGUI {get{ return this.overGUI;} set { this.overGUI = value;} }
        public bool Canend  {get{ return this.canend;} set { this.canend = value;}}
        public bool Ended   {get{ return this.ended;} set { this.ended = value;} }

    }

    [Serializable]
    public class Map
    {


        private string mapName;
        public static string mapsFilesPath = Application.dataPath + @"/Textures/Maps/";
        private string mapPath;
        private string mapFilePath;
        private List<List<Zone>> zoneMap;
        private List<List<System.Drawing.Color>> zoneColorMap;
        public static List<List<Tuile>> tuileMap;
        private List<Tuile> ligne;
        private Environnement environement;
        private int nbX;
        private int nbY;


        public Map(string x, Environnement y)
        {
            mapName = x;
            environement = y;
            zoneMap = new List<List<Zone>>();
            zoneColorMap = new List<List<System.Drawing.Color>>();
            tuileMap = new List<List<Tuile>>();
        }

        public string getCheminMap (string x)
        {
            mapName = x;
            mapPath = mapsFilesPath + mapName + @"/";
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

        public string getZoneDefTXT(string x)
        {
            mapName = x;
            mapPath = mapsFilesPath + mapName + @"/";
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

        public void GenererTuileMap()
        {
            foreach (List<Zone> list in this.zoneMap)
            {
                for (int i = 0; i < 16; i++)
                {
                    Map.tuileMap.Add(this.getTuileMapLigne(i));
                }
            }
        }

        public List<Tuile> getTuileMapLigne(int index)
        {
            List<Tuile> temp = new List<Tuile>();

                for (int j = 0; j < 16; j++)
                {
                    temp.Add(Map.tuileMap[index][j]);
                    //if (list[i].tilemap[index][j].type != "empty")
                    //{
                    //    //Debug.Log("adding tile to list " + list[i].tilemap[index][j].type);
                    //}
                }

            return temp;

        }

        public List<Pion> pionAdjacent(Pion pion)
        {
            int x = pion.PosX;
            int y = pion.PosY;
            List<Pion> pionAdjacent = new List<Pion>();
            Tuile tuileNord = tuileMap[y - 1][x];
            Tuile tuileSud = tuileMap[y + 1][x];
            Tuile tuileEst = tuileMap[y][x + 1];
            Tuile tuileOuest = tuileMap[y][x - 1];
            string typeNord = tuileNord.Type;
            string typeSud = tuileSud.Type;
            string typeEst = tuileEst.Type;
            string typeOuest = tuileOuest.Type;
            /*
            Console.WriteLine("Au nord il y a : une tuile type " + typeNord);
            Console.WriteLine("Au sud il y a : une tuile type " + typeSud);
            Console.WriteLine("A l'est il y a : une tuile type " + typeEst);
            Console.WriteLine("A l'ouest il y a : une tuile type " + typeOuest);
             * */
            if (typeNord == "pion")
            {
                pionAdjacent.Add(tuileNord.Pion);
            }
            if (typeSud == "pion")
            {
                pionAdjacent.Add(tuileSud.Pion);
            }
            if (typeEst == "pion")
            {
                pionAdjacent.Add(tuileEst.Pion);
            }
            if (typeOuest == "pion")
            {
                pionAdjacent.Add(tuileOuest.Pion);
            }

            Console.WriteLine("Pion adjacent : " + pionAdjacent.Count());

            return pionAdjacent;

        }//FIN ADJACENT

        public void attaquer(int direction, Pion pion)
        {
            int x = pion.PosX;
            int y = pion.PosY;
            Console.WriteLine("x : " + x + " y : " + y);
            if (direction == 1)
            {
                tuileMap[y - 1][x] = new Tuile(x, y - 1, "empty");
            }
            else if (direction == 2)
            {
                tuileMap[y + 1][x] = new Tuile(x, y + 1, "empty");
            }
            else if (direction == 3)
            {
                tuileMap[y][x + 1] = new Tuile(x + 1, y, "empty");
            }
            else if (direction == 4)
            {
                tuileMap[y][x - 1] = new Tuile(x - 1, y, "empty");
            }

        }//FIN ATTAQUER


        public int NbY { get { return this.nbY; } set { this.nbY = value; } }
        public int NbX { get { return this.nbX; } set { this.nbX = value; } }
        public Environnement Environnement { get { return this.environement; } set { this.environement = value; } }
        public List<Tuile> Ligne { get { return this.ligne; } set { this.ligne = value; } }
        public List<List<System.Drawing.Color>> ZoneColorMap { get { return this.zoneColorMap; } set { this.zoneColorMap = value; } }
        public List<List<Zone>> ZoneMap { get { return this.zoneMap; } set { this.zoneMap = value; } }
        public string MapFilePath { get { return this.mapFilePath; } set { this.mapFilePath = value; } }
        public string MapPath { get { return this.mapPath; } set { this.mapPath = value; } }
        public string MapName { get { return this.mapName; } set { this.mapName = value; } }


    }

    [Serializable]
    public class Tuile
    {
        private GameObject tuileHandle;
        private Renderer tuileRenderer;
        private string type;
        private Zone zone;
        private Vector3 realPosition;
        private bool interactable;
        private bool reinforced;
        private int hp;
        private int armor;
        private Vector3 worldPosition;
        private Pion pion;
        private ZoneEffet zoneEffet;
        private int posX, posY;

        public Tuile(string typetile, Zone z, GameObject hand, float i, float y, float x)
        {
            
            type = typetile;
            zone = z;
            tuileHandle = hand;
            realPosition = new Vector3(i, y, x);
            tuileHandle.transform.parent = zone.ZonePoint.transform;
            tuileHandle.transform.localPosition = realPosition;
            tuileRenderer = tuileHandle.GetComponent<Renderer>();
        }

        public List<Tuile> getTuileAutour()
        {
            List<Tuile> temp = new List<Tuile>();
            for (int x = (int)this.worldPosition.x - 1; x <= (int)this.worldPosition.x + 1; x++) 
            {
                for (int y = (int)this.worldPosition.z - 1; y <= (int)this.worldPosition.z + 1; y++) 
                    {
                        if (x >= 0 && y >= 0 && x < Map.tuileMap.Count && y < Map.tuileMap[1].Count) 
                        {
                            if (x != (int)this.worldPosition.x || y != (int)this.worldPosition.z)
                            {
                                temp.Add(Map.tuileMap[x][y]);
                            }
                        }
                    }
            }
            return temp;
        }

        public Tuile(string type)
        {
            this.type = type;
        }

        public Tuile(int x, int y, string type)
        {
            this.posX = x;
            this.posY = y;
            this.type = type;
        }

        public void ajouterPion(Pion unPion)
        {
            this.pion = unPion;
        }

        public void ajouterCoordonne(int x, int y)
        {
            posX = x;
            posY = y;
        }

        public void ajouterZoneEffet(ZoneEffet uneZoneEffet)
        {
            this.zoneEffet = uneZoneEffet;
        }

        public void supprimerZoneEffet()
        {
            this.zoneEffet = null;
        }

        public void supprimerPion()
        {
            this.pion = null;
        }


        public GameObject TuileHandle { get { return this.tuileHandle; } set { this.tuileHandle = value; } }
        public Renderer TuileRenderer { get { return this.tuileRenderer; } set { this.tuileRenderer = value; } }
        public string Type { get { return this.type; } set { this.type = value; } }
        public Zone Zone { get { return this.zone; } set { this.zone = value; } }
        public Vector3 RealPosition { get { return this.realPosition; } set { this.realPosition = value; } }
        public bool Interactable { get { return this.interactable; } set { this.interactable = value; } }
        public bool Reinforced { get { return this.reinforced; } set { this.reinforced = value; } }
        public int Hp { get { return this.hp; } set { this.hp = value; } }
        public int Armor { get { return this.armor; } set { this.armor = value; } }
        public Vector3 WorldPosition { get { return this.worldPosition; } set { this.worldPosition = value; } }
        public Pion Pion { get { return this.pion; } set { this.pion = value; } }
        public ZoneEffet ZoneEffet { get { return this.zoneEffet; } set { this.zoneEffet = value; } }
        public int PosX { get { return this.posX; } set { this.posX = value; } }
        public int PosY { get { return this.posY; } set { this.posY = value; } }


    }//TUILE


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
        private Vector2 worldpos;
        private string type;

        public Pion(bool estVisible)
        {
            estInvoque = true;
            this.estVisible = estVisible;
        }

        public int PosX { get { return this.posX; } set { this.posX = value; } }
        public int PosY { get { return this.posY; } set { this.posY = value; } }
        public bool EstInvoque { get { return this.estInvoque; } set { this.estInvoque = value; } }
        public bool EstVisible { get { return this.estVisible; } set { this.estVisible = value; } }
        public int Sens { get { return this.sens; } set { this.sens = value; } }
        public Creature Creature { get { return this.creature; } set { this.creature = value; } }
        public bool EstHero { get { return this.estHero; } set { this.estHero = value; } }
        public Vector2 Worldpos { get { return this.worldpos; } set { this.worldpos = value; } }
        public string Type { get { return this.type; } set { this.type = value; } }

                
    }//PION


    [Serializable]
    public class Tombeau
    {
        /*la position du tombeau depend de la crypte */
        private int posX;
        private int posY;

        public Tombeau() { }

        /*ce constructeur vient avec la crypte*/
        public Tombeau(int x, int y)
        {
            this.posX = x;
            this.posY = y;
        }

        public int PosX { get { return this.posX; } set { this.posX = value; } }
        public int PosY { get { return this.posY; } set { this.posY = value; } }


    }//TOMBEAU


    //zone family
    [Serializable]
    public class Zone
    {
        private string type;
        private GameObject zonePoint;

        public Zone(string x, GameObject y)
        {
            type = x;
            zonePoint = y;
        }



        public string getzoneimagepath(string y, string z)
        {
            string zonename = y;
            string mapname = z;
            string fullpath = Map.mapsFilesPath + mapname + @"tile\";
            return fullpath;
        }

        public string Type { get { return this.type; } set { this.type = value; } }
        public GameObject ZonePoint { get { return this.zonePoint; } set { this.zonePoint = value; } }

    }//ZONE


    [Serializable]
    public class Crypte : Zone
    {
        private Tombeau tombeau;
        //La dimension de la crypte doit etre imposé avant
        public Crypte(string type, GameObject gameObject)
            : base(type, gameObject)
        {
            tombeau = new Tombeau();
        }

        /*suivant la direction choisit on deplace le tout d'une case; on vérifie qu'aucune case ne touche un mur*/
        public void deplacerCrypte(int direction)
        {
            if (direction == 1)
            {
            }
            else if (direction == 2)
            {
            }
            else if (direction == 3)
            {
            }
            else if (direction == 4)
            {
            }
        }

        public Tombeau Tombeau { get { return this.tombeau; } set { this.tombeau = value; } }


    }//CRYPTE


    [Serializable]
    public class Arene : Zone
    {

        public Arene(string type, GameObject gameObject)
            : base(type, gameObject)
        {
        }

    }//ARENE
}
