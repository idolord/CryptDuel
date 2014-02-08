using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
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
}
