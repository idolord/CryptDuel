using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Worldbuilder
{
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
    class Carte
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
}
