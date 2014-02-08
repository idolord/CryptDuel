using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
    class Pioche : Deck
    {
        

        public void melanger(List<Entite> listeCarte)
        {
            Random random = new Random();
            int taille = listeCarte.Count;
            int chiffreRandom;
            List<Entite> tempPioche = new List<Entite>();

            for (int i = 0; i < taille; i++)
            {
                tempPioche.Add(listeCarte[i]);
            }

            List<int> melange = new List<int>();
            while (melange.Count < taille)
            {
                chiffreRandom = random.Next(0, taille);
                if (!(melange == null || melange.Contains(chiffreRandom)) && melange.Count != chiffreRandom)
                {
                    melange.Add(chiffreRandom);
                    //Console.WriteLine(chiffreRandom);
                }

            }

            for (int i = 0; i < taille; i++)
            {
                listeCarte[i] = tempPioche[melange[i]];
            }

        }


        public Entite piocher()//on considere que la premiere carte est celle du dessus
        {
            Entite carte;

            carte = listeCarte[0];
            listeCarte.RemoveAt(0);

            return carte;
        }

        public Creature piocherCreature()//on considere que la premiere carte est celle du dessus
        {
            Creature carte;

            carte = listeCreature[0];
            listeCreature.RemoveAt(0);
            listeCarte.RemoveAt(0);

            return carte;
        }

        public Sort piocherSort()//on considere que la premiere carte est celle du dessus
        {
            Sort carte;

            carte = listeSort[0];
            listeSort.RemoveAt(0);
            listeCarte.RemoveAt(0);

            return carte;
        }

        public Piege piocherPiege()//on considere que la premiere carte est celle du dessus
        {
            Piege carte;

            carte = listePiege[0];
            listePiege.RemoveAt(0);
            listeCarte.RemoveAt(0);

            return carte;
        }
    }
}
