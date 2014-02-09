using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Worldbuilder;

namespace Worldbuilder
{

    

    [Serializable]
    public class ZoneEffet
    {

        public ZoneEffet()
        {
        }

    }

    [Serializable]
    public class Joueur
    {
        /*pseudo qui sert notament pour le recap*/
        private string pseudo;
        private Deck deck;
        private Crypte crypte;
        private Hand hand;
        private Cimetiere cimetiere;
        private Pioche pioche;
        //private List<Creature> listeCreatureInvoque;
        private List<Pion> listePionInvoque;
        private Hero hero;
        private int energieSpirituelleMax = 700;
        private int energieSpirituelle;
        private int coutMinimal = 0;

        /*constructeur simple*/
        public Joueur(string pseudo)
        {
            this.pseudo = pseudo;
            deck = new Deck();
            listePionInvoque = new List<Pion>();

            hero = null;
            deck.ajouterCarte(new Creature("rat", 150, "creature"));
            deck.ajouterCarte(new Creature("ogre", 200, "creature"));
            deck.ajouterCarte(new Creature("monstre", 310, "creature"));
            deck.ajouterCarte(new Creature("tortue", 400, "creature"));
            deck.ajouterCarte(new Sort("Abracadabra", 130, "sort"));
            deck.ajouterCarte(new Piege("piege a ours!", 100, "piege"));
            deck.ajouterCarte(new Creature("rat", 150, "creature"));
            deck.ajouterCarte(new Creature("pikachu", 350, "creature"));
            deck.ajouterCarte(new Creature("dragon", 300, "creature"));
            deck.ajouterCarte(new Creature("archer", 150, "creature"));
            /*Petit coucou pour etre sur de rentrer ici*/
            Console.WriteLine("Bonjour " + pseudo + ". Voulez vous jouer un peu?");
        }

        /* l'initialisation se fait lorsque la partie se lance, pas avant sinon on parle d'une autre étape d'initialisation*/
        public void initialisation()
        {
            energieSpirituelle = energieSpirituelleMax;
            crypte = new Crypte("", crypte.getZonePoint());
            hand = new Hand();
            cimetiere = new Cimetiere();
            pioche = new Pioche();
            hero = new Hero("PANDA", energieSpirituelleMax - 100, "hero");

            /*On transfere les cartes du deck dans la pioche */
            pioche.setListeCarte(deck.getListeCarte());
            /*On mélange un peu tout ca*/
            pioche.melanger(pioche.getListeCarte());

            /*3 cartes dans la main pour test*/
            piocher();
            piocher();
            piocher();


            //hand.afficher("=== Votre main ===");
            //pioche.afficher("=== Votre pioche ===");
        }

        /*shift/déplacer la crypte entiere*/
        public void crypter()
        {
            Console.WriteLine("*** Shift ***");
            Console.WriteLine("Vers quelle direction voulez vous déplacer votre crypte?");
            Console.WriteLine("1. Nord");
            Console.WriteLine("2. Sud");
            Console.WriteLine("3. Est");
            Console.WriteLine("4. Ouest");
            string compteur = Console.ReadLine();
            crypte.deplacerCrypte(int.Parse(compteur));
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
            hero = new Hero("PandaOUF", 500, "hero");

            this.setEnergieSpirituelleActuelle(this.getEnergieSpirituelleActuelle() - this.hero.getCout());
            Console.WriteLine("++++++ INVOCATION D'UN HERO ++++++");
            Console.WriteLine("Le cout a été de :" + this.hero.getCout() + " et il vous reste " + this.getEnergieSpirituelleActuelle() + " ES");
            //un hero est forcément visible
            pion = new Pion();
            pion.setCreature(hero);
            pion.setEstHero(true);
            this.listePionInvoque.Add(pion);
            return pion;
        }

        /*piocher une carte*/
        public void piocher()
        {
                hand.ajouterCarte(pioche.piocher());
        }

        /*On invoquer une carte en donnant de l'ES*/
        public Pion invoquer()
        {
            string compteur;
            Pion pion;
            Entite carte;
            Console.WriteLine("Vous voulez invoquer quelle carte ?");

            //hand.afficherCreature("=== Votre main ===");
            compteur = Console.ReadLine();
            carte = hand.getListeCarte()[int.Parse(compteur) - 1];
            //hand.afficher("=== Votre main ===");

            if (this.getEnergieSpirituelleActuelle() > carte.getCout())
            {
                Console.WriteLine("1. Visible");
                Console.WriteLine("2. Caché");
                compteur = Console.ReadLine();
                bool estVisible = true;
                if (compteur == "1")
                {
                    Console.WriteLine("Bravo ! Vous avez invoqué " + carte.getNom() + " face visible");
                    estVisible = true;
                }
                else if (compteur == "2")
                {
                    Console.WriteLine("Bravo ! Vous avez invoqué " + carte.getNom() + " face caché");
                    estVisible = false;
                }
                this.setEnergieSpirituelleActuelle(this.getEnergieSpirituelleActuelle() - carte.getCout());
                Console.WriteLine("Le cout a été de :" + carte.getCout() + " et il vous reste " + this.getEnergieSpirituelleActuelle() + " ES");
                pion = new Pion();
                pion.setCreature((Creature)carte);
                this.listePionInvoque.Add(pion);
                hand.retirerCarte(carte);
                //hand.afficher("=== Votre main ===");
            }
            else
            {
                Console.WriteLine("Vous n'avez pas assez d'énergie spirituelle pour cette carte T_T");
                pion = null;
            }



            return pion;

        }

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
            for (int i = 0; i < listePionInvoque.Count(); i++)
            {
                listePionInvoque[i].setEstInvoque(false);
            }
            Console.WriteLine("Les créatures invoquées dans son tour n'ont plus le mal de l'invocation...");
            if (hero != null)
            {
                Console.WriteLine("Votre héro regagne de l'énergie spirituelle s'il en a perdu");
                hero.setVie(hero.getVie() + 100);
            }
        }

        public Crypte getCrypte()
        {
            return this.crypte;
        }

        public int getEnergieSpirituelleActuelle()
        {
            return this.energieSpirituelle;
        }

        public void setEnergieSpirituelleActuelle(int energieSpirituelle)
        {
            this.energieSpirituelle = energieSpirituelle;
        }

        public int getEnergieSpirituelleMax()
        {
            return this.energieSpirituelleMax;
        }

        public Hand getHand()
        {
            return this.hand;
        }

        public string getPseudo()
        {
            return this.pseudo;
        }

        public List<Pion> getListePionInvoque()
        {
            return this.listePionInvoque;
        }
    }

    [Serializable]
    public class Phase
    {
        public void afficher(Tour t)
        {
            Console.WriteLine(t.getNom());
        }
    }

    [Serializable]
    public class Tour
    {
        public string nom = "tour";
        public Tour()
        {
        }

        public string getNom()
        {
            return this.nom;
        }
    }
}
