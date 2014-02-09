using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Worldbuilder;

namespace Worldbuilder
{

    //entity familiy
    [Serializable]
    class Entite
    {
        protected string nom;
        protected int cout;
        protected string description;

        protected Carte carte;


        public Entite(string nom, int cout)
        {
            this.nom = nom;
            this.cout = cout;
            carte = new Carte(nom, cout, description);
        }

        public void setNom(string nom)
        {
            this.nom = nom;
        }

        public void setCout(int cout)
        {
            this.cout = cout;
        }

        public void setDescription(string description)
        {
            this.description = description;
        }

        public string getNom()
        {
            return this.nom;
        }

        public int getCout()
        {
            return this.cout;
        }

        public string getDescription()
        {
            return this.description;
        }
    }

    [Serializable]
    class Sort : Entite
    {
        public Sort(string nom, int cout)
            : base(nom, cout)
        {
        }
    }

    [Serializable]
    class Piege : Entite
    {
        public Piege(string nom, int cout)
            : base(nom, cout)
        {
        }
    }

    [Serializable]
    class Creature : Entite
    {
        /*les attributs réels de la créature*/
        protected int attaque;
        protected int defense;
        protected int retaliation;
        protected int duelScience;
        protected int vie;

        /*les chiffres qui permettent de générer les cases du layout correspondant a chaque action/status possible*/
        protected int layoutAttaque;
        protected int layoutDeplacement;
        protected int layoutVision;

        /*le layout se met sur la carte en bas a droite, est-ce qu'il se repartie aussi du le pion?*/
        protected Layout layout;

        public Creature(string nom, int cout)
            : base(nom, cout)
        {

            this.vie = cout;
            /*init de test*/
            layoutAttaque = 1;
            layoutDeplacement = 2;
            layoutVision = 4;
            layout = new Layout(layoutAttaque, layoutDeplacement, layoutVision);
        }

        public Layout getLayout()
        {
            return this.layout;
        }

        public void setDefense(int defense)
        {
            this.defense = defense;
        }

        public int getDefense()
        {
            return defense;
        }

        public void setDuelScience(int duelScience)
        {
            this.duelScience = duelScience;
        }

        public int getDuelScience()
        {
            return duelScience;
        }

        public void setRetalisation(int retaliation)
        {
            this.retaliation = retaliation;
        }

        public int getRetalisation()
        {
            return retaliation;
        }

        public void setAttaque(int attaque)
        {
            this.attaque = attaque;
        }

        public int getAttaque()
        {
            return attaque;
        }

        public void setVie(int vie)
        {
            this.vie = vie;
        }

        public int getVie()
        {
            return this.vie;
        }
    }

    [Serializable]
    class Hero : Creature
    {
        /*les attributs réels de la créature*/
        private int niveau;
        private int attaqueParNiveau;
        private int defenseParNiveau;
        private int retaliationParNiveau;
        private int duelScienceParNiveau;

        public Hero(string nom, int cout)
            : base(nom, cout)
        {
        }

        public void setNiveau(int niveau)
        {
            this.niveau = niveau;
        }

        public int getNiveau()
        {
            return this.niveau;
        }

        public int getAttaqueParNiveau()
        {
            return this.attaqueParNiveau;
        }

        public void setAttaqueParNiveau(int defenseParNiveau)
        {
            this.defenseParNiveau = defenseParNiveau;
        }

        public int getDefenseParNiveau()
        {
            return this.defenseParNiveau;
        }

        public void setDefenseParNiveau(int defenseParNiveau)
        {
            this.defenseParNiveau = defenseParNiveau;
        }

        public int getRetaliationParNiveau()
        {
            return this.retaliationParNiveau;
        }

        public void setRetaliationParNiveau(int retaliationParNiveau)
        {
            this.retaliationParNiveau = retaliationParNiveau;
        }

        public int getDuelScienceParNiveau()
        {
            return this.duelScienceParNiveau;
        }

        public void setDuelScienceParNiveau(int duelScienceParNiveau)
        {
            this.duelScienceParNiveau = duelScienceParNiveau;
        }

    }


    //card handler family
    [Serializable]
    class GestionCarte
    {
        /*Afin de bien différencier le type de carte,
         * on fait une liste de chaque, 
         * pour ensuite avoir les info et les action qui vont bien avec*/
        protected List<Entite> listeCarte = new List<Entite>();
        protected List<Creature> listeCreature = new List<Creature>();
        protected List<Sort> listeSort = new List<Sort>();
        protected List<Piege> listePiege = new List<Piege>();

        /*Le -1 signifie que le nombre est illimité par défaut, pour les phases de test et d'inconnu*/
        protected int nombreLimite = -1;
        protected int nombreCarte;

