using UnityEngine;
using System.Collections;
using Worldbuilder;
using System;

[Serializable]
public class Tileholder : MonoBehaviour
{

    // Use this for initialization
    public Material[] mats;
    public Material tempMaterial;
    public Material pasMaterial;
    public Tuile tile;
    public GameObject bosc;

    void OnMouseOver()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if (tile.Type == "empty")
            {
                int index = 1;
                tile.TuileRenderer.material = mats[index];
            }
            else if ((tile.TuileHandle.tag == "border") && (tile.Type == "wallTile"))
            {
                int index = 1;
                tile.TuileRenderer.material = mats[index];
            }
            else if ((tile.TuileHandle.tag == "border") && (tile.Type == "border"))
            {

            }
            else
            {
                int index = 3;
                tile.TuileRenderer.material = mats[index];
                if (tag == "wall")
                {
                    if (Input.GetMouseButton(0))
                    {
                        tile.Type = "openfloorTile";
                        transform.position = new Vector3(transform.position.x, -1, transform.position.z);
                        gameObject.tag = "floor";
                        //tile.zone.map.tileCheckForEmptyToWall();
                    }
                }
            } 
            
        }
        else
        {

            if ((tile.Type == "empty") || (tile.Type == "border"))
            {
                int index = 0;
                tile.TuileRenderer.material = mats[index];
            }
            else if ((tile.Type != "empty"))
            {
                int index = 2;
                tile.TuileRenderer.material = mats[index];
            }
        }
    }

    void OnMouseExit()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if (tile.Type == "empty")
            {
                int index = 0;
                tile.TuileRenderer.material = mats[index];
            }
            else if ((tile.Type == "border") && (tile.TuileHandle.tag == "border"))
            {
                int index = 0;
                tile.TuileRenderer.material = mats[index];
            }
            else if ((tile.Type == "wallTile") && (tile.TuileHandle.tag == "border"))
            {
                int index = 2;
                tile.TuileRenderer.material = mats[index];
            }
            else
            {
                int index = 2;
                tile.TuileRenderer.material = mats[index];
            }
            
        }
    }




    void OnMouseDown()
    {
        if (tile.Type == "pion")
        {
            Application.LoadLevelAdditive(
        }
    }

    void changetowall()
    {
        tile.Type = "wallTile";
        tile.TuileHandle.tag = "wall";
        tile.TuileRenderer.material = mats[2];
        Destroy(bosc);
    }

    void changetowallborder()
    {
        tile.Type = "wallTile";
        tile.TuileHandle.tag = "border";
        tile.TuileRenderer.material = mats[2];
        Destroy(bosc);
    }

    void Start()
    {
        if (tile.Type == "empty")
        {
            this.bosc = (GameObject)Instantiate(Resources.Load("tile/obscurator"));
            bosc.transform.position = new Vector3(transform.position.x,transform.position.y+0.51f,transform.position.z);
        }
    }
}
