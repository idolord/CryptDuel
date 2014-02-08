using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1
{
    class Test
    {
        /*A la base ce sont des fonctions sur la pioche */
        static void Main(string[] args)
        {


            /*
             * La pioche ici*/
             /* 
            List<string> carte = new List<string> { "rat", "ogre", "vache", "chat", "chien", "hero", "archer" };
            string cartePiochee = null;

            afficherPioche(carte);

            Console.WriteLine("=========================================");
            
            melanger(carte);

            afficherPioche(carte);

            Console.WriteLine("=========================================");

            cartePiochee = piocher(carte);

            Console.WriteLine(cartePiochee);

            Console.WriteLine("=========================================");
            cartePiochee = piocher(carte);

            Console.WriteLine(cartePiochee);

            Console.WriteLine("=========================================");
            afficherPioche(carte);

            Console.WriteLine("=========================================");
             * 
             * 
             */










            /*
             * on test le layout*/
            /*Layout layout;
            Entite e = new Entite("yo");
            layout = e.getLayout();
            layout.genererLayout();
            */



            
        




            /*Test sur pertinence de la relation d'héritage et sur l'override*/
            /*GestionCarte g = new GestionCarte();
            Hand h = new Hand(6);
            
            Console.WriteLine(g.getNombreLimite());
            Console.WriteLine(h.getNombreLimite());
            */

            /*test de la map et d'inclusion des cryptes, arene + tuile vide BEsoin de décommenter dans la classe MAP pour mettre en évidence le test*/
            Map map = new Map();

            Tuile tuile = new Tuile();

            mapGestion(map, tuile);



            /* LE SYSTEME MENU + CARTE EST FONCTIONNEL */
            /*Dans ce test, on va prétendre s'occuper de la gestion du tour par tour*/


            Joueur j1 = new Joueur("Panda");
            Joueur j2 = new Joueur("Ido");

            joueurTour(map, tuile, j1);
            joueurTour(map, tuile, j2);

            while (j1.getEnergieSpirituelleActuelle() > 0 || j2.getEnergieSpirituelleActuelle() > 0)
            {
                joueurTour(map, tuile, j1);
                joueurTour(map, tuile, j2);
            }



            Console.ReadLine();
        }//MAIN

        public static void joueurTour(Map map, Tuile tuile, Joueur j)
        {
            /*on lance une partie*/
            j.initialisation();

            /*tout est installé ...*/


            /* A notre tour de jouer youpi */

            Console.WriteLine("C'est au tour de " + j.getPseudo());

            /*Des tests des différents cas grace au variable booleenne*/
            
            
            string choixShift;
            string choixRecycle;
            string choixRecycleAction;
            string choixInvoque;

            Console.WriteLine("=========================");
            Console.WriteLine("****** SHIFT PHASE ******");
            Console.WriteLine("=========================");

            Console.WriteLine("1. Ok");
            Console.WriteLine("2. Je passe");

            choixShift = Console.ReadLine();
            
            if (choixShift == "1")
            {
	            j.crypter();
	            j.finirTour();
	        }
            else
            {
                Console.WriteLine("===========================");
                Console.WriteLine("****** RECYCLE PHASE ******");
                Console.WriteLine("===========================");

                Console.WriteLine("1. Ok");
                Console.WriteLine("2. Je passe");
                choixRecycle = Console.ReadLine(); 

                if (choixRecycle == "1")
                {
                    bool encore = false;
                    j.recycler();
                    Console.WriteLine("1. Je shift");
                    Console.WriteLine("2. Je ressucite mon hero");
                    Console.WriteLine("3. Je joue 2 fois tiens !");
                    choixRecycleAction = Console.ReadLine();

                    if (choixRecycleAction == "1")
                    {
			            j.crypter();
                    }
                    else if (choixRecycleAction == "2")
                    {
                        ressuciterHeroGestion(map, tuile, j);
                    }
                    else if (choixRecycleAction == "3")
                    {
                        encore = true;
                    }
                    if (j.getEnergieSpirituelleActuelle() > j.getHand().getCoutMini())
                    {
                        Console.WriteLine("==========================");
                        Console.WriteLine("****** SUMMON PHASE ******");
                        Console.WriteLine("==========================");

                        Console.WriteLine("1. Ok");
                        Console.WriteLine("2. Je passe");
                        choixInvoque = Console.ReadLine();

                        if (choixInvoque == "1")
                        {
                            invoquerGestion(map, tuile, j);
                        }
                    }
                    actionBase(map, tuile, j);
                    if (encore)
                        actionBase(map, tuile, j);
                    j.finirTour();
                }
                else
                {

                    if (j.getEnergieSpirituelleActuelle() > j.getHand().getCoutMini())
                    {
                        Console.WriteLine("==========================");
                        Console.WriteLine("****** SUMMON PHASE ******");
                        Console.WriteLine("==========================");

                        Console.WriteLine("1. Ok");
                        Console.WriteLine("2. Je passe");
                        choixInvoque = Console.ReadLine();

                        if (choixInvoque == "1")
                        {
                            invoquerGestion(map, tuile, j);
                        }
                    }
                    actionBase(map, tuile, j); 
                    j.piocher();
                    j.finirTour();
                }
            }
        }//FIN PLAYERTURN



        public static void invoquerGestion(Map map, Tuile tuile, Joueur j)
        {

            int x, y;
            string choixX;
            string choixY;
            bool flag = false;
            bool continuer = true;
            Pion pion;
            string compteur;

            //Tant que le joueur veut continuer a invoquer et si il a l'énergie pour on continue
            while (continuer && j.getEnergieSpirituelleActuelle() > j.getHand().getCoutMini())
            {

                pion = j.invoquer();

                if (pion != null)
                {
                    //Tant que la position du pion n'est pas valide on boucle
                    while (!flag)
                    {
                        map.afficherMap();
                        /*Je summon un pion*/
                        Console.WriteLine("=== INVOCATION ===");
                        Console.WriteLine("Donnez un x et un y pour invoquer votre pion");

                        Console.Write("x : ");
                        choixX = Console.ReadLine();
                        Console.Write("y : ");
                        choixY = Console.ReadLine();

                        if ((int.Parse(choixX)) <= 1)
                            x = 0;
                        else
                            x = int.Parse(choixX) - 1;

                        if ((int.Parse(choixY)) <= 1)
                            y = 0;
                        else
                            y = int.Parse(choixY) - 1;
                        tuile = new Tuile(x, y, "pion");
                        pion.setPosX(x);
                        pion.setPosY(y);
                        tuile.ajouterPion(pion);

                        flag = map.remplirTuile(tuile);
                    }

                    map.afficherMap();
                }

                if (j.getEnergieSpirituelleActuelle() > j.getHand().getCoutMini() && j.getHand().getListeCarte().Count > 0)
                {
                    Console.WriteLine("voulez vous continuer à invoquer ?");
                    Console.WriteLine("1. Oui         2. Non");
                    compteur = Console.ReadLine();
                    if (compteur == "1")
                        continuer = true;
                    else if (compteur == "2")
                        continuer = false;
                }
                else
                {
                    continuer = false;
                }

                

            }
        }//INVOCATION GESTION


        public static void ressuciterHeroGestion(Map map, Tuile tuile, Joueur j)
        {
            Pion pion = j.ressuciterHero();
            bool flag = false;
            string choixX;
            string choixY;
            int x, y;

            if (pion != null)
            {
                //Tant que la position du pion n'est pas valide on boucle
                while (!flag)
                {
                    map.afficherMap();
                    /*Je summon un pion*/
                    Console.WriteLine("=== INVOCATION ===");
                    Console.WriteLine("Donnez un x et un y pour invoquer votre pion");

                    Console.Write("x : ");
                    choixX = Console.ReadLine();
                    Console.Write("y : ");
                    choixY = Console.ReadLine();

                    if ((int.Parse(choixX)) <= 1)
                        x = 0;
                    else
                        x = int.Parse(choixX) - 1;

                    if ((int.Parse(choixY)) <= 1)
                        y = 0;
                    else
                        y = int.Parse(choixY) - 1;
                    tuile = new Tuile(x, y, "hero");
                    pion.setPosX(x);
                    pion.setPosY(y);
                    tuile.ajouterPion(pion);

                    flag = map.remplirTuile(tuile);
                }

                map.afficherMap();
            }
        }
        
        /*faire une action creature, action de base (attaquer, déplacer, attendre)*/
        public static void actionBase(Map map, Tuile tuile, Joueur j)
        {
            if (j.getListePionInvoque().Count > 0)
            {
                Console.WriteLine("*** Action de base ***");
                string choix;
                foreach (Pion p in j.getListePionInvoque())
                {
                    if (!p.getEstInvoque())
                    {
                        Console.WriteLine("Avec " + p.getCreature().getNom() + ", Voulez vous : ");
                        Console.WriteLine("1. Attaquer");
                        Console.WriteLine("2. Deplacer");
                        Console.WriteLine("3. Les deux");
                        choix = Console.ReadLine();
                        if (choix == "1")
                        {
                            Console.WriteLine("Vous attaquez");
                            attaquerGestion(map, p, j);
                        }
                        if (choix == "2")
                        {
                            Console.WriteLine("Vous vous déplacez");
                            deplacerGestion(map, p, j);
                        }
                        if (choix == "3")
                        {
                            Console.WriteLine("Vous vous déplacez puis attaquez");
                            deplacerGestion(map, p, j);
                            attaquerGestion(map, p, j);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Vous n'avez aucun pion pour le moment !");
            }

        }//ACTION DE BASE

        public static void attaquerGestion(Map map, Pion pion, Joueur j)
        {
            string choixPion;
            int x = pion.getPosX();
            int y = pion.getPosY();

            List<Pion> pionAdjacent = map.pionAdjacent(pion);
            int direction = new int();

            if (pionAdjacent.Count() > 0)
            {
                Console.WriteLine("Quel pion voulez-vous attaquer?");
                for (int i = 0; i < pionAdjacent.Count(); i++)
                {
                    if (pionAdjacent[i].getPosX() == (x) && pionAdjacent[i].getPosY() == (y - 1))
                    {
                        Console.WriteLine((i + 1) + ". le pion du nord");
                    }
                    else if (pionAdjacent[i].getPosX() == (x) && pionAdjacent[i].getPosY() == (y + 1))
                    {
                        Console.WriteLine((i + 1) + ". le pion du sud");
                    }
                    else if (pionAdjacent[i].getPosX() == (x + 1) && pionAdjacent[i].getPosY() == (y))
                    {
                        Console.WriteLine((i + 1) + ". le pion de l'est");
                    }
                    else if (pionAdjacent[i].getPosX() == (x - 1) && pionAdjacent[i].getPosY() == (y))
                    {
                        Console.WriteLine((i + 1) + ". le pion de l'ouest");
                    }

                }

                choixPion = Console.ReadLine();
                int indexPion = int.Parse(choixPion) - 1;
                Console.WriteLine("x : " + x + " y : " + y);

                if (pionAdjacent[indexPion].getPosX() == (x) && pionAdjacent[indexPion].getPosY() == (y - 1))
                {
                    direction = 1;
                }
                else if (pionAdjacent[indexPion].getPosX() == (x) && pionAdjacent[indexPion].getPosY() == (y + 1))
                {
                    direction = 2;
                }
                else if (pionAdjacent[indexPion].getPosX() == (x + 1) && pionAdjacent[indexPion].getPosY() == (y))
                {
                    direction = 3;
                }
                else if (pionAdjacent[indexPion].getPosX() == (x - 1) && pionAdjacent[indexPion].getPosY() == (y))
                {
                    direction = 4;
                }

                Pion pionAdverse = new Pion(pionAdjacent[indexPion].getPosX(), pionAdjacent[indexPion].getPosY());
                bool estMort = j.attaquer(pion, pionAdverse);

                if (estMort)
                {
                    map.attaquer(direction, pion);
                    map.afficherMap();
                }
            }
        }



        public static void deplacerGestion(Map map, Pion pion, Joueur j)
        {

            string choixDirection;
            string choixSens;
            int direction;
            int sens;
            bool flag = true;

            while (flag)
            {
                /* Déplacement du pion */
                Console.WriteLine("Dans quelle direction voulez vous déplacer votre pion ?");
                Console.WriteLine("1. Nord");
                Console.WriteLine("2. Sud");
                Console.WriteLine("3. Est");
                Console.WriteLine("4. Ouest");
                choixDirection = Console.ReadLine();
                direction = int.Parse(choixDirection);


                Console.WriteLine("Dans quelle sens voulez vous que votre pion soit?");
                Console.WriteLine("1. Nord");
                Console.WriteLine("2. Sud");
                Console.WriteLine("3. Est");
                Console.WriteLine("4. Ouest");
                choixSens = Console.ReadLine();
                sens = int.Parse(choixSens);
                pion.setSens(sens);
                bool estDeplace = map.deplacer(direction, pion);

                if (estDeplace)
                {
                    Console.Write("Bravo ! vous avez déplacer votre pion vers ");
                    if (direction == 1)
                    {
                        Console.WriteLine("le nord");
                    }
                    else if (direction == 2)
                    {
                        Console.WriteLine("le sud");
                    }
                    else if (direction == 3)
                    {
                        Console.WriteLine("l'est");
                    }
                    else if (direction == 4)
                    {
                        Console.WriteLine("l'ouest");
                    }
                    Console.WriteLine();
                    map.afficherMap();
                    flag = false;
                }
                else
                {
                    Console.WriteLine("Impossible ! vous essayez de déplacer votre pion vers une case non vide Bakaaaa");
                }

                choixDirection = null;
            }

        }


        public static void mapGestion(Map map, Tuile tuile)
        {
            /*reproduction de l'invocation + action de base version alpha*/
            Console.WriteLine("=== INVOCATION ALEATOIRE ===");
            Pion pion;
            for (int i = 0; i < 10; i++)
            {
                tuile = new Tuile(0, i, "mur");
                map.remplirTuile(tuile);

                tuile = new Tuile(i, 0, "mur");
                map.remplirTuile(tuile);

                tuile = new Tuile(i, 9, "mur");
                map.remplirTuile(tuile);

                tuile = new Tuile(9, i, "mur");
                map.remplirTuile(tuile);
            }
            tuile = new Tuile(2, 1, "pion");
            pion = new Pion(tuile.getPosX(), tuile.getPosY());
            pion.setEstVisible(true);
            tuile.ajouterPion(pion);
            map.remplirTuile(tuile);

            tuile = new Tuile(2, 2, "pion");
            pion = new Pion(tuile.getPosX(), tuile.getPosY());
            pion.setEstVisible(true);
            tuile.ajouterPion(pion);
            map.remplirTuile(tuile);

            tuile = new Tuile(5, 5, "pion");
            pion = new Pion(tuile.getPosX(), tuile.getPosY());
            pion.setEstVisible(false);
            tuile.ajouterPion(pion);
            map.remplirTuile(tuile);

            tuile = new Tuile(3, 3, "pion");
            pion = new Pion(tuile.getPosX(), tuile.getPosY());
            pion.setEstVisible(false);
            tuile.ajouterPion(pion);
            map.remplirTuile(tuile);

            map.afficherMap();
        }
        

    }
}