        /*la facon naive de différencier les types*/
        protected string[] type = { "creature", "carte", "sortilege", "piege" };

        public GestionCarte()
        {
        }

        /*Avec option*/
        public GestionCarte(int nombreLimite)
        {
            this.nombreLimite = nombreLimite;
        }

        /*De simple ACCESSEUR */


        public void setNombreLimite(int nombre)
        {
            this.nombreLimite = nombre;
        }

        public int getNombreLimite()
        {
            return this.nombreLimite;
        }

        public void setListeCreature(List<Creature> listeCreature)
        {
            this.listeCreature = listeCreature;
        }

        public List<Creature> getListeCreature()
        {
            return this.listeCreature;
        }

        public void setListePiege(List<Piege> listePiege)
        {
            this.listePiege = listePiege;
        }

        public List<Piege> getListePiege()
        {
            return this.listePiege;
        }

        public void setListeSort(List<Sort> listeSort)
        {
            this.listeSort = listeSort;
        }

        public List<Sort> getListeSort()
        {
            return this.listeSort;
        }

        public void setListeCarte(List<Entite> listeCarte)
        {
            this.listeCarte = listeCarte;
        }

        public List<Entite> getListeCarte()
        {
            return this.listeCarte;
        }

        /*On ajoute une carte dans la liste + surcharge des enfants */
        public virtual void ajouterCarte(Entite uneCarte)
        {
            if (listeCarte.Count > nombreLimite || nombreLimite == -1)
            {
                listeCarte.Add(uneCarte);
                this.nombreCarte++;
            }
        }

        public virtual void ajouterCarte(Creature uneCreature)
        {
            if (listeCarte.Count > nombreLimite || nombreLimite == -1)
            {
                listeCarte.Add(uneCreature);
                listeCreature.Add(uneCreature);
            }
        }

        public virtual void ajouterCarte(Piege unPiege)
        {
            if (listeCarte.Count > nombreLimite || nombreLimite == -1)
            {
                listeCarte.Add(unPiege);
                listePiege.Add(unPiege);
            }
        }
        public virtual void ajouterCarte(Sort unSort)
        {
            if (listeCarte.Count > nombreLimite || nombreLimite == -1)
            {
                listeCarte.Add(unSort);
                listeSort.Add(unSort);
            }
        }


        /*On retire une carte de(s) la liste + surcharge des enfants */

        public virtual void retirerCarte(Entite uneCarte)
        {
            if (listeCarte.Contains(uneCarte))
            {
                listeCarte.Remove(uneCarte);
                this.nombreCarte--;
            }
        }

        public virtual void retirerCarte(Creature uneCreature)
        {
            if (listeCarte.Contains(uneCreature) && listeCreature.Contains(uneCreature))
            {
                listeCarte.Remove(uneCreature);
                listeCreature.Remove(uneCreature);
            }
        }

        public virtual void retirerCarte(Piege unPiege)
        {
            if (listeCarte.Contains(unPiege) && listePiege.Contains(unPiege))
            {
                listeCarte.Remove(unPiege);
                listePiege.Remove(unPiege);
            }
        }

        public virtual void retirerCarte(Sort unSort)
        {
            if (listeCarte.Contains(unSort) && listeSort.Contains(unSort))
            {
                listeCarte.Remove(unSort);
                listeSort.Remove(unSort);
            }
        }

        /*Test d'affichage, différenciation des types de carte, il semblerait qu'avec cette facon de procéder
         * le tableau de string ne servent a rien*/



        public void afficherCreature(string presentation)
        {
            Console.WriteLine(presentation);
            int i = 1;
            foreach (Creature carte in listeCreature)
            {
                Console.Write(i + ". ");
                Console.Write("Creature");
                Console.Write(" - ");
                Console.Write(carte.getNom());
                Console.WriteLine();
                i++;
            }
            Console.WriteLine();
        }

    }


    [Serializable]
    class Hand : GestionCarte
    {

        private int coutMini;

        public Hand()
        {
        }

        /*Avec option*/
        public Hand(int nombreLimite)
            : base(nombreLimite)
        {
        }

        public void calculCoutMini()
        {
            coutMini = listeCarte[0].getCout();
            for (int i = 1; i < listeCarte.Count(); i++)
            {
                if (coutMini > listeCarte[i].getCout())
                {
                    coutMini = listeCarte[i].getCout();
                }
            }
        }

        public void setCoutMini(int cout)
        {
            this.coutMini = cout;
        }

        public int getCoutMini()
        {
            return this.coutMini;
        }


        /*On ajoute une carte dans la liste + surcharge des enfants */
        public override void ajouterCarte(Entite uneCarte)
        {
            if (listeCarte.Count > nombreLimite || nombreLimite == -1)
            {
                listeCarte.Add(uneCarte);
                this.nombreCarte++;
                calculCoutMini();
            }
        }

