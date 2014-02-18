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
                box = new Rect((w / 2) - ((w / 2) / 2) / 2, (h / 2) - (((h / 2)/2)/2) , (w / 2) / 2, Mathf.Max((h / 2)/2,200));
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
            case "Manage Deck":
                box = new Rect(w - (w - 10), h - (h - 10), w - 20, h - 20);
                break;
            case "Principal":
                box = new Rect(w - ((w / 2) / 2), (h / 2) - ((h / 2) / 2), (w / 2)/2, h / 2);
                break;
            default:
                box = new Rect(w - (w - 10), h - (h - 10), w - 20, h - 20);
                break;
        }
    }

    void login()
    {
        GameObject menuinstance = GameObject.Instantiate(handle) as GameObject;
        guiMenu = new List<iComponent>();
        name = "Login";
        SetbBox();
        guiMenu.Add(CreateMenu(name));
        guiMenu.Add(CreateFieldFrame("Username"));
        guiMenu.Add(CreateFieldFrame("Passworld"));
        guiMenu.Add(CreateButonFrame("Enter"));
        display = menuinstance.AddComponent<menuDisplay>();
        display.SetGUI(menuinstance, this, guiMenu, box);
    }

    void MenuPrincipal()
    {
        GameObject menuinstance = GameObject.Instantiate(handle) as GameObject;
        guiMenu = new List<iComponent>();
        name = "Principal";
        SetbBox();
        guiMenu.Add(CreateMenu(name));
        guiMenu.Add(CreateButonFrame("Play"));
        guiMenu.Add(CreateButonFrame("Load"));
        guiMenu.Add(CreateButonFrame("Manage Deck"));
        guiMenu.Add(CreateButonFrame("Options"));
        guiMenu.Add(CreateButonFrame("Exit"));
        display = menuinstance.AddComponent<menuDisplay>();
        display.SetGUI(menuinstance, this, guiMenu, box);

    }

    void MenuPlay()
    {
        GameObject menuinstance = GameObject.Instantiate(handle) as GameObject;
        guiMenu = new List<iComponent>();
        name = "Play";
        SetbBox();
        guiMenu.Add(CreateMenu(name));
        guiMenu.Add(CreateButonFrame("Solo"));
        guiMenu.Add(CreateButonFrame("Multi"));
        guiMenu.Add(CreateButonFrame("Back"));
        display = menuinstance.AddComponent<menuDisplay>();
        display.SetGUI(menuinstance, this, guiMenu, box);
    }

    void MenuLoad()
    {
        GameObject menuinstance = GameObject.Instantiate(handle) as GameObject;
        guiMenu = new List<iComponent>();
        name = "Load";
        SetbBox();
        guiMenu.Add(CreateMenu(name));
        guiMenu.Add(CreateiLoadSaveFrame());
        display = menuinstance.AddComponent<menuDisplay>();
        display.SetGUI(menuinstance, this, guiMenu, box);
    }

    void MenuSelectDeck()
    {
        GameObject menuinstance = GameObject.Instantiate(handle) as GameObject;
        guiMenu = new List<iComponent>();
        name = "Deck Selection";
        SetbBox();
        guiMenu.Add(CreateMenu(name));
        guiMenu.Add(CreateDeckSelect());
        display = menuinstance.AddComponent<menuDisplay>();
        display.SetGUI(menuinstance, this, guiMenu, box);
    }

    void MenuOptions()
    {
        GameObject menuinstance = GameObject.Instantiate(handle) as GameObject;
        guiMenu = new List<iComponent>();
        name = "Options";
        SetbBox();
        guiMenu.Add(CreateMenu(name));
        guiMenu.Add(CreateButonFrame("audio"));
        guiMenu.Add(CreateButonFrame("Manage Save"));
        guiMenu.Add(CreateButonFrame("Change User"));
        guiMenu.Add(CreateButonFrame("Back"));
        display = menuinstance.AddComponent<menuDisplay>();
        display.SetGUI(menuinstance, this, guiMenu, box);
    }

    void MenuManageDeck(string s)
    {
        GameObject menuinstance = GameObject.Instantiate(handle) as GameObject;
        guiMenu = new List<iComponent>();
        name = "Manage deck : " + s;
        SetbBox();
        guiMenu.Add(CreateMenu(name));
        guiMenu.Add(CreateCardContainer("library",((Screen.width*20)/100)/2,(Screen.height*20)/100));
        guiMenu.Add(CreateCardContainer("deck",((Screen.width * 20) / 100) / 2, (Screen.height * 60) / 100));
        display = menuinstance.AddComponent<menuDisplay>();
        display.SetGUI(menuinstance, this, guiMenu, box);
    }

    iBoutonFrame CreateButonFrame(string s)
    {
        iBoutonFrame temp = new iBoutonFrame();
        temp.SetType(s);
        return temp;
    }

    iLoadSaveFrame CreateiLoadSaveFrame()
    {
        iLoadSaveFrame temp = new iLoadSaveFrame();
        return temp;
    }

    iDeckSelect CreateDeckSelect()
    {
        iDeckSelect temp = new iDeckSelect();
        return temp;
    }

    iFormFrame CreateFieldFrame(string s)
    {
        iFormFrame temp = new iFormFrame();
        temp.SetType(s);
        return temp;
    }

    iManageDeckFrame CreateManageDeckFram(string s)
    {
        iManageDeckFrame temp = new iManageDeckFrame();
        temp.SetType(s);
        return temp;
    }

    iCardContainer CreateCardContainer(string s, int posx, int posy)
    {
        iCardContainer temp = new iCardContainer(posx,posy);
        temp.SetType(s);
        return temp;
    }
    iHeader CreateMenu(string s)
    {
        iHeader temp = new iHeader();
        temp.SetType("Menu : "+ s);
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
                MenuSelectDeck();
                GameObject.Destroy(o);
                break;
            case "Options":
                MenuOptions();
                GameObject.Destroy(o);
                break;
            case "Exit":
                Application.Quit();
                break;
        }
    }

    public void getDeck(string s, GameObject o)
    {
        MenuManageDeck(s);
        GameObject.Destroy(o);
    }
}

