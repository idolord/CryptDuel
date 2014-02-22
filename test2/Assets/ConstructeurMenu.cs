using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class ConstructeurMenu {

    // cette classe instancie et lance le menu

    // pour initialiser le menu
    public void initialise()
    {
        CreateInterface();
    }

    //instanciation et lancement du menu
    void CreateInterface()
    {
        //instanciation
        InterfaceObject menu = new InterfaceObject();

        //lancement
        menu.run();
    }

}

public class InterfaceObject 
{
    // cette classe rerésente l'objet interface, elle n'est que structurelle et représente la structure de l'interface avec:

    // la taille de l'écran
    static int w = Screen.width;
    static int h = Screen.height;

    //le nom de l'interface
    public string name;

    // une boite rectangulaire pour y afficher le contenu du menu en cour
    Rect box;

    // le gameobject sur lequel on ajoutera le monobehaviour pour récuperer les interactions
    GameObject handle = new GameObject();

    // une liste des composants virtuels de l'interface
    public List<iComponent> guiMenu;

    // une ancre vers les composant qui afiche l'interface, attaché au GameObject sus-mentioné
    menuDisplay display;

    // une ancre vers le joueur que l'interface instancie aprés le login
    joueur Joueur;

    // un string contenant le nom de l'utilisateur
    string username;

    // fonction pour acceder au joueur depuis l'exterieur
    public joueur getJoueur ()
    {
        return Joueur;
    }

    // fonction pour creer le joueur et attribuer le username en fonction
    void createJoueur(string s)
    {
        Joueur = new joueur(s);
        username = s;
    }

    // fonction d'initialisation de l'object interface: affiche le login
    public void run()
    {
        login();
    }

    // fonction pour acceder au composant d'affichage du gameobject soumis
    public menuDisplay GetDisplay(GameObject o)
    {
        menuDisplay temp = o.GetComponent<menuDisplay>();
        return temp;
    }

    // fonction pour définir la boite d'affichage en fonction du nom de l'interface
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

    // fonction pour creer une interface de type "Login"
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

