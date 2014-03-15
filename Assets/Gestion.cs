using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Worldbuilder;

public class Gestion : MonoBehaviour
{

    public static Joueur joueur1;
    public static Joueur joueur2;
    InterfaceIngame interfaceJoueur1;
    InterfaceIngame interfaceJoueur2;
    List<Tuile> listeA;
    List<Tuile> listeB;
    Material tuileHover;
    Material tuileNormal;
    bool encore;
    Map map;
    bool phaseTime = true;

    GUIStyle style;
    GUISkin skin;

    // Use this for initialization
    void Start()
    {

        /*Pour le style GUI*/

        //ICI LE STYLE DU GUI
        style = new GUIStyle();
        skin = ScriptableObject.CreateInstance<GUISkin>();

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

        encore = false;

        listeA = new List<Tuile>();
        listeB = new List<Tuile>();
        tuileNormal = Resources.Load("Materials/openfloorTile") as Material;
        tuileHover = Resources.Load("Materials/openfloorTileHovered") as Material;

        string pseudo1 = "Panda";

        /*INITIALISATION
         * */
        map = new Map();
        interfaceJoueur1 = new InterfaceIngame(pseudo1);
        interfaceJoueur1.update();
        joueur1 = new Joueur(pseudo1, interfaceJoueur1);
        joueur1.initialisation();

        List<Joueur> listeJoueur = new List<Joueur>();
        listeJoueur.Add(joueur1);
        listeJoueur.Add(joueur2);

        Vector2 taille = init.initialisation(listeJoueur);

        //string pseudo2 = "Ido";

        GameObject cam = new GameObject();
        cam.name = "Pov";
        cam.AddComponent<Camera>();
        cam.gameObject.AddComponent<camera>();
        cam.GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
        cam.GetComponent<Camera>().depth = -1;
        cam.GetComponent<Camera>().cullingMask = 1;
        cam.GetComponent<Camera>().backgroundColor = UnityEngine.Color.black;

        Debug.Log(joueur1.Crypte.Direction);
        if (joueur1.Crypte.Direction == Direction.nord)
        {
            cam.transform.localPosition = new Vector3(taille.x / 2, 10, 1);
            cam.transform.localEulerAngles = new Vector3(80, 0, 0);
        }
        else if (joueur1.Crypte.Direction == Direction.sud)
        {
            cam.transform.localPosition = new Vector3(taille.x / 2, 10, taille.y);
            cam.transform.localEulerAngles = new Vector3(80, 180, 0);
        }
        else if (joueur1.Crypte.Direction == Direction.ouest)
        {
            cam.transform.localPosition = new Vector3(taille.x / 2, 10, 0);
            cam.transform.localEulerAngles = new Vector3(80, 0, 0);
        }
        else
        {
            cam.transform.localPosition = new Vector3(taille.x / 2, 10, 0);
            cam.transform.localEulerAngles = new Vector3(80, 0, 0);
        }

        /*
        interfaceJoueur2 = new InterfaceIngame(pseudo2);
        joueur2 = new Joueur(pseudo2, interfaceJoueur2);
         */

    }//START

    bool isInvocation = false;
    Entite entiteCourante;
    float compteur = 1.0f;
    bool lancerCompteur = false;

