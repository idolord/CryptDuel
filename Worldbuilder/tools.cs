using System.Collections.Generic;
using System.Drawing;
using System.IO;
using UnityEngine;

namespace Worldbuilder
{
    public class tools
    {
        

        public static List<List<System.Drawing.Color>> getcolor(string path)
        {
            //Debug.Log("getting zonecolor form map image "+path);
            Bitmap zonemapimage = new Bitmap(path);
            List<List<System.Drawing.Color>> zonelist = new List<List<System.Drawing.Color>>();
            List<System.Drawing.Color> row = new List<System.Drawing.Color>();
            System.Drawing.Color pixelColor = new System.Drawing.Color();

            //zonemapimage = new Bitmap(path);
            for (int i = 0; i < zonemapimage.Height; i++)
            {
                row = new List<System.Drawing.Color>();
                for (int j = 0; j < zonemapimage.Width; j++)
                {
                    pixelColor = zonemapimage.GetPixel(j, i);
                    row.Add(pixelColor);
                    //Debug.Log("  " + pixelColor.R + "     " + pixelColor.G + "     " + pixelColor.B + "     " + pixelColor.A + "     \n");
                }
                zonelist.Add(row);
            }
            return zonelist;
        }

        public static string[] getMapZones(string path)
        {
            //Debug.Log("getting zone name from "+ path);
            string zonerecived = path;
            //Debug.Log("reader opened");
            StreamReader reader = openTextFile(zonerecived);
            string input;
            List<string> zonetext = new List<string>();
            List<string> zonetosendback = new List<string>();
            //Debug.Log("reading document");
            input = reader.ReadLine();
            while (input != null)
            {
                zonetext.Add(input);
                input = reader.ReadLine();
            }
            //Debug.Log("end reading");
            reader.Close();
            //Debug.Log ("computting");

            
            for (int i = 0; i < zonetext.Count; i++)
            {
                if (!zonetext[i].StartsWith("="))
                {
                    if (!zonetext[i].StartsWith(" "))
                    {
                        if (!zonetext[i].StartsWith("*"))
                        {
                            zonetosendback.Add(zonetext[i]);
                        }
                    }
                }
            }
            string definition;
            string[] zonetocompare = new string[zonetosendback.Count];
            int j = 0;
            for (int i = 0; i < zonetosendback.Count; i++)
            {
                definition = zonetosendback[i] + ",";
                i++;
                definition = definition + zonetosendback[i] + ",";
                i++;
                definition = definition + zonetosendback[i] + ",";
                i++;
                definition = definition + zonetosendback[i];
                //Debug.Log(definition);
                zonetocompare[j] = definition;
                j++;
            }
            
            //Debug.Log("returning string array of zone definitions");
            return zonetocompare;
        }

        public static string getzonename(string[] confzone, string reczone)
        {
            string[] mapconfig = new string[200];
            string recivedcolor = string.Empty;
            mapconfig = confzone;
            recivedcolor = reczone;
            string[] configparts = new string[4];
            string[] recparts = new string[3];
            string zonename = string.Empty;
            //Debug.Log("spliting recived zone" + recivedcolor);
            recparts = recivedcolor.Split(',');
            for (int i = 0; i < mapconfig.Length; i++)
            {
                configparts = mapconfig[i].Split(',');
                //Debug.Log("spliting config zone" + mapconfig[i]);
                if (configparts[0] == recparts[0])
                {
                    if (configparts[1] == recparts[1])
                    {
                        if (configparts[2] == recparts[2])
                        {
                            zonename = configparts[3];
                            //Debug.Log("sending back data "+zonename);
                            return zonename;
                        }
                    }
                }
            }

            
            return "error";
        }

        public static StreamReader openTextFile(string file)
        {
            StreamReader sr;
            sr = File.OpenText(file);
            return sr;
        }

        public static string colortostring(System.Drawing.Color x)
        {
            System.Drawing.Color color = x;
            string constr = color.R.ToString() + "," + color.G.ToString() + "," + color.B.ToString();
            return constr;
        }

    }


}

