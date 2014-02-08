using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
    class Tombeau
    {
        /*la position du tombeau depend de la crypte */
        private int posX;
        private int posY;

        public Tombeau() { }

        /*ce constructeur vient avec la crypte*/
        public Tombeau(int x, int y)
        {
            this.posX = x;
            this.posY = y;
        }

    }
}