        public override void ajouterCarte(Creature uneCreature)
        {
            if (listeCarte.Count > nombreLimite || nombreLimite == -1)
            {
                listeCarte.Add(uneCreature);
                listeCreature.Add(uneCreature);
                calculCoutMini();
            }
        }

        public override void ajouterCarte(Piege unPiege)
        {
            if (listeCarte.Count > nombreLimite || nombreLimite == -1)
            {
                listeCarte.Add(unPiege);
                listePiege.Add(unPiege);
                calculCoutMini();
            }
        }
        public override void ajouterCarte(Sort unSort)
        {
            if (listeCarte.Count > nombreLimite || nombreLimite == -1)
            {
                listeCarte.Add(unSort);
                listeSort.Add(unSort);
                calculCoutMini();
            }
        }


        /*On retire une carte de(s) la liste + surcharge des enfants */

        public override void retirerCarte(Entite uneCarte)
        {
            if (listeCarte.Contains(uneCarte))
            {
                listeCarte.Remove(uneCarte);
                this.nombreCarte--;
                calculCoutMini();
            }
        }

        public override void retirerCarte(Creature uneCreature)
        {
            if (listeCarte.Contains(uneCreature) && listeCreature.Contains(uneCreature))
            {
                listeCarte.Remove(uneCreature);
                listeCreature.Remove(uneCreature);
                calculCoutMini();
            }
        }

        public override void retirerCarte(Piege unPiege)
        {
            if (listeCarte.Contains(unPiege) && listePiege.Contains(unPiege))
            {
                listeCarte.Remove(unPiege);
                listePiege.Remove(unPiege);
                calculCoutMini();
            }
        }

        public override void retirerCarte(Sort unSort)
        {
            if (listeCarte.Contains(unSort) && listeSort.Contains(unSort))
            {
                listeCarte.Remove(unSort);
                listeSort.Remove(unSort);
                calculCoutMini();
            }
        }

    }

    [Serializable]
    class Cimetiere : GestionCarte
    {
        public Cimetiere()
        {
        }
    }

    [Serializable]
    class Deck : GestionCarte
    {

        public const int NOMBRE_MINIMAL = 5;

        public Deck()
        {
            /*if (nombreCarte < NOMBRE_MINIMAL)
            {
                Console.WriteLine("Le deck ne contient pas assez de carte pour etre joué");
            }*/
        }



    }

    [Serializable]
    class Pioche : Deck
    {


        public void melanger(List<Entite> listeCarte)
        {
            Random random = new Random();
            int taille = listeCarte.Count;
            int chiffreRandom;
            List<Entite> tempPioche = new List<Entite>();

            for (int i = 0; i < taille; i++)
            {
                tempPioche.Add(listeCarte[i]);
            }

            List<int> melange = new List<int>();
            while (melange.Count < taille)
            {
                chiffreRandom = random.Next(0, taille);
                if (!(melange == null || melange.Contains(chiffreRandom)) && melange.Count != chiffreRandom)
                {
                    melange.Add(chiffreRandom);
                    //Console.WriteLine(chiffreRandom);
                }

            }

            for (int i = 0; i < taille; i++)
            {
                listeCarte[i] = tempPioche[melange[i]];
            }

        }


        public Entite piocher()//on considere que la premiere carte est celle du dessus
        {
            Entite carte;

            carte = listeCarte[0];
            listeCarte.RemoveAt(0);

            return carte;
        }

        public Creature piocherCreature()//on considere que la premiere carte est celle du dessus
        {
            Creature carte;

            carte = listeCreature[0];
            listeCreature.RemoveAt(0);
            listeCarte.RemoveAt(0);

            return carte;
        }

        public Sort piocherSort()//on considere que la premiere carte est celle du dessus
        {
            Sort carte;

            carte = listeSort[0];
            listeSort.RemoveAt(0);
            listeCarte.RemoveAt(0);

            return carte;
        }

        public Piege piocherPiege()//on considere que la premiere carte est celle du dessus
        {
            Piege carte;

            carte = listePiege[0];
            listePiege.RemoveAt(0);
            listeCarte.RemoveAt(0);

            return carte;
        }
    }





    [Serializable]
    class Carte
    {
        private string nom;
        private int cout;
        private string description;
        private string type;

        /*pour l'init*/
        public Carte(string nom, int cout, string description, string type)
        {
            this.nom = nom;
            this.cout = cout;
            this.description = description;
            this.type = type;
        }

        /*init de test*/
        public Carte()
        {

        }

        public void afficherCarte()
        {
            Console.WriteLine("===================");
            Console.WriteLine("=== " + nom + " ===");
            Console.WriteLine("=== " + cout + " ===");
            Console.WriteLine("=== " + description + " ===");
            Console.WriteLine("===================");
        }





    }

