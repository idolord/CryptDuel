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


        public string mapname;
        public static string mapsfilespath = Application.dataPath+@"\Textures\Maps\";
        public string mappath;
        public string mapfilepath;
        public List<List<Zone>> zonemap;
        public List<List<System.Drawing.Color>> tempzone;
        public List<List<Tile>> tilemap;
        public env environement;



        public map(string x, env y)
        {
            mapname = x;
            environement = y;
            zonemap = new List<List<Zone>>();
            tempzone = new List<List<System.Drawing.Color>>();
            tilemap = new List<List<Tile>>();
        }

        public string getmappath (string x)
        {
            mapname = x;
            mappath = mapsfilespath + mapname + @"\";
            if (Directory.Exists(mappath))
            {
                mapfilepath = mappath + mapname + ".png";
                if (File.Exists(mapfilepath))
                {
                    //Debug.Log("found repertory " + mapname);
                    return mapfilepath;
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
            mapname = x;
            mappath = mapsfilespath + mapname + @"\";
            if (Directory.Exists(mappath))
            {
                mapfilepath = mappath + mapname + ".txt";
                if (File.Exists(mapfilepath))
                {
                    //Debug.Log("file found " + mapname + ".txt.");
                    return mapfilepath;
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
            foreach (List<Zone> list in this.zonemap)
            {
                for (int i = 0; i < 16; i++)
                {
                    this.tilemap.Add(this.gettilemaprow(i, list));
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
            for (int i = 0; i < tilemap.Count; i++)
            {
                for (int j = 0; j < tilemap[i].Count; j++)
                {
                    if (tilemap[i][j].type == "openfloorTile")
                    {
                        List<Tile> temp = tilemap[i][j].getsouroundingTiles();
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
                        if (x >= 0 && y >= 0 && x < zone.map.tilemap.Count && y < zone.map.tilemap[1].Count) 
                        {
                            if(x!=(int)this.worldpos.x || y!=(int)this.worldpos.z)
                            {
                                temp.Add(zone.map.tilemap[x][y]);
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
}
