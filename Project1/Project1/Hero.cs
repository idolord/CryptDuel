using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
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
}
