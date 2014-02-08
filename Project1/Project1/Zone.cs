using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
    class Zone
    {
        protected List<List<Tuile>> ordonnee;
        protected List<Tuile> abscisse;
        //Le nombre de tuile en x et en y
        protected int nbX;
        protected int nbY;
        protected int posX;
        protected int posY;


        public Zone(int x, int y)
        {
            init(x, y);
        }

        public Zone(int x, int y, int posX, int posY)
        {
            this.posX = posX;
            this.posY = posY;
            init(x, y);
        }

        /*fonction d'initialisation*/
        protected void init(int x, int y)
        {
            nbX = x;
            nbY = y;
            ordonnee = new List<List<Tuile>>();
            abscisse = new List<Tuile>();

            effaceTout();
        }


        /*Fonction de base sur la manipalation des doubles listes */


        //On rajoute une ligne a la zone
        public void ajouterLigne()
        {
            nbY = nbY + 1;
            init(nbX, nbY);
        }

        //On rajoute une colonne a la zone
        public void ajouterColonne()
        {
            nbX = nbX + 1;
            init(nbX, nbY);
        }

        //Remplace ou modifie une case de la zone suivant les coordonnées locales de la zone
        public void remplirTuile(Tuile tuile, int x, int y)
        {
            abscisse.Insert(x, tuile);
            ordonnee.Insert(y, abscisse);
        }


        /*Remplie ou remplace une colonne*/
        public void remplirColonne(Tuile[] listeTuile, int numeroColonne)
        {
            if ((listeTuile.Count() == nbY) && ((numeroColonne >= 0) || (numeroColonne <= nbX)))
            {
                for (int i = 0; i < nbY; i++)
                {
                    abscisse.Insert(numeroColonne, listeTuile[i]);
                    ordonnee.Insert(i, abscisse);
                }
            }
        }

        /*Surchage pour les listes*/
        public void remplirColonne(List<Tuile> listeTuile, int numeroColonne)
        {
            if ((listeTuile.Count() == nbY) && ((numeroColonne >= 0) || (numeroColonne <= nbX)))
            {
                for (int i = 0; i < nbY; i++)
                {
                    abscisse.Insert(numeroColonne, listeTuile[i]);
                    ordonnee.Insert(i, abscisse);
                }
            }
        }

        /*Remplie ou remplace une ligne*/
        public void remplirLigne(Tuile[] listeTuile, int numeroLigne)
        {
            if ((listeTuile.Count() == nbY) && ((numeroLigne >= 0) || (numeroLigne <= nbY)))
            {
                for (int i = 0; i < nbX; i++)
                {
                    abscisse.Insert(i, listeTuile[i]);
                }
                ordonnee.Insert(numeroLigne, abscisse);
            }
        }

        /*Surchage pour les listes*/
        public void remplirLigne(List<Tuile> listeTuile, int numeroLigne)
        {
            if ((listeTuile.Count() == nbY) && ((numeroLigne >= 0) || (numeroLigne <= nbY)))
            {
                for (int i = 0; i < nbX; i++)
                {
                    abscisse.Insert(i, listeTuile[i]);
                }
                ordonnee.Insert(numeroLigne, abscisse);
            }
        }

        public void effaceTout()
        {
            for (int i = 0; i < nbY; i++)
            {
                for (int j = 0; j < nbX; j++)
                {
                    abscisse.Insert(j, new Tuile("empty"));
                }
                ordonnee.Insert(i, abscisse);
            }
        }

        public void effaceColonne(int numeroColonne)
        {
            if ((numeroColonne >= 0) || (numeroColonne <= nbX))
            {
                for (int i = 0; i < nbY; i++)
                {
                    abscisse.Insert(numeroColonne, new Tuile("empty"));
                    ordonnee.Insert(i, abscisse);
                }
            }
        }

        public void effacePremiereColonne()
        {
            effaceColonne(0);
        }

        public void effaceDerniereColonne()
        {
            effaceColonne(nbX);
        }

        public void effaceLigne(int numeroLigne)
        {
            if ((numeroLigne >= 0) || (numeroLigne <= nbY))
            {
                int i;
                for (i = 0; i < nbY; i++)
                {
                    abscisse.Insert(i, new Tuile("empty"));
                }
                ordonnee.Insert(i, abscisse);
            }
        }

        public void effacePremiereLigne()
        {
            effaceLigne(0);
        }

        public void effaceDerniereLigne()
        {
            effaceLigne(nbY);
        }


    }
}
