using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class ConstructeurMenu {

    public void initialise()
    {
        CreateInterface();
    }

    void CreateInterface()
    {
        InterfaceObject menu = new InterfaceObject();
       menu.run();
    }
}

public class InterfaceObject 
{
    static int w = Screen.width;
    static int h = Screen.height;
    public string name;
    Rect box;
    GameObject handle = new GameObject();
    public List<iComponent> guiMenu;
    menuDisplay display;

    public void run()
    {
        MenuPrincipal();
    }

    public menuDisplay GetDisplay(GameObject o)
    {
        menuDisplay temp = o.GetComponent<menuDisplay>();
        return temp;
    }

    void SetbBox()
    {
        switch (this.name)
        {
            case "Play":
                box = new Rect((w / 2) - ((w / 2)/2)/2, (h / 2) - ((h / 2) / 2), (w / 2)/2, h / 2);
                break;
            case "Options":
                box = new Rect((w / 2) - ((w / 2)/2)/2, (h / 2) - ((h / 2) / 2), (w / 2)/2, h / 2);
                break;
            case "Load":
                box = new Rect(w - (w - 10), h - (h - 10), w - 20, h - 20);
                break;
            case "Manage Deck":
                box = new Rect(w - (w - 10), h - (h - 10), w - 20, h - 20);
                break;
            default:
                box = new Rect(w - ((w / 2) / 2), (h / 2) - ((h / 2) / 2), (w / 2)/2, h / 2);
                break;
        }
    }

    void MenuPrincipal()
    {
        GameObject menuinstance = GameObject.Instantiate(handle) as GameObject;
        guiMenu = new List<iComponent>();
        name = "principal";
        SetbBox();
        guiMenu.Add(CreateButon("Play"));
        guiMenu.Add(CreateButon("Load"));
        guiMenu.Add(CreateButon("Manage Deck"));
        guiMenu.Add(CreateButon("Options"));
        guiMenu.Add(CreateButon("Exit"));
        display = menuinstance.AddComponent<menuDisplay>();
        display.SetGUI(menuinstance, this, guiMenu, box, name);

    }

    void MenuPlay()
    {
        GameObject menuinstance = GameObject.Instantiate(handle) as GameObject;
        guiMenu = new List<iComponent>();
        name = "Play";
        SetbBox();
        guiMenu.Add(CreateButon("Solo"));
        guiMenu.Add(CreateButon("Multi"));
        guiMenu.Add(CreateButon("Back"));
        display = menuinstance.AddComponent<menuDisplay>();
        display.SetGUI(menuinstance, this, guiMenu, box, name);
    }

    void MenuLoad()
    {
        GameObject menuinstance = GameObject.Instantiate(handle) as GameObject;
        guiMenu = new List<iComponent>();
        name = "Load";
        SetbBox();
        guiMenu.Add(CreateiLoadSaveFrame());
        display = menuinstance.AddComponent<menuDisplay>();
        display.SetGUI(menuinstance, this, guiMenu, box, name);
    }

    void MenuManageDeck()
    {
        GameObject menuinstance = GameObject.Instantiate(handle) as GameObject;
        guiMenu = new List<iComponent>();
        name = "Manage Deck";
        SetbBox();
        guiMenu.Add(CreateManageDeckFrame());
        display = menuinstance.AddComponent<menuDisplay>();
        display.SetGUI(menuinstance, this, guiMenu, box, name);
    }

    void MenuOptions()
    {
        GameObject menuinstance = GameObject.Instantiate(handle) as GameObject;
        guiMenu = new List<iComponent>();
        name = "Options";
        SetbBox();
        guiMenu.Add(CreateButon("audio"));
        guiMenu.Add(CreateButon("Manage Save"));
        guiMenu.Add(CreateButon("Change User"));
        guiMenu.Add(CreateButon("Back"));
        display = menuinstance.AddComponent<menuDisplay>();
        display.SetGUI(menuinstance, this, guiMenu, box, name);
    }

    iBouton CreateButon(string s)
    {
        iBouton temp = new iBouton();
        temp.SetType(s);
        return temp;
    }

    iLoadSaveFrame CreateiLoadSaveFrame()
    {
        iLoadSaveFrame temp = new iLoadSaveFrame();
        return temp;
    }

