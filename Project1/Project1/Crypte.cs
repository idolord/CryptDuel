using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
    class Crypte : Zone
    {
        private Tombeau tombeau;
        //La dimension de la crypte doit etre imposé avant
        public Crypte(int x, int y) : base(x, y)
        {
            tombeau = new Tombeau();
        }

        /*suivant la direction choisit on deplace le tout d'une case; on vérifie qu'aucune case ne touche un mur*/
        public void deplacerCrypte(int direction)
        {
            if (direction == 1)
            {
                Console.WriteLine("La crypte s'est déplacé vers le nord");
            }
            else if (direction == 2)
            {
                Console.WriteLine("La crypte s'est déplacé vers le sud");
            }
            else if (direction == 3)
            {
                Console.WriteLine("La crypte s'est déplacé vers l'est");
            }
            else if (direction == 4)
            {
                Console.WriteLine("La crypte s'est déplacé vers l'ouest");
            }
        }

        public Tombeau getTombeau()
        {
            return this.tombeau;
        }


    }
}