    // fonction pour creer une interface de type "Principal"
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
        List<string> message = new List<string>();
        message.Add("Welcome, " + username);
        message.Add("CreepyCaveStudio's team hope you'll enjoy you'r Crypt Duel experience");
        message.Add("Have fun time palying it, come have a talk on our forum sometime.");
        display.showMessage(message);
    }

    // fonction pour creer une interface de type "Play"
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

    // fonction pour creer une interface de type "Load"
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

    // fonction pour creer une interface de type "DeckSelect"
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

    // fonction pour creer une interface de type "Options"
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

    // fonction pour creer une interface de type "MangeDeck"
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

    // fonction pour créer un bouton
    iBoutonFrame CreateButonFrame(string s)
    {
        iBoutonFrame temp = new iBoutonFrame();
        temp.SetType(s);
        return temp;
    }

    // fonction pour créer l'interface de chargement/sauvegarde
    iLoadSaveFrame CreateiLoadSaveFrame()
    {
        iLoadSaveFrame temp = new iLoadSaveFrame();
        return temp;
    }

    // fonction pour créer l'interface de selection de deck
    iDeckSelect CreateDeckSelect()
    {
        iDeckSelect temp = new iDeckSelect();
        return temp;
    }

    // fonction pour créer l'interface login
    iFormFrame CreateFieldFrame(string s)
    {
        iFormFrame temp = new iFormFrame();
        temp.SetType(s);
        return temp;
    }

    // fonction pour créer l'interface manage deck
    iManageDeckFrame CreateManageDeckFram(string s)
    {
        iManageDeckFrame temp = new iManageDeckFrame();
        temp.SetType(s);
        return temp;
    }

    // fonction pour créer un contenair a carte
    iCardContainer CreateCardContainer(string s, int posx, int posy)
    {
        iCardContainer temp = new iCardContainer(posx,posy);
        temp.SetType(s);
        return temp;
    }

    // fonction pour créer la frame d'un menu
    iHeader CreateMenu(string s)
    {
        iHeader temp = new iHeader();
        temp.SetType("Menu : "+ s);
        return temp;
    }

    // fonction pour récuperer le nom de l'interface
    public string getName()
    {
        return name;
    }

    // fonction de gession des evenement de type boutton
    public void getbouton(string s, GameObject o)
    {
        switch (s)
        {
            case "Enter":
                List<string> user = display.getUser();
                createJoueur(user[0]);
                MenuPrincipal();
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

    // fonction de gession  de selection de hero
    public void getDeck(string s, GameObject o)
    {
        MenuManageDeck(s);
        GameObject.Destroy(o);
    }
}

// composant d'interface de type bouton 
public class iBoutonFrame : iComponent
{
    
}

// composant d'interface de type loadSave 
public class iLoadSaveFrame : iComponent
{

}

// composant d'interface de type deck select
public class iDeckSelect : iComponent
{

}

// composant d'interface de type contenair a carte
public class iCardContainer : iComponent
{
    // contenu du contenair
    List<carte> contenu;

    // position du contenair
    Vector2 position;

    // variable qui retourne vrais si une carte est séléctionée dans le contenair
    bool isFocused;

    // à la création du contenair on  doit lui fournir une position, par défaut aucune carte n'est séléctionée
    public iCardContainer(int x, int y)
    {
        position = new Vector2(x, y);
        isFocused = false;
    }

    // fonction pour récuperer la position du contenair
    public Vector2 getpos()
    {
        return position;
    }

    // fonction pour récuperer l'état isFocused
    public bool getFocus()
    {
        return isFocused;
    }

    // fonction pour définir l'état isFocused
    public void setFocus(bool b)
    {
        isFocused = b;
    }

    // fonction pour récuperer le contenu du contenair
    public List<carte> getContenu()
    {
        List<carte> temp = new List<carte>();
        for (int i = 0; i < 30; i++)
        {
            temp.Add(new carte());
        }
        return temp;
    }
}

// composant d'interface de type management de deck
public class iManageDeckFrame : iComponent
{

}

// composant d'interface de type login
public class iFormFrame : iComponent
{

}

// composant d'interface de type titre d'interface
public class iHeader : iComponent
{

}

// classe de composant d'interface
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

// classe d'afichage
public class menuDisplay : MonoBehaviour
{
    //liste des composant de l'interface
    List<iComponent> gui;

    // boite dans laquel aficher les composants 
    Rect boite;

    // nom de l'interface
    string interfaceName;

    // état de visibilité de l'interface
    bool visible;

    // type de l'interface
    bool isMenu;

    // ancre vers l' interfaceobject lié a cet afichage 
    InterfaceObject handObject;

    // ancre vers le gameobject sur lequel est situé ce menudisplay
    GameObject hand;

    // variables pour les scroll
    public Vector2 scrollPosition1 = Vector2.zero;
    public Vector2 scrollPosition2 = Vector2.zero;

    // variable qui retourne la carte selectionée
    carte focusedCard;

    // texture de carte
    Texture2D carteTexture;
    Texture2D scaledhand;
    Texture2D scaledfocus;

    // ofset pour l'afichage de messages
    int messageYOfset = 0;

    // texte du message
    List<string> messagetext;

    // variable du login forme
    string username;
    string pasworld;

    // fonction pour récuperer les valeurs du login forme
    public List<string> getUser ()
    {
        List<string> temp = new List<string>();
        temp.Add(username);
        temp.Add(pasworld);
        return temp;
    }

    // fonction pour faire apparaitre le message puis l'éffacer
    IEnumerator ofsetMessage()
    {
       
            while (messageYOfset<20)
            {
                messageYOfset += 1;
                yield return null;
            }
            yield return new WaitForSeconds(10.0f);
            while (messageYOfset > 0)
            {
                messageYOfset -= 1;
                yield return null;
            }

        
    }

    // fonction pour afficher une liste de string 
    public void showMessage(List<string> s)
    {
        messagetext = s;
        StartCoroutine(ofsetMessage());
    }

    // initialistaion de l'affichage à sa construction
    void Awake()
    {
        // on instancie les images de carte
        carteTexture = (Texture2D)GameObject.Instantiate(Resources.Load("texture/Carte"));
        TextureScale.Bilinear(carteTexture, (int)((Screen.width * 15) / 100), (int)(((Screen.width * 15) / 100) / 0.6f));
        scaledfocus = carteTexture;
        //Destroy(carteTexture);
        carteTexture = (Texture2D)GameObject.Instantiate(Resources.Load("texture/Carte"));
        TextureScale.Bilinear(carteTexture, ((Screen.width * 90) / 100) / 10, ((Screen.height * 30) / 100) - 20);
        scaledhand = carteTexture;
        //Destroy(carteTexture);
        
        // on initialise messagetext et les variables utilisateur
        messagetext = new List<string>();
        username = "";
        pasworld = "";
    }


    // par défaut le display n'est pas de type menu et est caché 
    public menuDisplay()
    {
        visible = false;
        isMenu = false;
    }

    // fonction d'afichage de l'interface
    void OnGUI()
    {
        //si l'interface est visible
            if (visible)
            {
                // on crée l'emplacement de la boite d'afichage de message
                GUI.Box(new Rect(Screen.width - ((Screen.width * 91) / 100), (-((Screen.height * 20) / 100)) + ((Screen.height * messageYOfset) / 100), (Screen.width * 42) / 100, Screen.height - ((Screen.height * 80) / 100)), "");
                GUILayout.BeginArea(new Rect(Screen.width - ((Screen.width * 90) / 100), (-((Screen.height * 20) / 100)) + ((Screen.height * messageYOfset) / 100), (Screen.width * 42) / 100, Screen.height - ((Screen.height * 80) / 100)));
                GUILayout.BeginVertical();
                GUILayout.FlexibleSpace();
                foreach (string text in messagetext)
                {
                    GUILayout.Label(text);
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndVertical();
                GUILayout.EndArea();

                //ensuite

                // pour chaque composant de l'interface
                    foreach (iComponent compo in gui)
                    {
                        // s'il est de type titre
                        if (compo.GetType() == typeof(iHeader))
                        {
                            // l'interface est de type menu
                            isMenu = true;

                            // aficheage de la boite de l'interface
                            GUI.Box(boite, string.Empty);

                            //afichage du tytre de l'interface, centrée
                            GUILayout.BeginArea(boite);
                            GUILayout.BeginHorizontal("", GUILayout.Width(boite.width));
                            GUILayout.FlexibleSpace();
                            GUILayout.Label(compo.getLabel());
                            GUILayout.FlexibleSpace();
                            GUILayout.EndHorizontal();

                            //on commence un block d'afichage vertical
                            GUILayout.BeginVertical(GUILayout.Height(boite.height - Mathf.Floor((boite.height * 10) / 100)-10));
                            GUILayout.FlexibleSpace();
                        }
                        // s'il est de type bouton 
                        else if (compo.GetType() == typeof(iBoutonFrame))
                        {
                            // on crée un bouton
                            bool temp = GUILayout.Button(compo.getLabel(), GUILayout.Height((Mathf.Floor(boite.height - (Mathf.Floor((boite.height * 10) / 100))) / (gui.Count) - 5)));
                            //si le bouton est cliqué 
                            if (temp)
                            {
                                // on demande à l'interfaceobject de gérer l'événement
                                handObject.getbouton(compo.getLabel(), hand);
                            }
                        }
                        //s'il est de type loadsave
                        else if (compo.GetType() == typeof(iLoadSaveFrame))
                        {
                            
                            float sider = (Mathf.Floor((boite.height * 10) / 100));
                            // on définis la forme du menu (zone + bouton)
                            Rect frame = new Rect(boite.xMin - 5, boite.yMin - 5 + (sider), boite.width - 10, (boite.height - sider) - 5);
                            // on définis la zone d'afichage des sauvegarde
                            Rect loadSaveFrame = new Rect(boite.xMin - 5, boite.yMin - 5 + (sider), boite.width - 10, (boite.height - (sider * 2)) - 5);
                            // on désine une boite
                            GUI.Box(loadSaveFrame, "");
                            // on commence un zone
                            GUILayout.BeginArea(frame, "");
                            GUILayout.BeginVertical();
                            GUILayout.BeginHorizontal();
                            scrollPosition1 = GUILayout.BeginScrollView(scrollPosition1, GUILayout.Width(loadSaveFrame.width), GUILayout.Height(loadSaveFrame.height));
                            // pour chaque sauvegarde du joueur on l'affiche
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
                            //fin de la zone d'afichage des sauvegarde
                            GUILayout.BeginHorizontal();
                            GUILayout.FlexibleSpace();
                            // bouton retour
                            bool temp = GUILayout.Button("Back", GUILayout.Height((sider) - 5), GUILayout.Width((boite.width * 20) / 100));
                            if (temp)
                            {
                                handObject.getbouton("Back", hand);
                            }
                            GUILayout.EndHorizontal();
                            GUILayout.EndVertical();
                            GUILayout.EndArea();
                        }
                        // s'il est de type deckselect
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
                            //on afiche chaque hero débloqué par le joueur 
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
                            // bouton retour
                            bool temp = GUILayout.Button("Back", GUILayout.Height((sider) - 10), GUILayout.Width((boite.width * 20) / 100));
                            if (temp)
                            {
                                handObject.getbouton("Back", hand);
                            }
                            GUILayout.EndHorizontal();
                            GUILayout.EndVertical();
                            GUILayout.EndArea();
                        }
                        // si il est de type login
                         if (compo.GetType() == typeof(iFormFrame))
                        {
                            // on afiche les menu login
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
                        // si il est de type afichage de contenair de carte
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
                                         bool interactTemp = GUILayout.Button(scaledhand, GUILayout.Width(((Screen.width * 90) / 100) / 10), GUILayout.Height(((Screen.height * 30) / 100) - 20));
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
                                     GUILayout.BeginArea(new Rect((Screen.width/2) - (scaledfocus.width/ 2), position.y-(scaledfocus.height/2)+45, scaledfocus.width +10, scaledfocus.height +10));
                                     GUILayout.BeginHorizontal(GUILayout.Width(Screen.width));
                                     GUILayout.Box(scaledfocus);
                                     GUILayout.BeginVertical(GUILayout.Height((((Screen.width * 15) / 100) / 0.6f)));
                                     GUILayout.EndVertical();
                                     GUILayout.FlexibleSpace();
                                     GUILayout.EndHorizontal();
                                     GUILayout.EndArea();
                                 }
                         }
                    }
                    // si le display est de type menu, on ferme la zone titre
                    if (isMenu)
                    {
                        GUILayout.FlexibleSpace();
                        GUILayout.EndVertical(); 
                        GUILayout.EndArea();
                    }
            } 
       
    }

    //fonction pour définir un gui et l'aficher
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
        private Texture2D inCont;
        public Texture2D InCont { get; set; }
        private Texture2D focussed;
        public Texture2D Focussed { get; set; }

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

