using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using System.Text;
using Worldbuilder;

namespace Worldbuilder
{

    
    [Serializable]
    public class Joueur
    {
        /*pseudo qui sert notament pour le recap*/
        private InterfaceIngame interfaceIngame;
        private string pseudo;
        private GestionCarte deck;
        private Crypte crypte;
        private Hand hand;
        private Cimetiere cimetiere;
        private Pioche pioche;
        //private List<Creature> listeCreatureInvoque;
        private List<Pion> listePion;
        private Hero hero;
        private int energieSpirituelleMax = 700;
        private int energieSpirituelle;
        private int coutMinimal = 0;

        
        /*constructeur simple*/
        public Joueur(string pseudo, InterfaceIngame interfaceIngame)
        {
            this.interfaceIngame = interfaceIngame;
            this.pseudo = pseudo;
            deck = new GestionCarte();
            listePion = new List<Pion>();

            hero = null;
            deck.ajouterCarte(new Creature("ratspectral", 150));
            deck.ajouterCarte(new Creature("rat", 150));
            deck.ajouterCarte(new Creature("rat", 150));
            deck.ajouterCarte(new Creature("rat", 150));
            deck.ajouterCarte(new Creature("rat", 150));
            /*Petit coucou pour etre sur de rentrer ici*/
        }

        /* l'initialisation se fait lorsque la partie se lance, pas avant sinon on parle d'une autre étape d'initialisation*/
        public void initialisation()
        {
            EnergieSpirituelle = energieSpirituelleMax;
            //crypte = new Crypte("", crypte.ZonePoint);
            hand = new Hand(interfaceIngame.MainConteneur);
            cimetiere = new Cimetiere(interfaceIngame.CimetiereObjet);
            pioche = new Pioche(interfaceIngame.PiocheObjet);
            hero = new Hero("PandaOUF", 500);

            /*On transfere les cartes du deck dans la pioche */
            pioche.ajouterListe(deck.ListeCarte);
            /*On mélange un peu tout ca*/
            if (pioche == null)
            {
                Debug.Log("pioche null");
            }
            if (pioche.ListeCarte == null)
            {
                Debug.Log("liste null bouuuh");
            }

            //pioche.melanger();


            /*3 cartes dans la main pour test*/
            /*
            this.piocher();
            this.piocher();
            this.piocher();
             //*/

            //hand.afficher("=== Votre main ===");
            //pioche.afficher("=== Votre pioche ===");
        }

        /*shift/déplacer la crypte entiere*/
        public void crypter(Direction direction)
        {
            Debug.Log(crypte.Direction + " == " + direction);
            crypte.deplacerCrypte(crypte.Direction, direction);
        }

        /*recycler en défaussant une carte, puis choisis l'action supplémentaire a faire*/
        public void recycler()
        {
            //pioche.afficher("=== Votre pioche ===");
            pioche.piocher();
            //pioche.afficher("=== Votre pioche ===");
        }


        /*Fait partie du recyclage, on reprend notre hero du cimetiere*/
        public Pion ressuciterHero()
        {
            Pion pion;

            this.EnergieSpirituelle = this.EnergieSpirituelle - this.hero.Cout;
            //un hero est forcément visible
            pion = new Pion(true);
            pion.Entite = hero;
            this.listePion.Add(pion);
            return pion;
        }

        /*piocher une carte*/
        public void piocher()
        {
                hand.ajouterCarte(pioche.piocher());
        }


        /*
        public void cimetiere(){
            cimetiere.ajouterCarte*/

        /*On invoquer une carte en donnant de l'ES*/
        /*
        public Pion invoquer()
        {
            
            string compteur;
            Pion pion;
            Entite carte;

            //hand.afficherCreature("=== Votre main ===");
            compteur = Console.ReadLine();
            carte = hand.ListeCarte[int.Parse(compteur) - 1];
            //hand.afficher("=== Votre main ===");

            if (this.EnergieSpirituelle > carte.Cout)
            {
                GUI.Box(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), "Invocation");
                GUILayout.BeginArea(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2));
                

                bool estVisible = true;
                if (GUILayout.Button("Visible"))
                {
                    estVisible = true;
                }
                if (GUILayout.Button("Visible"))
                {
                    estVisible = false;
                }
                this.EnergieSpirituelle = this.EnergieSpirituelle - carte.Cout;
                pion = new Pion(estVisible);
                pion.Creature = (Creature)carte;
                this.listePionInvoque.Add(pion);
                hand.retirerCarte(carte);
                //hand.afficher("=== Votre main ===");
            }
            else
            {
                pion = null;
            }



            return pion;

        }*/

        public bool attaquer(Pion allie, Pion adverse)
        {

            //si l'entite vient de summon
            /*if (!allie.getEstInvoque())
            {
                int attaqueDonnee = allie.getCreature().getAttaque() - adverse.getCreature().getDefense();
                //si l'attaque est positive, sinon on redonne de la vie au pion (pas cool ca!)
                if (attaqueDonnee > 0)
                {
                    Console.WriteLine("*** Coup donnée: " + attaqueDonnee + " ***");
                    adverse.getCreature().setVie(adverse.getCreature().getVie() - attaqueDonnee);
                    if (adverse.getCreature().getVie() <= 0)
                    {
                        return true;
                    }
                }
            }

            return false;
            */
            return true;
        }//FIN ATTAQUER



        /*Le tour pour le joueur est finis T_T, avant il y a 2 ou 3 choses a régler pour pouvoir passer la main*/
        public void finirTour()
        {
            Console.WriteLine("Votre tour est finis");
            for (int i = 0; i < listePion.Count(); i++)
            {
                listePion[i].EstInvoque = false;
            }
            Console.WriteLine("Les créatures invoquées dans son tour n'ont plus le mal de l'invocation...");
            if (hero != null)
            {
                Console.WriteLine("Votre héro regagne de l'énergie spirituelle s'il en a perdu");
                hero.Vie = hero.Vie + 100;
            }
        }
        
        public int CoutMinimal  { get{ return this.coutMinimal;} set { this.coutMinimal = value;} }
        public int EnergieSpirituelle  { get{ return this.energieSpirituelle;} set { this.energieSpirituelle = value;} }
        public int EnergieSpirituelleMax  { get{ return this.energieSpirituelleMax;} set { this.energieSpirituelleMax = value;} }
        public Hero Hero  { get{ return this.hero;} set { this.hero = value;} }
        public List<Pion> ListePion { get { return this.listePion; } set { this.listePion = value; } }
        public Pioche Pioche { get { return this.pioche; } set { this.pioche = value; } }
        public Cimetiere Cimetiere { get { return this.cimetiere; } set { this.cimetiere = value; } }
        public Hand Hand { get { return this.hand; } set { this.hand = value; } }
        public Crypte Crypte  { get{ return this.crypte;} set { this.crypte = value;} }
        public GestionCarte Deck  { get{ return this.deck;} set { this.deck = value;} }
        public string Pseudo { get { return this.pseudo; } set { this.pseudo = value; } }
        public InterfaceIngame InterfaceIngame { get { return this.interfaceIngame; } set { this.interfaceIngame = value; } }
    }
}
