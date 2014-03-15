using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;
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

        public bool OverGUI { get { return this.overGUI; } set { this.overGUI = value; } }
        public bool Canend { get { return this.canend; } set { this.canend = value; } }
        public bool Ended { get { return this.ended; } set { this.ended = value; } }

    }

    [Serializable]
    public class Map
    {


        private string mapName;
        public static string mapsFilesPath = Application.dataPath + @"/Textures/Maps/";
        private string mapPath;
        private string mapFilePath;
        private List<List<Zone>> zoneMap;
        private List<List<System.Drawing.Color>> colorMap;
        public static List<List<Tuile>> tuileMap;
        private List<Tuile> ligne;
        private Environnement environement;
        private int nbX;
        private int nbY;


        public Map()
        {
            colorMap = new List<List<System.Drawing.Color>>();
            tuileMap = new List<List<Tuile>>();
        }

        public string getCheminMap(string x)
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
            TuileType typeNord = tuileNord.Type;
            TuileType typeSud = tuileSud.Type;
            TuileType typeEst = tuileEst.Type;
            TuileType typeOuest = tuileOuest.Type;
            /*
            Console.WriteLine("Au nord il y a : une tuile type " + typeNord);
            Console.WriteLine("Au sud il y a : une tuile type " + typeSud);
            Console.WriteLine("A l'est il y a : une tuile type " + typeEst);
            Console.WriteLine("A l'ouest il y a : une tuile type " + typeOuest);
             * */
            if (tuileNord.Pion != null)
            {
                pionAdjacent.Add(tuileNord.Pion);
            }
            if (tuileSud.Pion != null)
            {
                pionAdjacent.Add(tuileSud.Pion);
            }
            if (tuileEst.Pion != null)
            {
                pionAdjacent.Add(tuileEst.Pion);
            }
            if (tuileOuest.Pion != null)
            {
                pionAdjacent.Add(tuileOuest.Pion);
            }

            return pionAdjacent;

        }//FIN ADJACENT

        public void attaquer(int direction, Pion pion)
        {
            int x = pion.PosX;
            int y = pion.PosY;
            if (direction == 1)
            {
                tuileMap[y - 1][x] = new Tuile(x, y - 1);
                tuileMap[y - 1][x].Type = TuileType.vide;
            }
            else if (direction == 2)
            {
                tuileMap[y + 1][x] = new Tuile(x, y + 1);
                tuileMap[y + 1][x].Type = TuileType.vide;
            }
            else if (direction == 3)
            {
                tuileMap[y][x + 1] = new Tuile(x + 1, y);
                tuileMap[y][x + 1].Type = TuileType.vide;
            }
            else if (direction == 4)
            {
                tuileMap[y][x - 1] = new Tuile(x - 1, y);
                tuileMap[y][x - 1].Type = TuileType.vide;
            }

        }//FIN ATTAQUER


        public int NbY { get { return this.nbY; } set { this.nbY = value; } }
        public int NbX { get { return this.nbX; } set { this.nbX = value; } }
        public Environnement Environnement { get { return this.environement; } set { this.environement = value; } }
        public List<Tuile> Ligne { get { return this.ligne; } set { this.ligne = value; } }
        public List<List<System.Drawing.Color>> ColorMap { get { return this.colorMap; } set { this.colorMap = value; } }
        public List<List<Zone>> ZoneMap { get { return this.zoneMap; } set { this.zoneMap = value; } }
        public string MapFilePath { get { return this.mapFilePath; } set { this.mapFilePath = value; } }
        public string MapPath { get { return this.mapPath; } set { this.mapPath = value; } }
        public string MapName { get { return this.mapName; } set { this.mapName = value; } }


    }

    public enum TuileType
    {
        vide,
        normal,
        colonne,
        tombeau,
        mur
    }

    [Serializable]
    public class Tuile
    {
        private GameObject tuileObject;
        private GameObject tuileFow;
        private TuileType type;
        private Zone zone;
        private Vector3 realPosition;
        private bool interactable;
        private bool reinforced;
        private int hp;
        private int armor;
        private Vector3 worldPosition;
        private Pion pion;
        private int posX, posY;

        public List<Tuile> getTuileAutour()
        {
            List<Tuile> temp = new List<Tuile>();
            for (int x = (int)this.posX - 1; x <= (int)this.posX + 1; x++)
            {
                for (int y = (int)this.posY - 1; y <= (int)this.posY + 1; y++)
                {
                    if (x >= 0 && y >= 0 && x < Map.tuileMap.Count && y < Map.tuileMap[1].Count)
                    {
                        if (x != (int)this.posX || y != (int)this.posY)
                        {
                            temp.Add(Map.tuileMap[x][y]);
                        }
                    }
                }
            }
            return temp;
        }

        public Tuile(int x, int y)
        {
            this.posX = x;
            this.posY = y;
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

        public void supprimerPion()
        {
            this.pion = null;
        }


        public GameObject TuileObject { get { return this.tuileObject; } set { this.tuileObject = value; } }
        public GameObject TuileFow { get { return this.tuileFow; } set { this.tuileFow = value; } }
        public TuileType Type { get { return this.type; } set { this.type = value; } }
        public Zone Zone { get { return this.zone; } set { this.zone = value; } }
        public Vector3 RealPosition { get { return this.realPosition; } set { this.realPosition = value; } }
        public bool Interactable { get { return this.interactable; } set { this.interactable = value; } }
        public bool Reinforced { get { return this.reinforced; } set { this.reinforced = value; } }
        public int Hp { get { return this.hp; } set { this.hp = value; } }
        public int Armor { get { return this.armor; } set { this.armor = value; } }
        public Vector3 WorldPosition { get { return this.worldPosition; } set { this.worldPosition = value; } }
        public Pion Pion { get { return this.pion; } set { this.pion = value; } }
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
        private bool deplacer;
        private bool attaquer;
        private Entite entite;
        private bool estHero = false;
        private GameObject pionObject;
        private Direction direction;

        public Pion(bool estVisible)
        {
            estInvoque = true;
            this.estVisible = estVisible;
        }

        public Pion()
        {
            estInvoque = true;
        }

        public int PosX { get { return this.posX; } set { this.posX = value; } }
        public int PosY { get { return this.posY; } set { this.posY = value; } }
        public bool EstInvoque { get { return this.estInvoque; } set { this.estInvoque = value; } }
        public bool EstVisible { get { return this.estVisible; } set { this.estVisible = value; } }
        public Entite Entite { get { return this.entite; } set { this.entite = value; } }
        public Direction Direction { get { return this.direction; } set { this.direction = value; } }
        public GameObject PionObject { get { return this.pionObject; } set { this.pionObject = value; } }


    }//PION


    [Serializable]
    public class Tombeau
    {
        /*la position du tombeau depend de la crypte */
        private Tuile[] tuile;

        public Tombeau() { }

        /*ce constructeur vient avec la crypte*/
        public Tombeau(Tuile tuilea, Tuile tuileb)
        {
            tuile = new Tuile[2];
            tuile[0] = tuilea;
            tuile[1] = tuileb; 
        }

        public Tuile[] Tuile { get { return this.tuile; } set { this.tuile = value; } }


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

    public enum Direction
    {
        nord = 1,
        sud = 2,
        est = 3,
        ouest = 4
    }

    [Serializable]
    public class Crypte
    {
        Vector2 pointDepart;
        Direction direction;
        private Tombeau tombeau;

        //La dimension de la crypte doit etre imposé avant
        public Crypte(Vector2 pointDepart, Direction direction, Tombeau tombeau)           
        {
            this.pointDepart = pointDepart;
            this.direction = direction;
            this.tombeau = tombeau;
        }

        /*suivant la direction choisit on deplace le tout d'une case; on vérifie qu'aucune case ne touche un mur*/
        public void deplacerCrypte(Direction position, Direction direction)
        {
            List<Tuile> ligne = new List<Tuile>();
            int pointX = (int)(pointDepart.x);
            int pointY = (int)(pointDepart.y);
            int x = 0;
            int y = 0;
            int z = 0;

            Debug.Log("pointY : " + pointY + " == pointX " + pointX);
            if (position == Direction.nord)
            {
                pointX = pointX - 1;
                if (direction == Direction.ouest)
                {
                    x = -1;
                }
                else if (direction == Direction.est)
                {
                    x = 1;
                }
            }
            else if (position == Direction.sud)
            {
                if (direction == Direction.ouest)
                {
                    x = 1;
                }
                else if (direction == Direction.est)
                {
                    x = -1;
                }
            }
            else if (position == Direction.est)
            {
                if (direction == Direction.nord)
                {
                    z = 1;
                }
                else if(direction == Direction.sud)
                {
                    z = -1;
                }
            }
            else if (position == Direction.ouest)
            {
                if (direction == Direction.nord)
                {
                    z = -1;
                }
                else if (direction == Direction.sud)
                {
                    z = 1;
                }
            }

            for (int i = pointY; i < (pointY - 16); i++)
            {
                ligne = Map.tuileMap[i];
                for (int j = pointX; j < (pointX - 16); j++)
                {
                    ligne[j].TuileObject.transform.localPosition = new Vector3(ligne[j].TuileObject.transform.localPosition.x + x, ligne[j].TuileObject.transform.localPosition.y + y, ligne[j].TuileObject.transform.localPosition.z + z);
                }
            }
        }

        public Tombeau Tombeau { get { return this.tombeau; } set { this.tombeau = value; } }
        public Vector2 PointDepart { get { return this.pointDepart; } set { this.pointDepart = value; } }
        public Direction Direction { get { return this.direction; } set { this.direction = value; } }


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
