using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Worldbuilder;

[Serializable]
public class Tileholder : MonoBehaviour
{

    // Use this for initialization
    public Material[] mats;
    public Material tempMaterial;
    public Material pasMaterial;
    public Tuile tile;
    public GameObject bosc;
    int index;
    /*
    void OnMouseDown()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log(tile.Type);
            if (tile.Pion != null)
            {


            }
            else if (tile.Type == TuileType.normal)
            {
                Debug.Log(tile.WorldPosition + " ==== " + tile.PosX + " ===== " + tile.PosY);
                GameObject prefab = Resources.Load("object/pion_prefab") as GameObject;
                GameObject pion = GameObject.Instantiate(prefab) as GameObject;
                pion.name = "pion rat";
                pion.GetComponent<MeshRenderer>().material = Resources.Load("object/Materials/pionratbleu") as Material;
                pion.transform.parent = gameObject.transform;
                pion.transform.localPosition = new Vector3(0, 1, 0);
                tile.Type = "pion";
            }
        }
    }*/
    void OnMouseOver()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if (tile.Type == TuileType.vide)
            {
                tile.TuileObject.renderer.material = mats[1];
            }
            else if ((tile.TuileObject.tag == "border") && (tile.Type == TuileType.mur))
            {
                tile.TuileObject.renderer.material = mats[1];
            }
            else if ((tile.TuileObject.tag == "border") && (tile.Type == TuileType.mur))
            {

            }
            else
            {
                tile.TuileObject.renderer.material = mats[3];
                if (tag == "wall")
                {
                    if (Input.GetMouseButton(0))
                    {
                        tile.Type = TuileType.normal;
                        transform.position = new Vector3(transform.position.x, -1, transform.position.z);
                        gameObject.tag = "floor";
                        //tile.zone.map.tileCheckForEmptyToWall();
                    }
                }
            } 
            
        }
    }

    void OnMouseExit()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if (tile.Type == TuileType.vide)
            {
                index = 0;
                tile.TuileObject.renderer.material = mats[index];
            }
            else if ((tile.Type == TuileType.mur) && (tile.TuileObject.tag == "border"))
            {
                index = 0;
                tile.TuileObject.renderer.material = mats[index];
            }
            else if ((tile.Type == TuileType.mur) && (tile.TuileObject.tag == "border"))
            {
                index = 2;
                tile.TuileObject.renderer.material = mats[index];
            }
            else
            {
                index = 2;
                tile.TuileObject.renderer.material = mats[index];
            }
            
        }
    }
    
    void changetowall()
    {
        tile.Type = TuileType.mur;
        tile.TuileObject.tag = "wall";
        tile.TuileObject.renderer.material = mats[2];
        Destroy(bosc);
    }

    void changetowallborder()
    {
        tile.Type = TuileType.mur;
        tile.TuileObject.tag = "border";
        tile.TuileObject.renderer.material = mats[2];
        Destroy(bosc);
    }

    void Start()
    {
        if (tile.Type == TuileType.vide)
        {
            this.bosc = (GameObject)Instantiate(Resources.Load("tile/obscurator"));
            bosc.transform.position = new Vector3(transform.position.x,transform.position.y+0.51f,transform.position.z);
        }
    }
}
