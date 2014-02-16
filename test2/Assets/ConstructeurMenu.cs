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
    joueur Joueur;
    string username;
    string pasworld;

    public joueur getJoueur ()
    {
        return Joueur;
    }

    void createJoueur(string s)
    {
        Joueur = new joueur(s);
    }

    public void run()
    {
        login();
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
            case "Login":
                box = new Rect((w / 2) - ((w / 2) / 2) / 2, (h / 2) - (((h / 2)/2)/2) , (w / 2) / 2, (h / 2)/2);
                break;
            case "Play":
                box = new Rect((w / 2) - ((w / 2)/2)/2, (h / 2) - ((h / 2) / 2), (w / 2)/2, h / 2);
                break;
            case "Options":
                box = new Rect((w / 2) - ((w / 2)/2)/2, (h / 2) - ((h / 2) / 2), (w / 2)/2, h / 2);
                break;
            case "Load":
                box = new Rect(w - (w - 10), h - (h - 10), w - 20, h - 20);
                break;
            case "Deck Selection":
                box = new Rect(w - (w - 10), h - (h - 10), w - 20, h - 20);
                break;
            default:
                box = new Rect(w - ((w / 2) / 2), (h / 2) - ((h / 2) / 2), (w / 2)/2, h / 2);
                break;
        }
    }

    void login()
    {
        GameObject menuinstance = GameObject.Instantiate(handle) as GameObject;
        guiMenu = new List<iComponent>();
        name = "Login";
        SetbBox();
        guiMenu.Add(CreateFieldFrame("Username"));
        guiMenu.Add(CreateFieldFrame("Passworld"));
        guiMenu.Add(CreateButonFrame("Enter"));
        display = menuinstance.AddComponent<menuDisplay>();
        display.SetGUI(menuinstance, this, guiMenu, box, name);
    }

    void MenuPrincipal()
    {
        GameObject menuinstance = GameObject.Instantiate(handle) as GameObject;
        guiMenu = new List<iComponent>();
        name = "principal";
        SetbBox();
        guiMenu.Add(CreateButonFrame("Play"));
        guiMenu.Add(CreateButonFrame("Load"));
        guiMenu.Add(CreateButonFrame("Manage Deck"));
        guiMenu.Add(CreateButonFrame("Options"));
        guiMenu.Add(CreateButonFrame("Exit"));
        display = menuinstance.AddComponent<menuDisplay>();
        display.SetGUI(menuinstance, this, guiMenu, box, name);

    }

    void MenuPlay()
    {
        GameObject menuinstance = GameObject.Instantiate(handle) as GameObject;
        guiMenu = new List<iComponent>();
        name = "Play";
        SetbBox();
        guiMenu.Add(CreateButonFrame("Solo"));
        guiMenu.Add(CreateButonFrame("Multi"));
        guiMenu.Add(CreateButonFrame("Back"));
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
        name = "Deck Selection";
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
        guiMenu.Add(CreateButonFrame("audio"));
        guiMenu.Add(CreateButonFrame("Manage Save"));
        guiMenu.Add(CreateButonFrame("Change User"));
        guiMenu.Add(CreateButonFrame("Back"));
        display = menuinstance.AddComponent<menuDisplay>();
        display.SetGUI(menuinstance, this, guiMenu, box, name);
    }

    iBoutonFrame CreateButonFrame(string s)
    {
        iBoutonFrame temp = new iBoutonFrame();
        temp.SetType(s);
        return temp;
    }

    iComponent CreateiLoadSaveFrame()
    {
        iLoadSaveFrame temp = new iLoadSaveFrame();
        return temp;
    }

    iComponent CreateManageDeckFrame()
    {
        iManageDeckFrame temp = new iManageDeckFrame();
        return temp;
    }

    iFormFrame CreateFieldFrame(string s)
    {
        iFormFrame temp = new iFormFrame();
        temp.SetType(s);
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
            case "Enter":
                MenuPrincipal();
                List<string> user = display.getUser();
                createJoueur(user[0]);
                GameObject.Destroy(o);
                break;
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

public class iBoutonFrame : iComponent
{
    
}

public class iLoadSaveFrame : iComponent
{

}
public class iManageDeckFrame : iComponent
{

}

public class iFormFrame : iComponent
{

}
/*
public class iMessageFrame : iComponent
{

}*/

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
    private float scrollbarValue;
    private float scrollbarValue2;
    public Vector2 scrollPosition = Vector2.zero;

    string username = string.Empty;
    string pasworld= string.Empty;

    public List<string> getUser ()
    {
        List<string> temp = new List<string>();
        temp.Add(username);
        temp.Add(pasworld);
        return temp;
    }

    public menuDisplay()
    {
        visible = false;
    }

    void Update()
    {
       
    }

    void OnGUI()
    {
            if (visible)
            {
                GUILayout.BeginArea(new Rect(Screen.width - (Screen.width * 10) / 100, Screen.height + 10, Screen.width - (Screen.width * 10) / 100, Screen.height - (Screen.height * 30) / 100));
                GUILayout.EndArea();
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
                        if (compo.GetType() == typeof(iBoutonFrame))
                        {
                            bool temp = GUILayout.Button(compo.getLabel(), GUILayout.Height((Mathf.Floor(boite.height-(Mathf.Floor((boite.height * 10) / 100))) / (gui.Count)-5)));
                            if (temp)
                            {
                                handObject.getbouton(compo.getLabel(), hand);
                            } 
                        }
                        else if (compo.GetType() == typeof(iLoadSaveFrame))
                        {
                            float sider = (Mathf.Floor((boite.height * 10) / 100));
                            Rect frame = new Rect(boite.xMin - 5, boite.yMin - 5 + (sider), boite.width - 10, (boite.height - sider) - 5);
                            Rect loadSaveFrame = new Rect(boite.xMin-5, boite.yMin-5 + (sider), boite.width-10, (boite.height - (sider*2))-5);
                            GUI.Box(loadSaveFrame, "");
                            GUILayout.BeginArea(frame, "");
                            GUILayout.BeginVertical();
                            GUILayout.BeginHorizontal();
                            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(loadSaveFrame.width), GUILayout.Height(loadSaveFrame.height));
                            foreach (jeu Jeu in handObject.getJoueur().getJeu())
                            {
                                GUILayout.BeginHorizontal();
                                GUILayout.Button("", GUILayout.Width((loadSaveFrame.width * 20) / 100), GUILayout.Height(loadSaveFrame.height/3));
                                GUILayout.BeginVertical(GUILayout.Height(loadSaveFrame.height / 3));
                                GUILayout.FlexibleSpace();
                                GUILayout.Label("Save Name : " + Jeu.getSaveName());
                                GUILayout.FlexibleSpace();
                                GUILayout.Label("Game Turn : " + Jeu.getTurn());
                                GUILayout.FlexibleSpace();
                                GUILayout.Label("Played Hero : " + Jeu.getHero());
                                GUILayout.FlexibleSpace();
                                GUILayout.EndVertical();
                                GUILayout.EndHorizontal();
                            }
                            GUILayout.EndScrollView();
                            GUILayout.EndHorizontal();
                            GUILayout.BeginHorizontal();
                            GUILayout.FlexibleSpace();
                            bool temp = GUILayout.Button("Back", GUILayout.Height((sider) - 5), GUILayout.Width((boite.width * 20) / 100));
                            if (temp)
                            {
                                handObject.getbouton("Back", hand);
                            }
                            GUILayout.EndHorizontal();
                            GUILayout.EndVertical();
                            GUILayout.EndArea();
                        }
                        else if (compo.GetType() == typeof(iManageDeckFrame))
                        {
                            float sider = (Mathf.Floor((boite.height * 10) / 100));
                            Rect frame = new Rect(boite.xMin - 5, boite.yMin - 5 + (sider+10), boite.width - 10, (boite.height - sider));
                            Rect heroframe = new Rect(boite.xMin - 5, boite.yMin + ((sider*2)+(sider-5)), boite.width - 10, ((boite.height - (sider * 2))/2) - 10);
                            GUI.Box(heroframe, "");
                            GUILayout.BeginArea(frame, "");
                            GUILayout.FlexibleSpace();
                            GUILayout.BeginVertical();
                            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(heroframe.width), GUILayout.Height(heroframe.height));
                            GUILayout.BeginHorizontal();
                            foreach (hero Hero in handObject.getJoueur().getUnlockedHero())
                            {
                                GUILayout.Box(Hero.getName(), GUILayout.Width(heroframe.width / 9), GUILayout.Height(heroframe.height-30));
                            }
                            GUILayout.EndHorizontal();
                            GUILayout.EndScrollView();
                            GUILayout.FlexibleSpace();
                            GUILayout.BeginHorizontal();
                            GUILayout.FlexibleSpace();
                            bool temp = GUILayout.Button("Back", GUILayout.Height((sider) - 5), GUILayout.Width((boite.width * 20) / 100));
                            if (temp)
                            {
                                handObject.getbouton("Back", hand);
                            }
                            GUILayout.EndHorizontal();
                            GUILayout.EndVertical();
                            GUILayout.EndArea();
                        }
                         if (compo.GetType() == typeof(iFormFrame))
                        {
                            GUILayout.BeginHorizontal("",GUILayout.Width(boite.width));
                            GUILayout.FlexibleSpace();
                            GUILayout.Label(compo.getLabel());
                            GUILayout.FlexibleSpace();
                            GUILayout.EndHorizontal();
                            if (compo.getLabel() == "Username")
                            {
                                username = GUILayout.TextField(username, 25);
                            }
                            else
                            {
                                pasworld = GUILayout.PasswordField(pasworld,"*"[0]);
                            }
                        }

                    }
                
                GUILayout.EndArea();

            } 
       
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

    public class joueur
    {
        List<jeu> listeDesJeux;
        List<carte> cartestotal;
        string username;
        string heroselection;
        int cardselection;
        List<hero> unlockedHero;

        public joueur(string s)
        {
            listeDesJeux = new List<jeu>();
            cartestotal = new List<carte>();
            username = s;
            unlockedHero = getUnlockedHero();
            listeDesJeux.Add(new jeu());
            listeDesJeux.Add(new jeu());
            listeDesJeux.Add(new jeu());
            listeDesJeux.Add(new jeu());

        }

        public List<jeu> getJeu()
        {
            return listeDesJeux; 
        }

        public List<hero> getUnlockedHero()
        {
            List<hero> temp = new List<hero>();
            temp.Add(new hero("Raymond"));
            temp.Add(new hero("Robert"));
            temp.Add(new hero("Jean-eude"));
            temp.Add(new hero("François Ferdinand"));
            temp.Add(new hero("Super Dupond"));
            temp.Add(new hero("Talon Achile"));
            temp.Add(new hero("Lagafe Gaston"));
            temp.Add(new hero("Yukito Kishiro"));
            temp.Add(new hero("Goldorak"));
            temp.Add(new hero("Pikachu"));
            temp.Add(new hero("Bouletator"));
            temp.Add(new hero("Kapoué"));
            return temp;
        }

    }

    public class jeu
    {
        string name;
        int turn;
        List<hero> gameHeroes;
        string playedHero;
        //List<Tuile> map;
        //List<Entite> entite;

        public string getSaveName()
        {
            return name;
        }

        public int getTurn()
        {
            return turn;
        }

        public string getHero()
        {
            return playedHero;
        }

        public jeu()
        {
            name = "random game name";
            turn = 5;
            playedHero = "random hero name";
        }
    }

    public class carte
    {
        bool isUnlocked;
        int number;
    }
    public class hero
    {
        string name;

        public hero(string s)
        {
            name = s;
        }
        public string getName()
        {
            return name;
        }
    }