    [Serializable]
    class Layout
    {
        int attaque;
        int deplacement;
        int vision;
        int x = 13;
        int y = 13;

        public Layout(int attaque, int deplacement, int vision)
        {
            this.attaque = attaque;
            this.deplacement = deplacement;
            this.vision = vision;
        }

        public void genererLayout()
        {
            int calculAttaque1 = (x / 2) - attaque;
            int calculAttaque2 = (x / 2) + attaque;
            int calculDeplacement1 = (x / 2) - deplacement;
            int calculDeplacement2 = (x / 2) + deplacement;
            int calculVision1 = (x / 2) - vision;
            int calculVision2 = (x / 2) + vision;

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    bool conditionJoueur = (j == ((x / 2))) && (i == ((y / 2)));
                    bool conditionAttaque = ((j >= calculAttaque1 && j <= calculAttaque2) && (i >= calculAttaque1 && i <= calculAttaque2));//((j >= calculAttaque1 && j <= calculAttaque2) || (i >= calculAttaque1 && i <= calculAttaque2))
                    bool conditionDeplacement = ((j >= calculDeplacement1 && j <= calculDeplacement2) && (i >= calculDeplacement1 && i <= calculDeplacement2));
                    bool conditionVision = ((j >= calculVision1 && j <= calculVision2) && (i >= calculVision1 && i <= calculVision2));

                    if (conditionJoueur)
                        Console.Write(" J ");
                    else if (conditionAttaque)
                        Console.Write(" A ");
                    else if (conditionDeplacement)
                        Console.Write(" D ");
                    else if (conditionVision)
                        Console.Write(" V ");
                    else
                        Console.Write(" L ");
                }
                Console.WriteLine();
            }
        }
    }

    [Serializable]
    class ZoneEffet
    {

        public ZoneEffet()
        {
        }

    }

    [Serializable]
    class Joueur
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
            deck.ajouterCarte(new Creature("rat", 150));
            deck.ajouterCarte(new Creature("ogre", 200));
            deck.ajouterCarte(new Creature("monstre", 310));
            deck.ajouterCarte(new Creature("tortue", 400));
            deck.ajouterCarte(new Sort("Abracadabra", 130));
            deck.ajouterCarte(new Piege("piege a ours!", 100));
            deck.ajouterCarte(new Creature("rat", 150));
            deck.ajouterCarte(new Creature("pikachu", 350));
            deck.ajouterCarte(new Creature("dragon", 300));
            deck.ajouterCarte(new Creature("archer", 150));
            /*Petit coucou pour etre sur de rentrer ici*/
            Console.WriteLine("Bonjour " + pseudo + ". Voulez vous jouer un peu?");
        }

        /* l'initialisation se fait lorsque la partie se lance, pas avant sinon on parle d'une autre étape d'initialisation*/
        public void initialisation()
        {
            energieSpirituelle = energieSpirituelleMax;
            crypte = new Crypte("", crypte.zonepoint,crypte.map);
            hand = new Hand();
            cimetiere = new Cimetiere();
            pioche = new Pioche();
            hero = new Hero("PANDA", energieSpirituelleMax - 100);

            /*On transfere les cartes du deck dans la pioche */
            pioche.setListeCarte(deck.getListeCarte());
            pioche.setListePiege(deck.getListePiege());
            pioche.setListeCreature(deck.getListeCreature());
            pioche.setListeSort(deck.getListeSort());
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
            hero = new Hero("PandaOUF", 500);

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
            Console.WriteLine("*** Vous piochez ***");

            if (pioche.getListeCarte()[0].GetType().Name == "Creature")
            {
                hand.ajouterCarte(pioche.piocherCreature());
            }
            else if (deck.getListePiege().Contains(pioche.getListeCarte()[0]))
            {
                hand.ajouterCarte(pioche.piocherPiege());
            }
            else if (deck.getListeSort().Contains(pioche.getListeCarte()[0]))
            {
                hand.ajouterCarte(pioche.piocherSort());
            }
        }

        /*On invoquer une carte en donnant de l'ES*/
        public Pion invoquer()
        {
            string compteur;
            Pion pion;
            Creature carte;
            Console.WriteLine("Vous voulez invoquer quelle carte ?");

            //hand.afficherCreature("=== Votre main ===");
            compteur = Console.ReadLine();
            carte = hand.getListeCreature()[int.Parse(compteur) - 1];
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
                pion.setCreature(carte);
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
    class Phase
    {
        public void afficher(Tour t)
        {
            Console.WriteLine(t.getNom());
        }
    }

    [Serializable]
    class Tour
    {
        string nom = "tour";
        public Tour()
        {
        }

        public string getNom()
        {
            return this.nom;
        }
    }
}
