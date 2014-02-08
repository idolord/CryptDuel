using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
    class Carte
    {
        private string nom;
        private int cout;
        private string description;

        /*pour l'init*/
        public Carte(string nom, int cout, string description)
        {
            this.nom = nom;
            this.cout = cout;
            this.description = description;
        }

        /*init de test*/
        public Carte()
        {
            
        }

        public void afficherCarte()
        {
            Console.WriteLine("===================");
            Console.WriteLine("=== "+nom+" ===");
            Console.WriteLine("=== " + cout + " ===");
            Console.WriteLine("=== " + description + " ===");
            Console.WriteLine("===================");
        }
        


        

    }
}