    iManageDeckFrame CreateManageDeckFrame()
    {
        iManageDeckFrame temp = new iManageDeckFrame();
        return temp;
    }

    public string getName()
    {
        return name;
    }

    public void getbouton(string s, GameObject o)
    {
        switch (s)
        {
            case "Play":
                MenuPlay();
                GameObject.Destroy(o);
                break;
            case "Back":
                MenuPrincipal();
                GameObject.Destroy(o);
                break;
            case "Load":
                MenuLoad();
                GameObject.Destroy(o);
                break;
            case "Manage Deck":
                MenuManageDeck();
                GameObject.Destroy(o);
                break;
            case "Options":
                MenuOptions();
                GameObject.Destroy(o);
                break;
            case "Exit":
                Application.Quit();
                break;
            default:
                break;
        }
    }

    
}

public class iBouton : iComponent
{
    
}
public class iLoadSaveFrame : iComponent
{

}
public class iManageDeckFrame : iComponent
{

}

public class iComponent
{
    string label;

    public void SetType(string l)
    {
        label = l;
    }

    public string getLabel()
    {
        return label;
    }
}

public class menuDisplay : MonoBehaviour
{
    List<iComponent> gui;
    Rect boite;
    string interfaceName;
    bool visible;
    InterfaceObject handObject;
    GameObject hand;

    public menuDisplay()
    {
        visible = false;
    }

    void OnGUI()
    {
            if (visible)
            {
                GUI.Box(boite,string.Empty);
                GUILayout.BeginArea(boite);
                GUILayout.BeginVertical("", GUILayout.Height(Mathf.Floor((boite.height*10)/100)));
                GUILayout.FlexibleSpace();
                GUILayout.BeginHorizontal("", GUILayout.Width(boite.width));
                GUILayout.FlexibleSpace();
                GUILayout.Label(interfaceName);
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.EndVertical();
                    foreach (iComponent compo in gui)
                    {
                        if (compo.GetType() == typeof(iBouton))
                        {
                            bool temp = GUILayout.Button(compo.getLabel(), GUILayout.Height((Mathf.Floor(boite.height-(Mathf.Floor((boite.height * 10) / 100))) / (gui.Count)-5)));
                            if (temp)
                            {
                                handObject.getbouton(compo.getLabel(), hand);
                            } 
                        }
                        else if (compo.GetType() == typeof(iLoadSaveFrame))
                        {
                            float sider = (Mathf.Floor((boite.height * 10) / 100)) * 2;
                            GUI.Box(new Rect(boite.xMin-5, boite.yMin-5 + (sider / 2), boite.width-10, (boite.height - sider)-5), "");
                            GUILayout.Box(string.Empty, GUILayout.Height((boite.height - sider)-5));
                            GUILayout.BeginHorizontal();
                            GUILayout.FlexibleSpace();
                            bool temp = GUILayout.Button("Back", GUILayout.Height((sider / 2)-5), GUILayout.Width((boite.width*20)/100));
                            if (temp)
                            {
                                handObject.getbouton("Back", hand);
                            }
                            GUILayout.EndHorizontal();
                        }
                        else if (compo.GetType() == typeof(iManageDeckFrame))
                        {
                            float sider = (Mathf.Floor((boite.height * 10) / 100)) * 2;
                            GUI.Box(new Rect(boite.xMin - 5, boite.yMin - 5 + (sider / 2), boite.width - 10, (boite.height - sider) - 5), "");
                            GUILayout.Box(string.Empty, GUILayout.Height((boite.height-sider)-5));
                            GUILayout.BeginHorizontal();
                            GUILayout.FlexibleSpace();
                            bool temp = GUILayout.Button("Back", GUILayout.Height((sider / 2) - 5), GUILayout.Width((boite.width * 20) / 100));
                            if (temp)
                            {
                                handObject.getbouton("Back", hand);
                            }
                            GUILayout.EndHorizontal();
                        }

                    }
                
                GUILayout.EndArea();

            } 
       
    }

    void setvisible(bool s)
    {
        visible = s;
    }

    public void SetGUI(GameObject go,InterfaceObject o,List<iComponent> l, Rect b, string n)
    {
        gui = new List<iComponent>();
        gui = l;
        boite = b;
        interfaceName = n;
        handObject = o;
        hand = go;
        visible = true;
    }

}
