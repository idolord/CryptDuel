using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Worldbuilder;

public class Init : MonoBehaviour
{

    public int nombreTuile = 0;
    // Use this for initialization
    void Start()
    {

        
        //instatiating mainmenu.
        //GameObject startmenu = Instantiate(Resources.Load("ui/menuGO")) as GameObject;
        GameObject cam = new GameObject();
        cam.AddComponent<Camera>();
        cam.gameObject.AddComponent<camera>();
        //instantiating environement
        Environnement environnement = new Environnement();
        
        //instanciating map with it's parameters
        Map map = new Map("1v1", environnement);
        //Debug.Log(map.mapname);

        //getting global map path
        string mapfilepath = map.getCheminMap(map.MapName);
        //Debug.Log(mapfilepath);

        //getting map full path
        map.MapFilePath = mapfilepath;
        //Debug.Log(map.mapfilepath);


        // checking if file exist
        if (map.MapFilePath == "error")
        {
            Debug.LogError("error occured opening map file");
        }
        else
        {
            //Debug.Log("path ok");
            
            string zoneNom;
            string[] mapParts = new string[100];
            //populating map.ZoneColorMap acording to image
            map.ZoneColorMap = Tools.getColor(map.MapFilePath);
            string textfile = map.getZoneDefTXT(map.MapName);
            //grabing zone references 
            mapParts = Tools.getMapZones(textfile);
            map.ZoneMap = new List<List<Zone>>();
            List<Zone> row = new List<Zone>();

            //instanciating every zone + adding "border" zones (not interactive)
            for (int i = 0; i < (map.ZoneColorMap.Count) + 2; i++)
            {
                GameObject zonepos = Instantiate(Resources.Load("map/zonepointer")) as GameObject;
                zonepos.transform.position = new Vector3((i * 16) - 16, 0, - 16);
                Zone zone = new Zone("border", zonepos);
                row.Add(zone);
            }
            map.ZoneMap.Add(row);
            for (int i = 0; i < map.ZoneColorMap.Count; i++)
            {
                if (map.ZoneColorMap.Count > 1)
                {
                    row = new List<Zone>();
                    {
                        GameObject zonePos = Instantiate(Resources.Load("map/zonepointer")) as GameObject;
                        zonePos.transform.position = new Vector3(-16, 0, i * 16);
                        Zone zone = new Zone("border", zonePos);
                        row.Add(zone);
                    }
                    for (int j = 0; j < map.ZoneColorMap[i].Count; j++)
                    {
                        //Debug.Log("instanciating zones");
                        {
                            GameObject zonepos = Instantiate(Resources.Load("map/zonepointer")) as GameObject;
                            zonepos.transform.position = new Vector3(i * 16, 0, j * 16);
                            if (map.ZoneColorMap[i][j].A == 255)
                            {
                                string tempcolor = Tools.colorToString(map.ZoneColorMap[i][j]);
                                zoneNom = Tools.getNomZone(mapParts, tempcolor);
                                Zone zone = new Zone(zoneNom, zonepos);
                                row.Add(zone);
                            }
                            else
                            {
                                Zone zone = new Zone("empty", zonepos);
                                row.Add(zone);
                            }
                        }
                    }
                    {
                        GameObject zonepos = Instantiate(Resources.Load("map/zonepointer")) as GameObject;
                        zonepos.transform.position = new Vector3(map.ZoneColorMap.Count * 16, 0, i * 16);
                        Zone zone = new Zone("border", zonepos);
                        row.Add(zone);
                    }
                    map.ZoneMap.Add(row);
                }
                else
                {
                    row = new List<Zone>();
                    for (int j = 0; j < map.ZoneColorMap[i].Count; j++)
                    {
                        {
                            GameObject zonePos = Instantiate(Resources.Load("map/zonepointer")) as GameObject;
                            zonePos.transform.position = new Vector3((i*16)-16, 0, j * 16);
                            Zone zone = new Zone("border", zonePos);
                            row.Add(zone);
                        }
                        //Debug.Log("instanciating zones");
                        {
                            GameObject zonepos = Instantiate(Resources.Load("map/zonepointer")) as GameObject;
                            zonepos.transform.position = new Vector3(i * 16, 0, j * 16);
                            if (map.ZoneColorMap[i][j].A == 255)
                            {
                                string tempcolor = Tools.colorToString(map.ZoneColorMap[i][j]);
                                zoneNom = Tools.getNomZone(mapParts, tempcolor);
                                Zone zone = new Zone(zoneNom, zonepos);
                                row.Add(zone);
                            }
                            else
                            {
                                Zone zone = new Zone("empty", zonepos);
                                row.Add(zone);
                            }
                        }
                        {
                            GameObject zonepos = Instantiate(Resources.Load("map/zonepointer")) as GameObject;
                            zonepos.transform.position = new Vector3(map.ZoneColorMap.Count * 16, 0, j * 16);
                            Zone zone = new Zone("border", zonepos);
                            row.Add(zone);
                        }
                    }
                    map.ZoneMap.Add(row);
                }
            }
            row = new List<Zone>();
            for (int o = 0; o < (map.ZoneColorMap.Count) + 2; o++)
            {
                GameObject zonepos = Instantiate(Resources.Load("map/zonepointer")) as GameObject;
                zonepos.transform.position = new Vector3(((o * 16) - 16), 0, (map.ZoneColorMap[0].Count * 16));
                Zone zone = new Zone("border", zonepos);
                row.Add(zone);
            }
            map.ZoneMap.Add(row);
        }

        cam.transform.position = new Vector3(((map.ZoneMap.Count / 2) * 16) - 8, 15, ((map.ZoneMap[1].Count / 2) * 16) - 8);

        //populating every zone Tiles
        //Debug.Log("populating empty tile");
        for (int i = 0; i < map.ZoneMap.Count; i++)
        {
            for (int j = 0; j < map.ZoneMap[i].Count; j++)
            {
                if (map.ZoneMap[i][j].Type == "empty")
                {
                    for (int k = 0; k < 16; k++)
                    {
                        List<Tuile> tilerow = new List<Tuile>();
                        for (int l = 0; l < 16; l++)
                        {
                            GameObject tileHolder = Instantiate(Resources.Load("tile/TileHolder")) as GameObject;
                            Renderer rnd = tileHolder.GetComponent<Renderer>();
                            Tuile tile = new Tuile("empty", map.ZoneMap[i][j], tileHolder, l, 0, k);
                            rnd.material = tileHolder.GetComponent<TileHolder>().mats[0];
                            tileHolder.GetComponent<TileHolder>().tile = tile;
                            tileHolder.tag = "empty";
                            tile.WorldPosition = new Vector3((k + (16 * j)), 0, l + (16 * i));
                            tilerow.Add(tile);
                        }
                        Map.tuileMap.Add(tilerow);
                    }
                }
                else if (map.ZoneMap[i][j].Type == "border")
                {
                    for (int k = 0; k < 16; k++)
                    {
                        List<Tuile> tilerow = new List<Tuile>();
                        for (int l = 0; l < 16; l++)
                        {
                            GameObject tileHolder = Instantiate(Resources.Load("tile/TileHolder")) as GameObject;
                            Renderer rnd = tileHolder.GetComponent<Renderer>();
                            Tuile tile = new Tuile("border", map.ZoneMap[i][j], tileHolder, l, 0, k);
                            rnd.material = tileHolder.GetComponent<TileHolder>().mats[0];
                            tileHolder.GetComponent<TileHolder>().tile = tile;
                            tileHolder.tag = "border";
                            tile.WorldPosition = new Vector3((k + (16 * i)), 0, l + (16 * j));
                            tilerow.Add(tile);
                        }
                        Map.tuileMap.Add(tilerow);
                    }
                }
                else
                {
                    //Debug.Log("grabbing file directory");
                    string imagepath = map.ZoneMap[i][j].getzoneimagepath(map.ZoneMap[i][j].Type, map.MapName);
                    //Debug.Log(imagepath);

                    //Debug.Log("grabbing tile color list");
                    Debug.Log("imagepath : " + imagepath);
                    Debug.Log("zonemap type : " + map.ZoneMap[i][j].Type);
                    List<List<System.Drawing.Color>> tilescolor = Tools.getColor(imagepath + map.ZoneMap[i][j].Type + ".png");



                    //Debug.Log("grabbing zone tile definitions");
                    string[] tilref = Tools.getMapZones(imagepath + "Tiles.txt");


                    for (int k = 0; k < 16; k++)
                    {
                        List<Tuile> tilerow = new List<Tuile>();
                        for (int l = 0; l < 16; l++)
                        {
                            GameObject tileHolder = Instantiate(Resources.Load("tile/TileHolder")) as GameObject;
                            Renderer rnd = tileHolder.GetComponent<Renderer>();

                            if (tilescolor[l][k].A == 255)
                            {
                                string tilemame = Tools.getNomZone(tilref, Tools.colorToString(tilescolor[l][k]));
                                if (tilemame == "openfloorTile")
                                {
                                    Tuile tile = new Tuile("openfloorTile", map.ZoneMap[i][j], tileHolder, l, -1, k);
                                    rnd.material = tileHolder.GetComponent<TileHolder>().mats[2];
                                    tileHolder.GetComponent<TileHolder>().tile = tile;
                                    tileHolder.tag = "floor";
                                    tile.WorldPosition = new Vector3((k + (16 * i)), 0, l + (16 * j));
                                    tilerow.Add(tile);
                                }
                                else if (tilemame == "wallTile")
                                {
                                    Tuile tile = new Tuile("wallTile", map.ZoneMap[i][j], tileHolder, l, 0, k);
                                    rnd.material = tileHolder.GetComponent<TileHolder>().mats[2];
                                    tileHolder.GetComponent<TileHolder>().tile = tile;
                                    tileHolder.tag = "wall";
                                    tile.WorldPosition = new Vector3((k + (16 * i)), 0, l + (16 * j));
                                    tilerow.Add(tile);

                                }
                                else if (tilemame == "collumn")
                                {
                                    Tuile tile = new Tuile("collumn", map.ZoneMap[i][j], tileHolder, l, 0, k);
                                    rnd.material = tileHolder.GetComponent<TileHolder>().mats[2];
                                    tileHolder.GetComponent<TileHolder>().tile = tile;
                                    tileHolder.tag = "wall";
                                    tile.WorldPosition = new Vector3((k + (16 * i)), 0, l + (16 * j));
                                    tilerow.Add(tile);
                                }
                            }
                            else
                            {
                                Tuile tile = new Tuile("empty", map.ZoneMap[i][j], tileHolder, l, 0, k);
                                rnd.material = tileHolder.GetComponent<TileHolder>().mats[0];
                                tileHolder.GetComponent<TileHolder>().tile = tile;
                                tileHolder.tag = "empty";
                                tile.WorldPosition = new Vector3((k + (16 * i)), 0, l + (16 * j));
                                tilerow.Add(tile);
                            }
                        }
                        Map.tuileMap.Add(tilerow);
                        
                    }
                }
            }
        }
        //generating map global tilemap
        map.GenererTuileMap();
        //updating tiles according to surounding
       // map.tileCheckForEmptyToWall();
       
        
    }


    // Update is called once per frame
    void Update()
    {
        nombreTuile = Map.tuileMap.Count;
    }
}

