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
    }

    [Serializable]
    public class Map
    {


        private string mapName;
        private static string mapsFilesPath = Application.dataPath + @"\Textures\Maps\";
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

        public string getZoneDefTXT(string x)
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
            int x = pion.getPosX();
            int y = pion.getPosY();
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

        }//FIN ATTAQUER


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
            tuileHandle.transform.parent = zone.getZonePoint().transform;
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

        public void setType(string type)
        {
            this.type = type;
        }

        public string getType()
        {
            return this.type;
        }

        public void setPosY(int y)
        {
            this.posY = y;
        }

        public int getPosY()
        {
            return this.posY;
        }

        public void setPosX(int x)
        {
            this.posX = x;
        }

        public int getPosX()
        {
            return this.posX;
        }
        public void setPion(Pion pion)
        {
            this.pion = pion;
        }

        public Pion getPion()
        {
            return this.pion;
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
        private Vector2 worldpos;
        private string type;

        public Pion pions(bool estVisible)
        {
            estInvoque = true;
            this.estVisible = estVisible;
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

    }

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



        public string getzoneimagepath(string x, string y, string z)
        {
            string path = x;
            string zonename = y;
            string mapname = z;
            string fullpath = path + mapname + @"tile\";
            return fullpath;
        }

        public GameObject getZonePoint()
        {
            return this.zonePoint;
        }

    }

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
                Console.WriteLine("La crypte s'est déplacé vers le nord");
            }
            else if (direction == 2)
            {
                Console.WriteLine("La crypte s'est déplacé vers le sud");
            }
            else if (direction == 3)
            {
                Console.WriteLine("La crypte s'est déplacé vers l'est");
            }
            else if (direction == 4)
            {
                Console.WriteLine("La crypte s'est déplacé vers l'ouest");
            }
        }

        public Tombeau getTombeau()
        {
            return this.tombeau;
        }


    }

    [Serializable]
    public class Arene : Zone
    {
        public Arene(string type, GameObject gameObject)
            : base(type, gameObject)
        {
        }
    }
}