public class iBoutonFrame : iComponent
{
    
}

public class iLoadSaveFrame : iComponent
{

}
public class iDeckSelect : iComponent
{

}
public class iCardContainer : iComponent
{
    List<carte> contenu;
    Vector2 position;
    bool isFocused;

    public iCardContainer(int x, int y)
    {
        position = new Vector2(x, y);
        isFocused = false;
    }

    public Vector2 getpos()
    {
        return position;
    }
    public bool getFocus()
    {
        return isFocused;
    }

    public void setFocus(bool b)
    {
        isFocused = b;
    }

    public List<carte> getContenu()
    {
        List<carte> temp = new List<carte>();
        temp.Add(new carte());
        temp.Add(new carte());
        temp.Add(new carte());
        temp.Add(new carte());
        temp.Add(new carte());
        temp.Add(new carte());
        temp.Add(new carte());
        temp.Add(new carte());
        temp.Add(new carte());
        temp.Add(new carte());
        temp.Add(new carte());
        temp.Add(new carte());
        temp.Add(new carte());
        temp.Add(new carte());
        temp.Add(new carte());
        temp.Add(new carte());
        temp.Add(new carte());
        temp.Add(new carte());
        return temp;
    }
}
public class iManageDeckFrame : iComponent
{

}
public class iFormFrame : iComponent
{

}
public class iHeader : iComponent
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
    bool isMenu;
    InterfaceObject handObject;
    GameObject hand;
    public Vector2 scrollPosition1 = Vector2.zero;
    public Vector2 scrollPosition2 = Vector2.zero;
    carte focusedCard;
    Texture2D carteTexture; 

    string username = string.Empty;
    string pasworld= string.Empty;

    public List<string> getUser ()
    {
        List<string> temp = new List<string>();
        temp.Add(username);
        temp.Add(pasworld);
        return temp;
    }

    void Awake()
    {
        carteTexture = (Texture2D)GameObject.Instantiate(Resources.Load("texture/Carte"));
        TextureScale.Bilinear(carteTexture, (int)((Screen.width*13)/100), (int)(((Screen.width*13)/100)/0.6f));
    }

    public menuDisplay()
    {
        visible = false;
        isMenu = false;
    }

    void Update()
    {
       
    }

    void OnGUI()
    {
            if (visible)
            {
                    foreach (iComponent compo in gui)
                    {
                        if (compo.GetType() == typeof(iHeader))
                        {
                            isMenu = true;
                            GUILayout.BeginArea(new Rect(Screen.width - (Screen.width * 10) / 100, Screen.height + 10, Screen.width - (Screen.width * 10) / 100, Screen.height - (Screen.height * 30) / 100));
                            GUILayout.EndArea();
                            GUI.Box(boite, string.Empty);
                            GUILayout.BeginArea(boite);
                            GUILayout.BeginHorizontal("", GUILayout.Width(boite.width));
                            GUILayout.FlexibleSpace();
                            GUILayout.Label(compo.getLabel());
                            GUILayout.FlexibleSpace();
                            GUILayout.EndHorizontal();
                            GUILayout.BeginVertical(GUILayout.Height(boite.height - Mathf.Floor((boite.height * 10) / 100)-10));
                            GUILayout.FlexibleSpace();
                        }
                        else if (compo.GetType() == typeof(iBoutonFrame))
                        {
                            bool temp = GUILayout.Button(compo.getLabel(), GUILayout.Height((Mathf.Floor(boite.height - (Mathf.Floor((boite.height * 10) / 100))) / (gui.Count) - 5)));
                            if (temp)
                            {
                                handObject.getbouton(compo.getLabel(), hand);
                            }
                        }
                        else if (compo.GetType() == typeof(iLoadSaveFrame))
                        {
                            float sider = (Mathf.Floor((boite.height * 10) / 100));
                            Rect frame = new Rect(boite.xMin - 5, boite.yMin - 5 + (sider), boite.width - 10, (boite.height - sider) - 5);
                            Rect loadSaveFrame = new Rect(boite.xMin - 5, boite.yMin - 5 + (sider), boite.width - 10, (boite.height - (sider * 2)) - 5);
                            GUI.Box(loadSaveFrame, "");
                            GUILayout.BeginArea(frame, "");
                            GUILayout.BeginVertical();
                            GUILayout.BeginHorizontal();
                            scrollPosition1 = GUILayout.BeginScrollView(scrollPosition1, GUILayout.Width(loadSaveFrame.width), GUILayout.Height(loadSaveFrame.height));
                            foreach (jeu Jeu in handObject.getJoueur().getJeu())
                            {
                                GUILayout.BeginHorizontal();
                                bool tempGame = GUILayout.Button("", GUILayout.Width((loadSaveFrame.width * 20) / 100), GUILayout.Height(loadSaveFrame.height / 3));
                                if (tempGame)
                                {
                                    handObject.getbouton(Jeu.getSaveName(), hand);
                                }
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
                        else if (compo.GetType() == typeof(iDeckSelect))
                        {
                            float sider = (Mathf.Floor((boite.height * 10) / 100));
                            Rect frame = new Rect(boite.xMin - 5, boite.yMin - 5 + (sider + 10), boite.width - 10, (boite.height - sider) - 20);
                            Rect heroframe = new Rect(boite.xMin - 5, boite.yMin - 5 + (((sider - 5) * 2) + (sider - 5)), boite.width - 10, ((boite.height - (sider * 2)) / 2) - 5);
                            GUI.Box(heroframe, "");
                            GUILayout.BeginArea(frame, "");
                            GUILayout.FlexibleSpace();
                            GUILayout.BeginVertical();
                            scrollPosition1 = GUILayout.BeginScrollView(scrollPosition1, GUILayout.Width(heroframe.width), GUILayout.Height(heroframe.height));
                            GUILayout.BeginHorizontal();
                            foreach (hero Hero in handObject.getJoueur().getUnlockedHero())
                            {
                                bool temphero = GUILayout.Button(Hero.getName(), GUILayout.Width(heroframe.width / 9), GUILayout.Height(heroframe.height - 30));
                                if (temphero)
                                {
                                    handObject.getDeck(Hero.getName(), hand);
                                }
                            }
                            GUILayout.EndHorizontal();
                            GUILayout.EndScrollView();
                            GUILayout.FlexibleSpace();
                            GUILayout.BeginHorizontal();
                            GUILayout.FlexibleSpace();
                            bool temp = GUILayout.Button("Back", GUILayout.Height((sider) - 10), GUILayout.Width((boite.width * 20) / 100));
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
                            GUILayout.BeginVertical("", GUILayout.Width(boite.width), GUILayout.Height((Mathf.Floor(boite.height - (Mathf.Floor((boite.height * 10) / 100)))-5)/gui.Count));
                            GUILayout.FlexibleSpace();
                            GUILayout.Label(compo.getLabel());
                            GUILayout.FlexibleSpace();
                            if (compo.getLabel() == "Username")
                            {
                                username = GUILayout.TextField(username, 25);
                            }
                            else
                            {
                                pasworld = GUILayout.PasswordField(pasworld,"*"[0]);
                            }
                            GUILayout.FlexibleSpace();
                            GUILayout.EndVertical();
                        }
                         if (compo.GetType() == typeof(iCardContainer))
                         {
                                 iCardContainer temp = compo as iCardContainer;
                                 Vector2 position = temp.getpos();
                                 GUI.Box(new Rect(position.x, position.y + (((Screen.height * 30) / 100) / 2), ((Screen.width * 80) / 100), (((Screen.height * 30) / 100) / 2)), "");
                                 if (!temp.getFocus())
                                 {
                                     GUILayout.BeginArea(new Rect(position.x, position.y + 10, (Screen.width * 80) / 100, ((Screen.height * 30) / 100) + 10));
                                     if (compo.getLabel() == "deck" || compo.getLabel() == "Hand")
                                     {
                                         scrollPosition1 = GUILayout.BeginScrollView(scrollPosition1);
                                     }
                                     else
                                     {
                                         scrollPosition2 = GUILayout.BeginScrollView(scrollPosition2);
                                     }
                                     GUILayout.BeginHorizontal();
                                     for (int x = 0; x < temp.getContenu().Count; x++)
                                     {
                                         bool interactTemp = GUILayout.Button(temp.getContenu()[x].getName(), GUILayout.Width(((Screen.width * 90) / 100) / 10), GUILayout.Height(((Screen.height * 30) / 100) - 20));
                                         if (interactTemp)
                                         {
                                             focusedCard = temp.getContenu()[x];
                                             Debug.Log("boutton carte " + temp.getContenu()[x].getName() + " pressé");
                                             temp.setFocus(true);
                                         }
                                     }
                                     GUILayout.EndHorizontal();
                                     GUILayout.EndScrollView();
                                     GUILayout.EndArea();
                                 }
                                 else
                                 {
                                     GUILayout.BeginArea(new Rect(((Screen.width - (Screen.width * 13) / 100)/2), (position.y-((((Screen.width * 13) / 100) / 0.5f))/5), ((Screen.width * 13) / 100)+10, (((Screen.width * 13) / 100) / 0.5f)-10));
                                     GUILayout.BeginHorizontal(GUILayout.Width(Screen.width));
                                     GUILayout.Box(carteTexture);
                                     GUILayout.BeginVertical(GUILayout.Height((((Screen.width * 15) / 100) / 0.5f)));
                                     GUILayout.EndVertical();
                                     GUILayout.FlexibleSpace();
                                     GUILayout.EndHorizontal();
                                     GUILayout.EndArea();
                                 }
                         }
                    }
                    if (isMenu)
                    {
                        GUILayout.FlexibleSpace();
                        GUILayout.EndVertical(); 
                        GUILayout.EndArea();
                    }
            } 
       
    }

    public void aficherMessage()
    {

    }

    public void SetGUI(GameObject go,InterfaceObject o,List<iComponent> l, Rect b)
    {
        gui = new List<iComponent>();
        gui = l;
        boite = b;
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
        string name;

        public string getName ()
        {
            return name;
        }
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

