using UnityEngine;
using System.Collections;
using Worldbuilder;
using System;

[Serializable]
public class Tileholder : MonoBehaviour
{

    // Use this for initialization
    public Material[] mats;
    public Material tempmaterial;
    public Material pasmaterial;
    public Tile tile;
    public GameObject bosc;

    void OnMouseOver()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if (tile.type == "empty")
            {
                int index = 1;
                tile.tilerenderer.material = mats[index];
            }
            else if ((tile.tilehandle.tag == "border") && (tile.type == "wallTile"))
            {
                int index = 1;
                tile.tilerenderer.material = mats[index];
            }
            else if ((tile.tilehandle.tag == "border") && (tile.type == "border"))
            {

            }
            else
            {
                int index = 3;
                tile.tilerenderer.material = mats[index];
                if (tag == "wall")
                {
                    if (Input.GetMouseButton(0))
                    {
                        tile.type = "openfloorTile";
                        transform.position = new Vector3(transform.position.x, -1, transform.position.z);
                        gameObject.tag = "floor";
                        tile.zone.map.tileCheckForEmptyToWall();
                    }
                }
            } 
            
        }
        else
        {

            if ((tile.type == "empty")||(tile.type =="border"))
            {
                int index = 0;
                tile.tilerenderer.material = mats[index];
            }
            else if ((tile.type != "empty"))
            {
                int index = 2;
                tile.tilerenderer.material = mats[index];
            }
        }
    }

    void OnMouseExit()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if (tile.type == "empty")
            {
                int index = 0;
                tile.tilerenderer.material = mats[index];
            }
            else if ((tile.type == "border") && (tile.tilehandle.tag == "border"))
            {
                int index = 0;
                tile.tilerenderer.material = mats[index];
            }
            else if ((tile.type == "wallTile") && (tile.tilehandle.tag == "border"))
            {
                int index = 2;
                tile.tilerenderer.material = mats[index];
            }
            else
            {
                int index = 2;
                tile.tilerenderer.material = mats[index];
            }
            
        }
    }

    void changetowall()
    {
        tile.type = "wallTile";
        tile.tilehandle.tag = "wall";
        tile.tilerenderer.material = mats[2];
        Destroy(bosc);
    }

    void changetowallborder()
    {
        tile.type = "wallTile";
        tile.tilehandle.tag = "border";
        tile.tilerenderer.material = mats[2];
        Destroy(bosc);
    }

    void Start()
    {
        if (tile.type == "empty")
        {
            this.bosc = (GameObject)Instantiate(Resources.Load("tile/obscurator"));
            bosc.transform.position = new Vector3(transform.position.x,transform.position.y+0.51f,transform.position.z);
        }
    }
}
