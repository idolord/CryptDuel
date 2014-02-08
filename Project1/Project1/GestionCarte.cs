using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
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
        protected string[] type = { "creature", "carte", "sortilege", "piege"};

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

        public virtual void afficher(string presentation)
        {
            Console.WriteLine(presentation);
            int i = 1;
            foreach (Entite carte in listeCarte)
            {
                Console.Write(i + ". ");
                if (listeCreature.Contains(carte))
                {
                    Console.Write(type[0]);
                }
                else if (listeSort.Contains(carte))
                {
                    Console.Write(type[2]);
                }
                else if (listePiege.Contains(carte))
                {
                    Console.Write(type[3]);
                }
                else
                {
                    Console.Write(type[1]);
                }

                Console.Write(" - ");
                Console.Write(carte.getNom());
                Console.WriteLine();
                i++;
            }
            Console.WriteLine();
        }

        public void afficher()
        {
            int i = 1;
            foreach (Entite carte in listeCarte)
            {
                Console.Write(i + ". ");
                if (listeCreature.Contains(carte))
                {
                    Console.Write(type[0]);
                }
                else if (listeSort.Contains(carte))
                {
                    Console.Write(type[2]);
                }
                else if (listePiege.Contains(carte))
                {
                    Console.Write(type[3]);
                }
                else
                {
                    Console.Write(type[1]);
                }

                Console.Write(" - ");
                Console.Write(carte.getNom());
                Console.Write(" - ");
                Console.Write(carte.getCout());
                Console.WriteLine();
                i++;
            }
            Console.WriteLine();
        }//AFFICHER

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
}