    void Update()
    {

        if (lancerCompteur)
        {
            compteur = compteur - Time.deltaTime;
            Debug.Log(compteur);
        }

        /*Les vérifications pour savoir si un joueur peut invoquer*/
        //*
        if (joueur1.Hand.ListeCarte.Count > 0)
        {
            /*
            isInvocation = false;
            foreach (Entite e in joueur1.Hand.ListeCarte)
            {
                if (e.Carte.carte.GetComponent<MainAction>().isClicked)
                {

                    if (e.Carte != carteCourante.Carte && carteCourante.Carte != null)
                    {
                        carteCourante.Carte.carte.GetComponent<MainAction>().isClicked = false;
                    }

                    isInvocation = true;
                    carteCourante = e;
                }

            }*/
        }
        //*/

        /*Les vérification pour savoir si un joueur peut piocher*/
        /*
        if (joueur1.Pioche.ListeCarte.Count > 0)
        {

            for (int i = 0; i < joueur1.Pioche.ListeCarte.Count; i++)
            {
                Entite e = joueur1.Pioche.ListeCarte[i];
                if (e.Carte.carte.GetComponent<PiocheAction>().isClicked)
                {
                    e.Carte.carte.GetComponent<PiocheAction>().isClicked = false;
                    joueur1.piocher();
                }
            }
        }
         * //*/

        /*Si on veut invoquer alors on affiche les tuiles autour du tombeau 
         * ensuite TuileAction s'occupe de placer le pion sur la tuile choisit*/
        if (listeA.Count > 0 && listeB.Count > 0 && !TuileAction.isSummoned)
        {
            for (int i = 0; i < listeA.Count; i++)
            {
                if (listeA[i].Type == TuileType.normal)
                {
                    listeA[i].TuileObject.renderer.material = tuileHover;
                    TuileAction.liste.Add(listeA[i]);
                }
            }

            for (int i = 0; i < listeB.Count; i++)
            {
                if (listeB[i].Type == TuileType.normal)
                {
                    listeB[i].TuileObject.renderer.material = tuileHover;
                    TuileAction.liste.Add(listeB[i]);
                }
            }
        }
    }//FIN UPDATE

    public void mainClicked(Entite entite)
    {
        isInvocation = true;
        joueur1.EnergieSpirituelle -= entite.Cout;
        entiteCourante = entite;
    }



    bool choixShift;
    bool choixRecycle;
    int choixRecycleAction;
    bool choixInvoque;
    bool finShift = false;
    Direction direction;
    bool shift = false;
    bool shiftPhase = true;
    bool recyclePhase = false;
    bool actionBasePhase = false;
    bool canDrawCard = true;
    bool choixInvocation = true;
    bool choixVisible = false;
    bool invocation = false;

    Joueur j;
    
        /*
         * 
         * Phase time met une box qui cache la main, la pioche ainsi que le cimetiere
         * afin d'effectuer les différentes phases suivants:
         * Shift : action disponible : Shift ou passe
         * Recycle : action disponible : Recycle ou passe
         * RecycleAction une fois recycle accepté : action disponible : shift, ressuciter son hero ou bien jouer 2 fois
         * une fois ces phases terminés on passe ensuite aux actions de bases
         * 
         * Remarque : Si un joueur a recycle, il lui est impossible de piocher par la suite         * 
         * 
         * */
   
