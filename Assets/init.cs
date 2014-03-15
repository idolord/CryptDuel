using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Drawing;
using Worldbuilder;

public class init : MonoBehaviour
{

    public static List<List<System.Drawing.Color>> getColor(string path)
    {
        //Debug.Log("getting zonecolor form map image "+path);

        Bitmap zoneMapImage;
        zoneMapImage = new Bitmap(path);
        List<List<System.Drawing.Color>> zoneListe = new List<List<System.Drawing.Color>>();
        List<System.Drawing.Color> row = new List<System.Drawing.Color>();
        System.Drawing.Color pixelColor = new System.Drawing.Color();

        //zonemapimage = new Bitmap(path);
        for (int i = 0; i < zoneMapImage.Width; i++)
        {
            row = new List<System.Drawing.Color>();
            for (int j = 0; j < zoneMapImage.Height; j++)
            {
                pixelColor = zoneMapImage.GetPixel(i, j);
                row.Add(pixelColor);
                //Debug.Log("  " + pixelColor.R + "     " + pixelColor.G + "     " + pixelColor.B + "     " + pixelColor.A + "     \n");
            }
            zoneListe.Add(row);
        }
        zoneMapImage.Dispose();
        return zoneListe;
    }


    // Use this for initialization
    public static Vector2 initialisation(List<Joueur> listeJoueur)
    {

        //instatiating mainmenu.
        //GameObject startmenu = Instantiate(Resources.Load("ui/menuGO")) as GameObject;

        string mapPath = @"/Textures/";
        //instanciating map with it's parameters

        int nombreJoueur = listeJoueur.Count;

        Map map = new Map();

        Debug.Log(mapPath + Convert.ToString(nombreJoueur) + ".png");
        //map.ColorMap = Tools.getColor(mapPath + Convert.ToString(nombreJoueur));  
        string path = Application.dataPath + mapPath + Convert.ToString(nombreJoueur) + ".png";
        Bitmap zoneMapImage;
        zoneMapImage = new Bitmap(path);

        Material tuileVide = Resources.Load("Materials/noir") as Material;
        Material tuileNormal = Resources.Load("Materials/openfloorTile") as Material;
        Material tuileTombeau = Resources.Load("Materials/rouge") as Material;
        Material tuileColonne = Resources.Load("Materials/violet") as Material;

        GameObject tuilePrefab = Resources.Load("tile/tuile_prefab") as GameObject;
        GameObject tuileFow = Resources.Load("tile/obsc_prefab") as GameObject;


        /*
        Debug.Log(zoneMapImage.Width + " == " + zoneMapImage.Height);
        for (int i = 0; i < zoneMapImage.Width; i++)
        {
            List<Tuile> ligne = new List<Tuile>();
            for (int j = 0; j < zoneMapImage.Height; j++)
            {
                Tuile tuile = new Tuile(i, j);
                tuile.TuileObject = Instantiate(tuilePrefab) as GameObject;
                /*
                 * Noir 0,0,0 = normal
                 * rouge 255,0,0 = tombeau
                 * blanc 255,255,255 = vide
                 * gris foncé : 60,60,60 = mur
                 * marron-gris foncé 80,80,60 = colonne
                 * */
             /*//   if (zoneMapImage.GetPixel(i, j).R == 0 && zoneMapImage.GetPixel(i, j).G == 0 && zoneMapImage.GetPixel(i, j).B == 0)
                {
                    tuile.TuileObject.name = "Normal";
                    tuile.TuileObject.AddComponent<TuileAction>();
                    tuile.Type = TuileType.normal;
                    tuile.TuileObject.renderer.material = tuileNormal;
                    tuile.TuileObject.transform.localPosition = new Vector3(i, 0, j);

                }
                else if (zoneMapImage.GetPixel(i, j).R == 80 && zoneMapImage.GetPixel(i, j).G == 80 && zoneMapImage.GetPixel(i, j).B == 60)
                {
                    tuile.TuileObject.name = "Colonne";
                    tuile.Type = TuileType.colonne;
                    tuile.TuileObject.renderer.material = tuileColonne;
                    tuile.TuileObject.transform.localPosition = new Vector3(i, 1, j);
                }
                else if (zoneMapImage.GetPixel(i, j).R == 60 && zoneMapImage.GetPixel(i, j).G == 60 && zoneMapImage.GetPixel(i, j).B == 60)
                {
                    tuile.TuileObject.name = "Mur";
                    tuile.Type = TuileType.mur;
                    tuile.TuileObject.renderer.material = tuileNormal;
                    tuile.TuileObject.transform.localPosition = new Vector3(i, 1, j);
                }
                else if (zoneMapImage.GetPixel(i, j).R == 255 && zoneMapImage.GetPixel(i, j).G == 0 && zoneMapImage.GetPixel(i, j).B == 0)
                {
                    tuile.TuileObject.name = "Tombeau";
                    tuile.Type = TuileType.tombeau;
                    tuile.TuileObject.renderer.material = tuileTombeau;
                    tuile.TuileObject.transform.localPosition = new Vector3(i, 1, j);
                }
                else if (zoneMapImage.GetPixel(i, j).R == 255 && zoneMapImage.GetPixel(i, j).G == 255 && zoneMapImage.GetPixel(i, j).B == 255)
                {
                    tuile.TuileObject.name = "Vide";
                    tuile.Type = TuileType.vide;
                    tuile.TuileObject.renderer.material = tuileVide;
                    tuile.TuileObject.transform.localPosition = new Vector3(i, 1, j);
                }
                ligne.Add(tuile);

            }
            Map.tuileMap.Add(ligne);
        }


        Direction random = (Direction)UnityEngine.Random.Range(1, nombreJoueur);
        Vector2 point;
        Tuile tuilea;
        Tuile tuileb;
        Tombeau tombeau;
        if (random == Direction.nord)
        {
            point = new Vector2(((zoneMapImage.Width / 2) - 8), 0);
            tuilea = new Tuile((int)(point.x + 7), (int)(point.y + 1));
            tuileb = new Tuile((int)(point.x + 8), (int)(point.y + 1));
            tombeau = new Tombeau(tuilea, tuileb);
        }
        else if (random == Direction.sud)
        {
            point = new Vector2(((zoneMapImage.Width / 2) + 8), zoneMapImage.Height);
            tuilea = new Tuile((int)(point.x - 7), (int)(point.y - 1));
            tuileb = new Tuile((int)(point.x - 8), (int)(point.y - 1));
            tombeau = new Tombeau(tuilea, tuileb);
        }
        else if (random == Direction.est)
        {
            point = new Vector2(zoneMapImage.Width, ((zoneMapImage.Height / 2) - 8));
            tuilea = new Tuile((int)(point.x - 1), (int)(point.y + 7));
            tuileb = new Tuile((int)(point.x - 1), (int)(point.y + 8));
            tombeau = new Tombeau(tuilea, tuileb);
        }
        else
        {
            point = new Vector2(0, ((zoneMapImage.Height / 2) + 8));
            tuilea = new Tuile((int)(point.x + 1), (int)(point.y - 7));
            tuileb = new Tuile((int)(point.x + 1), (int)(point.y - 8));
            tombeau = new Tombeau(tuilea, tuileb);
        }
        Crypte crypteJoueur1 = new Crypte(point, random, tombeau);

        listeJoueur[0].Crypte = crypteJoueur1;

        return new Vector2(zoneMapImage.Width, zoneMapImage.Height);

        //*/



        //*
         
        List<List<System.Drawing.Color>> liste = new List<List<System.Drawing.Color>>();
        liste = getColor(path);

        List<System.Drawing.Color> couleur = new List<System.Drawing.Color>();
        for (int i = 0; i < liste.Count; i++)
        {
            List<Tuile> ligne = new List<Tuile>();
            couleur = liste[i];
            for (int j = 0; j < couleur.Count; j++)
            {
                Tuile tuile = new Tuile(i, j);
                tuile.TuileObject = Instantiate(tuilePrefab) as GameObject;
                //*/

                /*/
                 * Noir 0,0,0 = normal
                 * rouge 255,0,0 = tombeau
                 * blanc 255,255,255 = vide
                 * gris foncé : 60,60,60 = mur
                 * marron-gris foncé 80,80,60 = colonne
                 //*/
        //*
                if (liste[i][j].R == 0 && liste[i][j].G == 0 && liste[i][j].B == 0)
                {
                    tuile.TuileObject.name = "Normal";
                    tuile.TuileObject.AddComponent<TuileAction>();
                    tuile.Type = TuileType.normal;
                    tuile.TuileObject.renderer.material = tuileNormal;
                    tuile.TuileObject.transform.localPosition = new Vector3(i, 0, j);
                    tuile.TuileObject.GetComponent<TuileAction>().tuile = tuile;

                }
                else if (liste[i][j].R == 80 && liste[i][j].G == 80 && liste[i][j].B == 60)
                {
                    tuile.TuileObject.name = "Colonne";
                    tuile.Type = TuileType.colonne;
                    tuile.TuileObject.renderer.material = tuileColonne;
                    tuile.TuileObject.transform.localPosition = new Vector3(i, 1, j);
                }
                else if (liste[i][j].R == 60 && liste[i][j].G == 60 && liste[i][j].B == 60)
                {
                    tuile.TuileObject.name = "Mur";
                    tuile.Type = TuileType.mur;
                    tuile.TuileObject.renderer.material = tuileNormal;
                    tuile.TuileObject.transform.localPosition = new Vector3(i, 1, j);
                }
                else if (liste[i][j].R == 255 && liste[i][j].G == 0 && liste[i][j].B == 0)
                {
                    tuile.TuileObject.name = "Tombeau";
                    tuile.Type = TuileType.tombeau;
                    tuile.TuileObject.renderer.material = tuileTombeau;
                    tuile.TuileObject.transform.localPosition = new Vector3(i, 1, j);
                }
                else if (liste[i][j].R == 255 && liste[i][j].G == 255 && liste[i][j].B == 255)
                {
                    tuile.TuileObject.name = "Vide";
                    tuile.Type = TuileType.vide;
                    tuile.TuileObject.renderer.material = tuileVide;
                    tuile.TuileObject.transform.localPosition = new Vector3(i, 1, j);
                    tuile.TuileFow = GameObject.Instantiate(tuileFow) as GameObject;
                    tuile.TuileFow.transform.parent = tuile.TuileObject.transform;
                    tuile.TuileFow.transform.localPosition = new Vector3(0, 1, 0);
                }
                ligne.Add(tuile);

            }
            Map.tuileMap.Add(ligne);
        }


        Debug.Log("width : " + liste.Count + " ==  height : " + couleur.Count);

        //Direction random = (Direction)UnityEngine.Random.Range(1, nombreJoueur);
        Vector2 point;
        Tuile tuilea;
        Tuile tuileb;
        Tombeau tombeau;
        Direction random = Direction.nord;

        if (random == Direction.nord)
        {
            point = new Vector2(((liste.Count / 2) - 8), 1);
            tuilea = new Tuile((int)(point.x + 7), (int)(point.y));
            tuileb = new Tuile((int)(point.x + 8), (int)(point.y));
            tombeau = new Tombeau(tuilea, tuileb);
        }
        else if (random == Direction.sud)
        {
            point = new Vector2((((liste.Count-1) / 2) + 8), couleur.Count-1);
            tuilea = new Tuile((int)(point.x - 7), (int)(point.y - 1));
            tuileb = new Tuile((int)(point.x - 8), (int)(point.y - 1));
            tombeau = new Tombeau(tuilea, tuileb);
        }
        else if (random == Direction.est)
        {
            point = new Vector2(liste.Count, (((couleur.Count-1) / 2) - 8));
            tuilea = new Tuile((int)(point.x - 1), (int)(point.y + 7));
            tuileb = new Tuile((int)(point.x - 1), (int)(point.y + 8));
            tombeau = new Tombeau(tuilea, tuileb);
        }
        else
        {
            point = new Vector2(0, ((couleur.Count / 2) + 8));
            tuilea = new Tuile((int)(point.x + 1), (int)(point.y - 7));
            tuileb = new Tuile((int)(point.x + 1), (int)(point.y - 8));
            tombeau = new Tombeau(tuilea, tuileb);
        }

        Debug.Log(point + " == " + random + " == " + couleur.Count + " == " + liste.Count + " == " + (point.y - 1) + " == " + (int)(point.x - 7));
        Crypte crypteJoueur1 = new Crypte(point, random, tombeau);

        Gestion.joueur1.Crypte = crypteJoueur1;

        return new Vector2(liste.Count, couleur.Count);
        //*/

    }
    
}