using UnityEngine;
using System.Collections;
using System;
using Worldbuilder;
using System.Collections.Generic;
using System.IO;

public class TuileAction : MonoBehaviour {

    Material tuileHover;
    static Material tuileNormal;
    public bool isSelected;
    public static bool isSummoned;
    public Material mat;
    public static Tuile tuileSelect;
    public Tuile tuile;
    public static List<Tuile> liste;
    int x;
    int y;
    public static Pion pion;
    bool choixDirection = false;
    bool isClicked = false;
    Direction direction;
    GUISkin skin;
    Quaternion rotation = new Quaternion();

    void Start()
    {
        isSummoned = false;
        isSelected = false;
        tuileNormal = Resources.Load("Materials/openfloorTile") as Material;
        tuileHover = Resources.Load("Materials/openfloorTileHovered") as Material;
        pion = new Pion();
        skin = ScriptableObject.CreateInstance<GUISkin>();
        liste = new List<Tuile>();

        Texture2D btNormal = Resources.Load("style/buttonNormal") as Texture2D;
        Texture2D btHover = Resources.Load("style/buttonHover") as Texture2D;
        Texture2D btActive = Resources.Load("style/buttonActive") as Texture2D;

        Texture2D labelNormal = Resources.Load("style/textField") as Texture2D;

        Texture2D box = Resources.Load("style/box") as Texture2D;

        Color couleur = new Color((126f / 255f), (102f / 255f), (186f / 255f));
        
        skin.box.normal.background = box;

        skin.label.alignment = TextAnchor.MiddleCenter;
        skin.label.normal.background = labelNormal;
        skin.label.normal.textColor = Color.black;

        skin.button.alignment = TextAnchor.MiddleCenter;
        skin.button.normal.background = btNormal;
        skin.button.normal.textColor = Color.black;

        skin.button.hover.background = btHover;
        skin.button.hover.textColor = Color.white;


        skin.button.active.background = btActive;
        skin.button.active.textColor = Color.red;

    }

    void OnGUI()
    {
        if (isClicked)
        {

            GUI.Box(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), "Direction");
            GUILayout.BeginArea(new Rect(((Screen.width / 4) + 30), Screen.height / 4, Screen.width / 2, Screen.height / 2));

            if (GUILayout.Button("Nord", skin.button, GUILayout.Height(40)))
            {
                rotation = Quaternion.Euler(new Vector3(0,0,0));
                choixDirection = true;
                isClicked = false;
            }
            else if (GUILayout.Button("Sud", skin.button, GUILayout.Height(40)))
            {
                rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                choixDirection = true;
                isClicked = false;
            }
            else if (GUILayout.Button("Est", skin.button, GUILayout.Height(40)))
            {
                rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                choixDirection = true;
                isClicked = false;
            }
            else if (GUILayout.Button("Ouest", skin.button, GUILayout.Height(40)))
            {
                rotation = Quaternion.Euler(new Vector3(0, -90, 0));
                choixDirection = true;
                isClicked = false;
            }

            GUILayout.EndArea();

        }

    }//ONGUI

    void Update()
    {
        if (!isSummoned)//on invoque
        {
            if (choixDirection)
            {
                GameObject prefab = Resources.Load("object/pion_prefab") as GameObject;
                GameObject pionObject = GameObject.Instantiate(prefab) as GameObject;

                string nom;
                string couleur;
                if (pion.EstVisible)
                {
                    nom = pion.Entite.Nom;
                }
                else
                {
                    nom = "cache";
                }

                couleur = "bleu";

                pionObject.name = "pion " + pion.Entite.Nom;

                Material[] mats = pionObject.GetComponent<MeshRenderer>().materials;
                mats[0] = Resources.Load("object/Materials/pion" + nom) as Material;
                mats[1] = Resources.Load("object/Materials/" + couleur) as Material;
                pionObject.GetComponent<MeshRenderer>().materials = mats;

                Debug.Log(nom + " == " + couleur + " == " + pion.EstVisible);
                pionObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x, 1, this.gameObject.transform.localPosition.z);
                pionObject.transform.localEulerAngles = rotation.eulerAngles;

                //pion.PionObject = pionObject;
                Pion temp = new Pion(pion.EstVisible);
                temp.PionObject = pionObject;
                tuile.Pion = temp;
                Gestion.joueur1.ListePion.Add(temp);
                liste = new List<Tuile>();
                choixDirection = false;
                isSummoned = true;
                clear();
            }
        }
        else//on déplace
        {
            if (choixDirection)
            {

                pion.PionObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x, 1, this.gameObject.transform.localPosition.z);
                pion.PionObject.transform.localEulerAngles = rotation.eulerAngles;
                tuile.Pion = pion;
                tuileSelect.Pion = null;
                liste = new List<Tuile>();
                choixDirection = false;
                clear();
            }
        }

    }//UPDATE

    void OnMouseOver()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetMouseButtonDown(0))
            {

                if (tuile.Pion != null)// && Gestion.joueur1.ListePion.Contains(pion) //  && !pion.EstInvoque
                {
                    clear();

                    pion = tuile.Pion;
                    tuileSelect = tuile;

                    if (!tuile.Pion.EstInvoque && Gestion.joueur1.ListePion.Contains(pion))
                    {
                        liste = tuileSelect.getTuileAutour();

                        if (liste != null)
                        {
                            for (int i = 0; i < liste.Count; i++)
                            {
                                if (liste[i].Type == TuileType.normal)
                                {
                                    liste[i].TuileObject.renderer.material = tuileHover;
                                }
                            }
                        }
                    }
                }
                else if (liste.Contains(tuile) && tuile.Type == TuileType.normal)
                {
                    isClicked = true;
                    Debug.Log("tuile hoover da");
                    //Gestion.joueur1.ListePion.Add(pion);
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                clear();
            }
        }

    }

    public static  void clear()
    {
        for (int i = 0; i < Map.tuileMap.Count; i++)
        {
            List<Tuile> liste = Map.tuileMap[i];
            for (int j = 0; j < liste.Count; j++)
            {
                if (liste[j].Type == TuileType.normal)
                {
                    Map.tuileMap[i][j].TuileObject.renderer.material = tuileNormal;
                }
            }
        }
    }


    public static Pion Pion { get { return pion; } set { pion = value; } }


}
