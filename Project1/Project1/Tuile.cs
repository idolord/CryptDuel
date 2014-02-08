using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
    class Tuile
    {
        private int posX;
        private int posY;
        private string type;

        Pion pion;
        ZoneEffet zoneEffet;
        

        public Tuile()
        {
        }

        public Tuile(string type)
        {
            this.type = type;
        }
        
        public Tuile(int x, int y)
        {
            this.posX = x;
            this.posY = y;
        }

        public Tuile(int x, int y, string type)
        {
            this.posX = x;
            this.posY = y;
            this.type = type;
        }

        public void ajouterPion(Pion unPion)
        {
            this.pion = unPion;
        }

        public void ajouterCoordonne(int x, int y)
        {
            posX = x;
            posY = y;
        }

        public void ajouterZoneEffet(ZoneEffet uneZoneEffet)
        {
            this.zoneEffet = uneZoneEffet;
        }

        public void supprimerZoneEffet()
        {
            this.zoneEffet = null;
        }

        public void supprimerPion()
        {
            this.pion = null;
        }

        public void setType(string type)
        {
            this.type = type;
        }

        public string getType()
        {
            return this.type;
        }

        public void setPosY(int y)
        {
            this.posY = y;
        }

        public int getPosY()
        {
            return this.posY;
        }

        public void setPosX(int x)
        {
            this.posX = x;
        }

        public int getPosX()
        {
            return this.posX;
        }
        public void setPion(Pion pion)
        {
            this.pion = pion;
        }

        public Pion getPion()
        {
            return this.pion;
        }

    }
}
