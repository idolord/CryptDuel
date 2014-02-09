using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Worldbuilder
{

    //entity familiy
    [Serializable]
    public class Entite
    {
        protected string nom;
        protected int cout;
        protected string description;
        protected string type;
        protected Carte carte;


        public Entite(string nom, int cout, string type)
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
    public class Sort : Entite
    {
        public Sort(string nom, int cout, string type)
            : base(nom, cout, type)
        {
        }
    }

    [Serializable]
    public class Piege : Entite
    {
        public Piege(string nom, int cout, string type)
            : base(nom, cout, type)
        {
        }
    }

    [Serializable]
    public class Creature : Entite
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

        public Creature(string nom, int cout, string type)
            : base(nom, cout, type)
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
    public class Hero : Creature
    {
        /*les attributs réels de la créature*/
        private int niveau;
        private int attaqueParNiveau;
        private int defenseParNiveau;
        private int retaliationParNiveau;
        private int duelScienceParNiveau;

        public Hero(string nom, int cout, string type)
            : base(nom, cout, type)
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
    public class GestionCarte
    {
        /*Afin de bien différencier le type de carte,
         * on fait une liste de chaque, 
         * pour ensuite avoir les info et les action qui vont bien avec*/
        protected List<Entite> listeCarte = new List<Entite>();

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


        /*On retire une carte de(s) la liste + surcharge des enfants */

        public virtual void retirerCarte(Entite uneCarte)
        {
            if (listeCarte.Contains(uneCarte))
            {
                listeCarte.Remove(uneCarte);
                this.nombreCarte--;
            }
        }

    }


    [Serializable]
    public class Hand : GestionCarte
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

    }

    [Serializable]
    public class Cimetiere : GestionCarte
    {
        public Cimetiere()
        {
        }
    }

    [Serializable]
    public class Deck : GestionCarte
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
    public class Pioche : Deck
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

    }





    [Serializable]
    public class Carte
    {
        private string nom;
        private int cout;
        private string description;

        /*pour l'init*/
        public Carte(string nom, int cout, string description)
        {
            this.nom = nom;
            this.cout = cout;
            this.description = description;
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
    public class Layout
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


}
