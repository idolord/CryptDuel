using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Worldbuilder;

public class init : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {
        //instatiating mainmenu.
        //GameObject startmenu = Instantiate(Resources.Load("ui/menuGO")) as GameObject;
        GameObject cam = new GameObject();
        cam.AddComponent<Camera>();
        cam.gameObject.AddComponent<camera>();
        //instantiating environement
        env envi = new env();
        
        //instanciating map with it's parameters
        map map = new map("entrance", envi);
        //Debug.Log(map.mapname);

        //getting global map path
        string mapfilepath = map.getmappath(map.mapname);
        //Debug.Log(mapfilepath);

        //getting map full path
        map.mapfilepath = mapfilepath;
        //Debug.Log(map.mapfilepath);


        // checking if file exist
        if (map.mapfilepath == "error")
        {
            Debug.LogError("error occured opening map file");
        }
        else
        {
            //Debug.Log("path ok");

            string zonename;
            string[] mapparts = new string[100];
            //populating map.tempzone acording to image
            map.tempzone = tools.getcolor(map.mapfilepath);
            string textfile = map.getzonedeftxt(map.mapname);
            //grabing zone references 
            mapparts = tools.getMapZones(textfile);
            map.zonemap = new List<List<Zone>>();
            List<Zone> row = new List<Zone>();

            //instanciating every zone + adding "border" zones (not interactive)
            for (int i = 0; i < (map.tempzone.Count) + 2; i++)
            {
                GameObject zonepos = Instantiate(Resources.Load("map/zonepointer")) as GameObject;
                zonepos.transform.position = new Vector3(((i * 16) - 16), 0, -16);
                Zone zone = new Zone("border", zonepos, map);
                row.Add(zone);
            }
            map.zonemap.Add(row);
            for (int i = 0; i < map.tempzone.Count; i++)
            {
                row = new List<Zone>();

                if (true)
                {
                    GameObject zonepos = Instantiate(Resources.Load("map/zonepointer")) as GameObject;
                    zonepos.transform.position = new Vector3(-16 , 0, i*16);
                    Zone zone = new Zone("border", zonepos, map);
                    row.Add(zone);
                }
                for (int j = 0; j < map.tempzone[i].Count; j++)
                {
                    //Debug.Log("instanciating zones");

                    GameObject zonepos = Instantiate(Resources.Load("map/zonepointer")) as GameObject;
                    zonepos.transform.position = new Vector3(j * 16, 0, i * 16);
                    if (map.tempzone[j][i].A == 255)
                    {
                        string tempcolor = tools.colortostring(map.tempzone[j][i]);
                        zonename = tools.getzonename(mapparts, tempcolor);
                        Zone zone = new Zone(zonename, zonepos, map);
                        row.Add(zone);
                    }
                    else
                    {
                        Zone zone = new Zone("empty", zonepos, map);
                        row.Add(zone);
                    }
                }
                if (true)
                {
                    GameObject zonepos = Instantiate(Resources.Load("map/zonepointer")) as GameObject;
                    zonepos.transform.position = new Vector3((map.tempzone.Count)*16, 0, i * 16);
                    Zone zone = new Zone("border", zonepos, map);
                    row.Add(zone);
                }
                map.zonemap.Add(row);
            }
            row = new List<Zone>();
            for (int o = 0; o < (map.tempzone.Count) + 2; o++)
            {
                GameObject zonepos = Instantiate(Resources.Load("map/zonepointer")) as GameObject;
                zonepos.transform.position = new Vector3(((o * 16) - 16), 0, (map.tempzone.Count * 16));
                Zone zone = new Zone("border", zonepos, map);
                row.Add(zone);
            }
            map.zonemap.Add(row);
        }

        cam.transform.position = new Vector3(((map.zonemap.Count/2)*16)-8,15,((map.zonemap[1].Count/2)*16)-8);