    void OnGUI()
    {


        j = joueur1;


        GUI.Label(new Rect(5, 5, 100, 20), (joueur1.EnergieSpirituelle + " / " + joueur1.EnergieSpirituelleMax), skin.label);


        if (phaseTime)
        {
            //Boite du bas qui propose toutes les actions des phases
            GUI.Box(new Rect(0, (Screen.height - Screen.height / 3), Screen.width, Screen.height / 3), "");
            GUILayout.BeginArea(new Rect(0, (Screen.height - Screen.height / 3), Screen.width, Screen.height / 3));
            if (shiftPhase)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Shift phase", skin.label, GUILayout.Height(40));
                GUILayout.EndHorizontal();
                //GUILayout.FlexibleSpace();
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Shift", skin.button, GUILayout.Height(40)))
                {
                    choixShift = true;
                    shiftPhase = false;
                    recyclePhase = false;
                }
                if (GUILayout.Button("Passer", skin.button, GUILayout.Height(40)))
                {
                    shiftPhase = false;
                    recyclePhase = true;
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
            else if (recyclePhase)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Recycle phase", skin.label, GUILayout.Height(40));
                GUILayout.EndHorizontal();
                //GUILayout.FlexibleSpace();
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Recycler", skin.button, GUILayout.Height(40)))
                {
                    choixRecycle = true;
                    shiftPhase = false;
                    recyclePhase = false;
                    canDrawCard = false;
                    actionBasePhase = false;
                    j.recycler();
                }
                if (GUILayout.Button("Passer", skin.button, GUILayout.Height(40)))
                {
                    shiftPhase = false;
                    phaseTime = false;
                    recyclePhase = false;
                    actionBasePhase = true;
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
            else if (choixRecycle)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Action supplémentaire !", skin.label, GUILayout.Height(40));
                GUILayout.EndHorizontal();
                //GUILayout.FlexibleSpace();
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Shift", skin.button, GUILayout.Height(40)))
                {
                    choixRecycleAction = 1;
                    choixRecycle = false;
                }
                if (GUILayout.Button("Ressuciter votre Héros", skin.button, GUILayout.Height(40)))
                {
                    choixRecycleAction = 2;
                    choixRecycle = false;
                }

                if (GUILayout.Button("Jouer 2 fois", skin.button, GUILayout.Height(40)))
                {
                    choixRecycleAction = 3;
                    choixRecycle = false;
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
            
            GUILayout.EndArea();

        }//fin si phase time 

        if (choixShift)
        {
            //La boite du milieu qui propose les direction
            GUI.Box(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), "Deplacement de la crypte");
            GUILayout.BeginArea(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2));

            if (j.Crypte.Direction == Direction.nord)
            {
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                if(GUILayout.Button("Ouest", skin.button, GUILayout.Height(40)))
                {
                    direction = Direction.ouest;
                    shift = true;
                }
                if(GUILayout.Button("Est", skin.button, GUILayout.Height(40)))
                {
                    direction = Direction.est;
                    shift = true;
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }//fin si direction == nord
            else if (j.Crypte.Direction == Direction.sud)
            {
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                if(GUILayout.Button("Ouest", skin.button, GUILayout.Height(40)))
                {
                    direction = Direction.ouest;
                    shift = true;
                }
                if(GUILayout.Button("Est", skin.button, GUILayout.Height(40)))
                {
                    direction = Direction.est;
                    shift = true;
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }//fin si direciton == sud
            else if (j.Crypte.Direction == Direction.est)
            {
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                if(GUILayout.Button("Nord", skin.button, GUILayout.Height(40)))
                {
                    direction = Direction.nord;
                    shift = true;
                }
                if(GUILayout.Button("Sud", skin.button, GUILayout.Height(40)))
                {
                    direction = Direction.sud;
                    shift = true;
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }//fin direction == est
            else
            {
                GUILayout.BeginVertical();
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Nord", skin.button, GUILayout.Height(40)))
                {
                    direction = Direction.nord;
                    shift = true;
                }
                if(GUILayout.Button("Sud", skin.button, GUILayout.Height(40)))
                {
                    direction = Direction.sud;
                    shift = true;
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }//fin else

            if (shift)
            {
                j.crypter(direction);
                choixShift = false;
                shift = false;
                if (actionBasePhase)
                {
                    finShift = false;
                }
                else
                {
                    finShift = true;
                }
            }

            GUILayout.EndArea();

        }//fin si choix shift



        if (choixRecycleAction == 1)
        {
            choixShift = true;
            choixRecycleAction = 0;
            actionBasePhase = true;
            phaseTime = false;
        }//fin si choixRecycleAction
        else if (choixRecycleAction == 2)
        {
            ressuciterHeroGestion(j);
            phaseTime = false;
            actionBasePhase = true;
            choixRecycleAction = 0;
        }//fin si choixRecycleAction
        else if (choixRecycleAction == 3)
        {
            encore = true;
            phaseTime = false;
            actionBasePhase = true;
            choixRecycleAction = 0;
        }//fin si choixRecycleAction

        if (!canDrawCard)
        {
            PiocheAction.isActive = false;
        }
        else
        {
            PiocheAction.isActive = true;
        }

        if (finShift)
        {
            /*
            GUI.Box(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), "");
            GUILayout.BeginArea(new Rect(((Screen.width / 4) + 30), Screen.height / 4, Screen.width / 2, Screen.height / 2));

            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Fin de votre Tour !!", skin.label, GUILayout.Height(40));
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            lancerCompteur = true;
            if (compteur <= 0)
            {
                //*/
            finTour();
            /* 
            compteur = 1.0f;
            }

            GUILayout.EndArea();
             //*/
            //j.crypter();
            //j.finirTour();

        }//FIN SHIFT

        if (actionBasePhase)
        {

           //On affiche une bouton invoquer au milieu de l'écran, A changer en fonction de la position de la carte
         
            if (isInvocation)
            {
                GUI.Box(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), "Invocation");
                GUILayout.BeginArea(new Rect(((Screen.width / 4) + 30), Screen.height / 4, Screen.width / 2, Screen.height / 2));

                if (choixInvocation)
                {
                    if (GUILayout.Button("Invoquer"))
                    {
                        choixVisible = true;
                        choixInvocation = false;
                        TuileAction.clear();
                    }
                }
                if (choixVisible)
                {
                    GUILayout.Label("Visibilité de votre pion :");
                    if (GUILayout.Button("Visible"))
                    {
                        choixVisible = false;
                        invocation = true;
                        if (TuileAction.Pion == null)
                        {
                            TuileAction.Pion = new Pion(true);
                        }
                        else
                        {
                            TuileAction.Pion.EstVisible = true;
                        }
                        TuileAction.Pion.Entite = entiteCourante;
                    } 
                    if (GUILayout.Button("Caché"))
                    {
                        choixVisible = false;
                        invocation = true;
                        if (TuileAction.Pion == null)
                        {
                            TuileAction.Pion = new Pion(false);
                        }
                        else
                        {
                            TuileAction.Pion.EstVisible = false;
                        }
                        TuileAction.Pion.Entite = entiteCourante;
                    }
                }
                if (invocation)
                {
                    listeA = joueur1.Crypte.Tombeau.Tuile[0].getTuileAutour();
                    listeB = joueur1.Crypte.Tombeau.Tuile[1].getTuileAutour();
                    //carteCourante = null;
                    //Destroy(carteCourante.carte);
                    //j.Cimetiere.ajouterCarte(carteCourante);
                    //j.Hand.retirerCarte(carteCourante);
                    Debug.Log("joueur 1 " + joueur1.Hand.ListeCarte.Count);
                    Debug.Log("j " + j.Hand.ListeCarte.Count);
                    TuileAction.isSummoned = false;
                    isInvocation = false;
                    choixInvocation = true;
                    invocation = false;
                    //carteCourante.carte.GetComponent<MainAction>().isClicked = false;
                }

                GUILayout.EndArea();
            }//isInvocation

            if (GUI.Button(new Rect(5, (Screen.height - Screen.height / 4), 100, 40), "Finir mon tour", skin.button))
            {
                finTour();
            }
        }//Fin si action phase
        //if (joueur1.EnergieSpirituelle > joueur1.Hand.CoutMini)



    }//ON GUI



    //if( (joueur1.EnergieSpirituelle > 0 || joueur2.EnergieSpirituelle > 0)


    public void ressuciterHeroGestion(Joueur j)
    {
        Pion pion = j.ressuciterHero();     

    }

    public void attaquerGestion(Pion pion, Joueur j)
    {
        int x = pion.PosX;
        int y = pion.PosY;

        List<Pion> pionAdjacent = map.pionAdjacent(pion);

        Pion pionChoisit = TuileAction.Pion;

        bool estMort = j.attaquer(pion, pionChoisit);

        if (estMort)
        {
            Map.tuileMap[pionChoisit.PosX][pionChoisit.PosY].Pion = null;
        }
    }
    /*
    public void updateMap()
    {
        List<Tuile> ligne = new List<Tuile>();

        for(int i = 0; i < Map.tuileMap.Count; i++){
            ligne = Map.tuileMap[i];
            for(int j = 0; j < ligne.Count; j++){


    }*/



    void finTour()
    {
        phaseTime = true;
        shiftPhase = true;
        actionBasePhase = false;
        canDrawCard = true;
        finShift = false;
        lancerCompteur = false;

        for (int i = 0; i < joueur1.ListePion.Count; i++)
        {
            joueur1.ListePion[i].EstInvoque = false;
        }
    }
}
