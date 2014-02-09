using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;

namespace Worldbuilder
{
    public class Tools
    {
        

        public static List<List<System.Drawing.Color>> getColor(string path)
        {
            //Debug.Log("getting zonecolor form map image "+path);
            Bitmap zoneMapImage = new Bitmap(path);
            List<List<System.Drawing.Color>> zoneListe = new List<List<System.Drawing.Color>>();
            List<System.Drawing.Color> row = new List<System.Drawing.Color>();
            System.Drawing.Color pixelColor = new System.Drawing.Color();

            //zonemapimage = new Bitmap(path);
            for (int i = 0; i < zoneMapImage.Height; i++)
            {
                row = new List<System.Drawing.Color>();
                for (int j = 0; j < zoneMapImage.Width; j++)
                {
                    pixelColor = zoneMapImage.GetPixel(j, i);
                    row.Add(pixelColor);
                    //Debug.Log("  " + pixelColor.R + "     " + pixelColor.G + "     " + pixelColor.B + "     " + pixelColor.A + "     \n");
                }
                zoneListe.Add(row);
            }
            return zoneListe;
        }

        public static string[] getMapZones(string path)
        {
            //Debug.Log("getting zone name from "+ path);
            string zoneRecu = path;
            //Debug.Log("reader opened");
            StreamReader reader = openTextFile(zoneRecu);
            string input;
            List<string> zoneTexte = new List<string>();
            List<string> zoneARenvoyer = new List<string>();
            //Debug.Log("reading document");
            input = reader.ReadLine();
            while (input != null)
            {
                zoneTexte.Add(input);
                input = reader.ReadLine();
            }
            //Debug.Log("end reading");
            reader.Close();
            //Debug.Log ("computting");


            for (int i = 0; i < zoneTexte.Count; i++)
            {
                if (!zoneTexte[i].StartsWith("="))
                {
                    if (!zoneTexte[i].StartsWith(" "))
                    {
                        if (!zoneTexte[i].StartsWith("*"))
                        {
                            zoneARenvoyer.Add(zoneTexte[i]);
                        }
                    }
                }
            }
            string definition;
            string[] zoneAComparer = new string[zoneARenvoyer.Count];
            int j = 0;
            for (int i = 0; i < zoneARenvoyer.Count; i++)
            {
                definition = zoneARenvoyer[i] + ",";
                i++;
                definition = definition + zoneARenvoyer[i] + ",";
                i++;
                definition = definition + zoneARenvoyer[i] + ",";
                i++;
                definition = definition + zoneARenvoyer[i];
                //Debug.Log(definition);
                zoneAComparer[j] = definition;
                j++;
            }
            
            //Debug.Log("returning string array of zone definitions");
            return zoneAComparer;
        }

        public static string getNomZone(string[] configZone, string recZone)
        {
            string[] mapConfig = new string[200];
            string receivedColor = string.Empty;
            mapConfig = configZone;
            receivedColor = recZone;
            string[] configParts = new string[4];
            string[] recparts = new string[3];
            string zoneName = string.Empty;
            //Debug.Log("spliting recived zone" + recivedcolor);
            recparts = receivedColor.Split(',');
            for (int i = 0; i < mapConfig.Length; i++)
            {
                configParts = mapConfig[i].Split(',');
                //Debug.Log("spliting config zone" + mapconfig[i]);
                if (configParts[0] == recparts[0])
                {
                    if (configParts[1] == recparts[1])
                    {
                        if (configParts[2] == recparts[2])
                        {
                            zoneName = configParts[3];
                            //Debug.Log("sending back data "+zonename);
                            return zoneName;
                        }
                    }
                }
            }

            
            return "error";
        }

        public static StreamReader openTextFile(string fichier)
        {
            StreamReader streamReader;
            streamReader = File.OpenText(fichier);
            return streamReader;
        }

        public static string colorToString(System.Drawing.Color uneCouleur)
        {
            System.Drawing.Color couleur = uneCouleur;
            string couleurString = couleur.R.ToString() + "," + couleur.G.ToString() + "," + couleur.B.ToString();
            return couleurString;
        }

    }


}

