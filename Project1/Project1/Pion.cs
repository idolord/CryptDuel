using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
    class Pion
    {
        private int posX;
        private int posY;
        private bool estInvoque;
        private bool estVisible;
        private int sens;
        private Creature creature;
        private bool estHero = false;

        public Pion(bool estVisible)
        {
            estInvoque = true;
            this.estVisible = estVisible;
            Console.WriteLine("Pion crée");
        }

        /*Invocation du pion */
        public Pion(int x, int y)
        {
            posX = x;
            posY = y;
            estInvoque = true;
            Console.WriteLine("Pion crée");
        }

        public void setEstHero(bool estHero)
        {
            this.estHero = estHero;
        }

        public bool getEstHero()
        {
            return this.estHero;
        }

        public void setPosX(int x)
        {
            this.posX = x;
        }

        public int getPosX()
        {
            return this.posX;
        }

        public void setPosY(int y)
        {
            this.posY = y;
        }

        public int getPosY()
        {
            return this.posY;
        }

        public void setCreature(Creature creature)
        {
            this.creature = creature;
        }

        public Creature getCreature()
        {
            return this.creature;
        }

        public void setEstInvoque(bool b)
        {
            this.estInvoque = b;
        }

        public void setEstVisible(bool b)
        {
            this.estVisible = b;
        }

        public bool getEstVisible()
        {
            return this.estVisible;
        }

        public bool getEstInvoque()
        {
            return this.estInvoque;
        }

        public void setSens(int sens)
        {
            this.sens = sens;
        }

        public int getSens()
        {
            return this.sens;
        }


    }
}
