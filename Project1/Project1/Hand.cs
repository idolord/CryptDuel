using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
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

        public override  void ajouterCarte(Creature uneCreature)
        {
            if (listeCarte.Count > nombreLimite || nombreLimite == -1)
            {
                listeCarte.Add(uneCreature);
                listeCreature.Add(uneCreature);
                calculCoutMini();
            }
        }

        public override  void ajouterCarte(Piege unPiege)
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

        public override  void retirerCarte(Entite uneCarte)
        {
            if (listeCarte.Contains(uneCarte))
            {
                listeCarte.Remove(uneCarte);
                this.nombreCarte--;
                calculCoutMini();
            }
        }

        public override  void retirerCarte(Creature uneCreature)
        {
            if (listeCarte.Contains(uneCreature) && listeCreature.Contains(uneCreature))
            {
                listeCarte.Remove(uneCreature);
                listeCreature.Remove(uneCreature);
                calculCoutMini();
            }
        }

        public override  void retirerCarte(Piege unPiege)
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
}