        //populating every zone Tiles
        //Debug.Log("populating empty tile");
        for (int i = 0; i < map.zonemap.Count; i++)
        {
            for (int j = 0; j < map.zonemap[i].Count; j++)
            {
                if (map.zonemap[j][i].type == "empty")
                {
                    for (int k = 0; k < 16; k++)
                    {
                        List<Tile> tilerow = new List<Tile>();
                        for (int l = 0; l < 16; l++)
                        {
                            GameObject tileholder = Instantiate(Resources.Load("tile/Tileholder")) as GameObject;
                            Renderer rnd = tileholder.GetComponent<Renderer>();
                            Tile tile = new Tile("empty", map.zonemap[j][i], tileholder, l, 0, k);
                            rnd.material = tileholder.GetComponent<Tileholder>().mats[0];
                            tileholder.GetComponent<Tileholder>().tile = tile;
                            tileholder.tag = "empty";
                            tile.worldpos = new Vector3((k+(16*j)),0,l+(16*i));
                            tilerow.Add(tile);
                        }
                        map.zonemap[j][i].tilemap.Add(tilerow);
                    }
                }
                else if (map.zonemap[j][i].type == "border")
                {
                    for (int k = 0; k < 16; k++)
                    {
                        List<Tile> tilerow = new List<Tile>();
                        for (int l = 0; l < 16; l++)
                        {
                            GameObject tileholder = Instantiate(Resources.Load("tile/Tileholder")) as GameObject;
                            Renderer rnd = tileholder.GetComponent<Renderer>();
                            Tile tile = new Tile("border", map.zonemap[j][i], tileholder, l, 0, k);
                            rnd.material = tileholder.GetComponent<Tileholder>().mats[0];
                            tileholder.GetComponent<Tileholder>().tile = tile;
                            tileholder.tag = "border";
                            tile.worldpos = new Vector3((k + (16 * j)), 0, l + (16 * i));
                            tilerow.Add(tile);
                        }
                        map.zonemap[j][i].tilemap.Add(tilerow);
                    }
                }
                else
                {
                    //Debug.Log("grabbing file directory");
                    string imagepath = map.zonemap[j][i].getzoneimagepath(map.mapsfilespath, map.zonemap[j][i].type, map.mapname);
                    //Debug.Log(imagepath);

                    //Debug.Log("grabbing tile color list");
                    List<List<System.Drawing.Color>> tilescolor = tools.getcolor(imagepath + map.zonemap[j][i].type + ".png");



                    //Debug.Log("grabbing zone tile definitions");
                    string[] tilref = tools.getMapZones(imagepath + "Tiles.txt");


                    for (int k = 0; k < 16; k++)
                    {
                        List<Tile> tilerow = new List<Tile>();
                        for (int l = 0; l < 16; l++)
                        {
                            GameObject tileholder = Instantiate(Resources.Load("tile/Tileholder")) as GameObject;
                            Renderer rnd = tileholder.GetComponent<Renderer>();

                            if (tilescolor[l][k].A == 255)
                            {
                                string tilemame = tools.getzonename(tilref, tools.colortostring(tilescolor[l][k]));
                                if (tilemame == "openfloorTile")
                                {
                                    Tile tile = new Tile("openfloorTile", map.zonemap[j][i], tileholder, l, -1, k);
                                    rnd.material = tileholder.GetComponent<Tileholder>().mats[2];
                                    tileholder.GetComponent<Tileholder>().tile = tile;
                                    tileholder.tag = "floor";
                                    tile.worldpos = new Vector3((k + (16 * j)), 0, l + (16 * i));
                                    tilerow.Add(tile);
                                }
                                else if (tilemame == "wallTile")
                                {
                                    Tile tile = new Tile("wallTile", map.zonemap[j][i], tileholder, l, 0, k);
                                    rnd.material = tileholder.GetComponent<Tileholder>().mats[2];
                                    tileholder.GetComponent<Tileholder>().tile = tile;
                                    tileholder.tag = "wall";
                                    tile.worldpos = new Vector3((k + (16 * j)), 0, l + (16 * i));
                                    tilerow.Add(tile);

                                }
                                else if (tilemame == "collumn")
                                {
                                    Tile tile = new Tile("collumn", map.zonemap[j][i], tileholder, l, 0, k);
                                    rnd.material = tileholder.GetComponent<Tileholder>().mats[2];
                                    tileholder.GetComponent<Tileholder>().tile = tile;
                                    tileholder.tag = "wall";
                                    tile.worldpos = new Vector3((k + (16 * j)), 0, l + (16 * i));
                                    tilerow.Add(tile);
                                }
                            }
                            else
                            {
                                Tile tile = new Tile("empty", map.zonemap[j][i], tileholder, l, 0, k);
                                rnd.material = tileholder.GetComponent<Tileholder>().mats[0];
                                tileholder.GetComponent<Tileholder>().tile = tile;
                                tileholder.tag = "empty";
                                tile.worldpos = new Vector3((k + (16 * j)), 0, l + (16 * i));
                                tilerow.Add(tile);
                            }
                        }
                        map.zonemap[j][i].tilemap.Add(tilerow);
                        
                    }
                }
            }
        }
        //generating map global tilemap
        map.generatemaptilemap();
        //updating tiles according to surounding
        map.tileCheckForEmptyToWall();
       
        
    }


    // Update is called once per frame
    void Update()
    {
     
    }
}

