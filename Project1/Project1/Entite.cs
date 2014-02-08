using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
    class Entite
    {
        protected string nom;
        protected int cout;
        protected string description;

        protected Carte carte;


        public Entite(string nom, int cout)           
        {
            this.nom = nom;
            this.cout = cout;
            carte = new Carte(nom, cout, description);
        }

        public void setNom(string nom)
        {
            this.nom = nom;
        }

        public void setCout(int cout)
        {
            this.cout = cout;
        }

        public void setDescription(string description)
        {
            this.description = description;
        }

        public string getNom()
        {
            return this.nom;
        }

        public int getCout()
        {
            return this.cout;
        }

        public string getDescription()
        {
            return this.description;
        }
    }
}
