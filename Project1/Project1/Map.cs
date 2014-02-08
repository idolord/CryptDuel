using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
    class Map
    {

        protected List<List<Tuile>> tuileMap;
        protected List<Tuile> ligne;

        //Le nombre de tuile en x et en y
        protected int nbX;
        protected int nbY;
        /*protected int posX;
        protected int posY;
        private Arene arene;
        private Crypte crypteNord;
        private Crypte crypteSud;
        private Crypte crypteEst;
        private Crypte crypteOuest;*/

        const int TAILLE_ZONE = 3;

        public Map()
        {
            tuileMap = new List<List<Tuile>>();
            ligne = new List<Tuile>();

            /*On initialise les zones qui seront dans la map avec une taille fixe et prédéfini*/
            /*arene = new Arene(TAILLE_ZONE, TAILLE_ZONE);
            crypteNord = new Crypte(TAILLE_ZONE, TAILLE_ZONE);
            crypteSud = new Crypte(TAILLE_ZONE, TAILLE_ZONE);
            crypteEst = new Crypte(TAILLE_ZONE, TAILLE_ZONE);
            crypteOuest = new Crypte(TAILLE_ZONE, TAILLE_ZONE);
             * */
            genererMap();
            afficherMap();
        }

        /*Double boucle pour créer une matrice a 2 dim, affichage sur console*/
        /*les zones s'adaptent suivant TAILLE_ZONE et la aussi */
        /*public void genererMap()
        {
            int tailleMap = TAILLE_ZONE * 3;


            for (int i = 0; i < tailleMap; i++)
            {
                for (int j = 0; j < tailleMap; j++)
                {
                    /* Crypte du nord */
                  /*  if (((j >= (tailleMap / 2) - 1) && (j <= ((tailleMap) - TAILLE_ZONE) - 1)) && (i >= 0 && i <= TAILLE_ZONE - 1))
                    {
                        Console.Write(" N ");
                    }*/

                    /* Crypte du sud */
                   /* else if (((j >= (tailleMap / 2) - 1) && (j <= ((tailleMap) - TAILLE_ZONE) - 1)) && (i >= tailleMap - TAILLE_ZONE && i <= tailleMap))
                    {
                        Console.Write(" S ");
                    }*/

                    /* Crypte du ouest */
                  /*  else if (((i >= (tailleMap / 2) - 1) && (i <= ((tailleMap) - TAILLE_ZONE) - 1)) && (j >= 0 && j <= TAILLE_ZONE - 1))
                    {
                        Console.Write(" O ");
                    }*/

                    /* Crypte du est */
                  /*  else if (((i >= (tailleMap / 2) - 1) && (i <= ((tailleMap) - TAILLE_ZONE) - 1)) && (j >= tailleMap - TAILLE_ZONE && j <= tailleMap))
                    {
                        Console.Write(" E ");
                    }*/
                    /* Arene */
                   /* else if (((j >= (tailleMap / 2) - 1) && (j <= ((tailleMap) - TAILLE_ZONE) - 1)) && ((i >= (tailleMap / 2) - 1) && (i <= ((tailleMap) - TAILLE_ZONE) - 1)))
                    {
                        Console.Write(" A ");
                    }
                    else
                    {
                        Console.Write(" R ");
                    }

                }
                Console.WriteLine();
            }
        }*/

        public List<Pion> pionAdjacent(Pion pion)
        {
            int x = pion.getPosX();
            int y = pion.getPosY();
            List<Pion> pionAdjacent = new List<Pion>();
            Tuile tuileNord = tuileMap[y - 1][x];
            Tuile tuileSud = tuileMap[y + 1][x];
            Tuile tuileEst = tuileMap[y][x + 1];
            Tuile tuileOuest = tuileMap[y][x - 1];
            string typeNord = tuileNord.getType();
            string typeSud = tuileSud.getType();
            string typeEst = tuileEst.getType();
            string typeOuest = tuileOuest.getType();
            /*
            Console.WriteLine("Au nord il y a : une tuile type " + typeNord);
            Console.WriteLine("Au sud il y a : une tuile type " + typeSud);
            Console.WriteLine("A l'est il y a : une tuile type " + typeEst);
            Console.WriteLine("A l'ouest il y a : une tuile type " + typeOuest);
             * */
            if (typeNord == "pion")
            {
                pionAdjacent.Add(tuileNord.getPion());
            }
            if (typeSud == "pion")
            {
                pionAdjacent.Add(tuileSud.getPion());
            }
            if (typeEst == "pion")
            {
                pionAdjacent.Add(tuileEst.getPion());
            }
            if (typeOuest == "pion")
            {
                pionAdjacent.Add(tuileOuest.getPion());
            }

            Console.WriteLine("Pion adjacent : " + pionAdjacent.Count());

            return pionAdjacent;
            
        }//FIN ADJACENT



        /*Pour l'attaque on fait confiance au autre*/
        public void attaquer(int direction, Pion pion)
        {
            int x = pion.getPosX();
            int y = pion.getPosY();
            Console.WriteLine("x : " + x + " y : " + y);
            if (direction == 1)
            {
                Console.WriteLine("A l'attaque ! nord " + tuileMap[y - 1][x].getType());
                tuileMap[y - 1][x] = new Tuile(x, y - 1, "empty");
            }
            else if (direction == 2)
            {
                Console.WriteLine("A l'attaque ! sud " + tuileMap[y + 1][x].getType());
                tuileMap[y + 1][x] = new Tuile(x, y + 1, "empty");
            }
            else if (direction == 3)
            {
                Console.WriteLine("A l'attaque ! est " + tuileMap[y][x + 1].getType());
                tuileMap[y][x + 1] = new Tuile(x + 1, y, "empty");
            }
            else if (direction == 4)
            {
                Console.WriteLine("A l'attaque ! ouest " + tuileMap[y][x - 1].getType());
                tuileMap[y][x - 1] = new Tuile(x - 1, y, "empty");
            }

            Console.WriteLine("fin de l'attaque");
            afficherMap();

        }//FIN ATTAQUER


        public bool deplacer(int direction, Pion pion)
        {
            int x = pion.getPosX();
            int y = pion.getPosY();
            Tuile tuile = new Tuile(x, y, "empty");            
            tuile.setPion(null);
            Tuile tuilePion = new Tuile("pion");
            if (direction == 1 && tuileMap[y - 1][x].getType() == "empty")
            {
                Console.WriteLine("Vers le nord !");
                tuileMap[y][x] = tuile;
                pion.setPosX(x);
                pion.setPosY(y - 1);
                tuilePion.setPion(pion);
                tuilePion.setPosX(x);
                tuilePion.setPosY(y - 1);
                tuileMap[y - 1][x] = tuilePion;
            }
            else if (direction == 2 && tuileMap[y + 1][x].getType() == "empty")
            {
                Console.WriteLine("Vers le sud !"); tuileMap[y][x] = tuile;
                pion.setPosX(x);
                pion.setPosY(y + 1);
                tuilePion.setPion(pion);
                tuilePion.setPosX(x);
                tuilePion.setPosY(y + 1);
                tuileMap[y + 1][x] = tuilePion;
            }
            else if (direction == 3 && tuileMap[y][x + 1].getType() == "empty")
            {
                Console.WriteLine("Vers l'est !");
                tuileMap[y][x] = tuile;
                pion.setPosX(x + 1);
                pion.setPosY(y);
                tuilePion.setPion(pion);
                tuilePion.setPosX(x + 1);
                tuilePion.setPosY(y);
                tuileMap[y][x + 1] = tuilePion;
            }
            else if (direction == 4 && tuileMap[y][x - 1].getType() == "empty")
            {
                Console.WriteLine("Vers l'ouest !");
                tuileMap[y][x] = tuile;
                pion.setPosX(x - 1);
                pion.setPosY(y);
                tuilePion.setPion(pion);
                tuilePion.setPosX(x - 1);
                tuilePion.setPosY(y);
                tuileMap[y][x - 1] = tuilePion;
            }
            else
            {
                return false;
            }

            return true;

        }//FIN DEPLACER


        /*TEST*/
        public void afficherMap()
        {
            Tuile temp;
            for (int i = 0; i < tuileMap.Count(); i++)
            {
                Console.Write(" " + (i + 1) + " ");
            }
            Console.WriteLine();
            for (int i = 0; i < tuileMap.Count(); i++)
            {
                for (int j = 0; j < tuileMap.Count(); j++)
                {
                    temp = tuileMap[i][j];

                    if (temp.getType() == "empty")
                    {
                        Console.Write(" E ");
                    }
                    else if (temp.getType() == "pion")
                    {

                        if (temp.getPion().getEstHero())
                        {
                            Console.Write(" P ");
                        }
                        else if (temp.getPion().getEstVisible())
                        {
                            Console.Write(" H ");
                        }
                        else
                        {
                            Console.Write(" ? ");
                        }
                    }
                    else if (temp.getType() == "mur")
                    {
                        Console.Write(" M ");
                    }
                    else if (temp.getType() == "colonne")
                    {
                        Console.Write(" C ");
                    }
                    else if (temp.getType() == "tombeau")
                    {
                        Console.Write(" T ");
                    }
                }
                Console.WriteLine();
            }

        }

        public void genererMap()
        {
            List<Tuile> temp = new List<Tuile>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    temp.Add(new Tuile(j, i, "empty"));
                    
                }
                tuileMap.Add(temp);
                temp = new List<Tuile>();
            }
        }

        public int pionSens(Pion pion)
        {
                Console.Write("Le sens du pion est vers ");
            int sens = pion.getSens();
                //nord
            if (sens == 1)
            {
                Console.WriteLine("le nord");
            }
            else if (sens == 2)
            {
                Console.WriteLine("le sud");
            }
            else if (sens == 3)
            {
                Console.WriteLine("l'est");
            }
            else if (sens == 4)
            {
                Console.WriteLine("l'ouest");
            }

            return sens;
        }

        //Remplace ou modifie une case de la zone suivant les coordonnées locales de la zone
        public bool remplirTuile(Tuile tuile)
        {
            int x = tuile.getPosX();
            int y = tuile.getPosY();
            /*
            if (tuile.getType() == "pion")
            {
                tuile.ajouterPion(new Pion(x, y));
            }
            */
            if (tuileMap[y][x].getType() == "empty" && x != 0 && y != 0)
            {
                tuileMap[y][x] = tuile;
                return true;
            }
                //Pour les murs et autres éléments
            else if (tuile.getType() != "pion")
            {
                tuileMap[y][x] = tuile;
                return true;
            }
            else
            {
                Console.WriteLine("La tuile n'est pas vide ou est en bordure, impossible de placer votre pion !");
                return false;
            }
        }

        /*l'init de base */
        protected void init(int x, int y)
        {
            nbX = x;
            nbY = y;
            tuileMap = new List<List<Tuile>>();

            effaceTout();
        }

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
        
        /*Remplie ou remplace une colonne*/
        public void remplirColonne(Tuile[] listeTuile, int numeroColonne)
        {
            if ((listeTuile.Count() == nbY) && ((numeroColonne >= 0) || (numeroColonne <= nbX)))
            {
                for (int i = 0; i < nbY; i++)
                {
                    ligne.Insert(numeroColonne, listeTuile[i]);
                    tuileMap.Insert(i, ligne);
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
                    ligne.Insert(numeroColonne, listeTuile[i]);
                    tuileMap.Insert(i, ligne);
                }
            }
        }

        /*Remplie ou remplace une ligne*/
        public void remplirLigne(Tuile[] listeTuile, int numeroLigne)
        {
            if((listeTuile.Count() == nbY) && ((numeroLigne >= 0) || (numeroLigne <= nbY))){
                for (int i = 0; i < nbX; i++)
                {
                    ligne.Insert(i, listeTuile[i]);
                }
                tuileMap.Insert(numeroLigne, ligne);
            }
        }

        /*Surchage pour les listes*/
        public void remplirLigne(List<Tuile> listeTuile, int numeroLigne)
        {
            if ((listeTuile.Count() == nbY) && ((numeroLigne >= 0) || (numeroLigne <= nbY)))
            {
                for (int i = 0; i < nbX; i++)
                {
                    ligne.Insert(i, listeTuile[i]);
                }
                tuileMap.Insert(numeroLigne, ligne);
            }
        }

        /*Effacer tout signifie tout remplacer pas du neuf*/
        public void effaceTout()
        {
            for (int i = 0; i < nbY; i++)
            {
                for (int j = 0; j < nbX; j++)
                {
                    ligne.Insert(j, new Tuile(j,i));
                }
                tuileMap.Insert(i, ligne);
            }
        }

        /*On spécifie une colonne*/
        public void effaceColonne(int numeroColonne)
        {
            if((numeroColonne >= 0) || (numeroColonne <= nbX)){
                for (int i = 0; i < nbY; i++)
                {
                    ligne.Insert(numeroColonne, new Tuile(i, numeroColonne));
                    tuileMap.Insert(i, ligne); 
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
                    ligne.Insert(i, new Tuile(numeroLigne, i));
                }
                tuileMap.Insert(i, ligne); 
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
