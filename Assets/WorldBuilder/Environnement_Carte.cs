using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

namespace Worldbuilder
{

    //entity familiy
    [Serializable]
    public class Entite
    {
        protected string nom;
        protected int cout;
        protected Carte carte;
        
        public Entite(string nom, int cout)
        {
            this.nom = nom;
            this.cout = cout;
            carte = new Carte(nom, cout);
        }

        public Entite(Carte carte)
        {
            this.nom = carte.Nom;
            this.cout = carte.Cout;
            this.carte = carte;
        }

        public string Nom { get { return this.nom; } set { this.nom = value; } }
        public Carte Carte { get { return this.carte; } set { this.carte = value; } }
        public int Cout { get { return this.cout; } set { this.cout = value; } }

    }

    [Serializable]
    public class Sort : Entite
    {
        public Sort(string nom, int cout)
            : base(nom, cout)
        {
        }
    }

    [Serializable]
    public class Piege : Entite
    {
        public Piege(string nom, int cout)
            : base(nom, cout)
        {
        }
    }

    [Serializable]
    public class Creature : Entite
    {
        /*les attributs réels de la créature*/
        protected int attaque;
        protected int defense;
        protected int retaliation;
        protected int duelScience;
        protected int vie;

        /*les chiffres qui permettent de générer les cases du layout correspondant a chaque action/status possible*/
        protected int layoutAttaque;
        protected int layoutDeplacement;
        protected int layoutVision;

        /*le layout se met sur la carte en bas a droite, est-ce qu'il se repartie aussi du le pion?*/
        protected Layout layout;

        public Creature(string nom, int cout)
            : base(nom, cout)
        {

            this.vie = cout;
            /*init de test*/
            layoutAttaque = 1;
            layoutDeplacement = 2;
            layoutVision = 4;
            layout = new Layout(layoutAttaque, layoutDeplacement, layoutVision);
        }

        public Layout getLayout()
        {
            return this.layout;
        }

        /*les attributs réels de la créature*/
        public int Attaque { get { return this.attaque; } set { this.attaque = value; } }
        public int Defense { get { return this.defense; } set { this.defense = value; } }
        public int Retaliation { get { return this.retaliation; } set { this.retaliation = value; } }
        public int DuelScience { get { return this.duelScience; } set { this.duelScience = value; } }
        public int Vie { get { return this.vie; } set { this.vie = value; } }

        /*les chiffres qui permettent de générer les cases du layout correspondant a chaque action/status possible*/
        public int LayoutAttaque { get { return this.layoutAttaque; } set { this.layoutAttaque = value; } }
        public int LayoutDeplacement { get { return this.layoutDeplacement; } set { this.layoutDeplacement = value; } }
        public int LayoutVision { get { return this.layoutVision; } set { this.layoutVision = value; } }
    }


    [Serializable]
    public class Hero : Creature
    {
        /*les attributs réels de la créature*/
        private int niveau;
        private int attaqueParNiveau;
        private int defenseParNiveau;
        private int retaliationParNiveau;
        private int duelScienceParNiveau;

        public Hero(string nom, int cout)
            : base(nom, cout)
        {
        }

        public int Niveau { get { return this.niveau; } set { this.niveau = value; } }
        public int AttaqueParNiveau { get { return this.attaqueParNiveau; } set { this.attaqueParNiveau = value; } }
        public int DefenseParNiveau { get { return this.defenseParNiveau; } set { this.defenseParNiveau = value; } }
        public int RetaliationParNiveau { get { return this.retaliationParNiveau; } set { this.retaliationParNiveau = value; } }
        public int DuelScienceParNiveau { get { return this.duelScienceParNiveau; } set { this.duelScienceParNiveau = value; } }

    }
    public class Gestion{
        protected List<Entite> listeCarte;

        public Gestion()
        {
            listeCarte = new List<Entite>();
        }

        public List<Entite> ListeCarte { get { return this.listeCarte; } set { this.listeCarte = value; } }
                
    }


    [Serializable]
    public class GestionCarte : Gestion{
           
        

        /*On ajoute une carte dans la liste + surcharge des enfants */
        public virtual void ajouterCarte(Entite uneCarte)
        {
                listeCarte.Add(uneCarte);            
        }
        
        /*On retire une carte de(s) la liste + surcharge des enfants */

        public virtual void retirerCarte(Entite uneCarte)
        {
                listeCarte.Remove(uneCarte);           
        }



    }

    //card handler family
    [Serializable]
    public abstract class InterfaceCarte : Gestion
    {
        /*Afin de bien différencier le type de carte,
         * on fait une liste de chaque, 
         * pour ensuite avoir les info et les action qui vont bien avec*/
        protected GameObject conteneur;

        public InterfaceCarte(GameObject conteneur)
        {
            this.conteneur = conteneur;
        }

        public abstract void ajouterCarte(Entite uneCarte);
        public abstract void retirerCarte(Entite uneCarte);
        public abstract void placerDansConteneur();

        public GameObject Conteneur { get { return this.conteneur; } set { this.conteneur = value; } }
    }

    public class Cimetiere : InterfaceCarte
    {

        public Cimetiere(GameObject conteneur) : base(conteneur)
        {
        }

        public override void ajouterCarte(Entite uneCarte)
        {
            Debug.Log("Cimetiere ajout");
            Debug.Log("liste cimetiere avant " + listeCarte.Count);
            Debug.Log(conteneur.name);
            uneCarte.Carte.create(conteneur);
            listeCarte.Add(uneCarte);
            placerDansConteneur();
            Debug.Log("liste cim apres " + listeCarte.Count);
        }


        public override void retirerCarte(Entite uneCarte)
        {
            /*
            if (listeCarte.Contains(uneCarte))
            {
                Debug.Log("Cimetiere retirer");
                listeCarte.Remove(uneCarte);
                GameObject.Destroy(uneCarte.Carte.carte);
            }//*/
        }

        public override void placerDansConteneur() {
            for (int i = 0; i < listeCarte.Count; i++)
            {
                Debug.Log("cimetiere conteneur");
                Entite e = listeCarte[i];
                e.Carte.carte.transform.localScale = new Vector3(1, 1, 1);
                e.Carte.carte.transform.localEulerAngles = new Vector3(0, 0, 0);
                e.Carte.carte.transform.localPosition = new Vector3(0, 0, -1);
            }
        }


    }


    [Serializable]
    public class Hand : InterfaceCarte
    {

        private int coutMini;
        
        public Hand(GameObject conteneur)
            : base(conteneur)
        {
        }

        public void calculCoutMini()
        {
            coutMini = listeCarte[0].Cout;
            for (int i = 1; i < listeCarte.Count(); i++)
            {
                if (coutMini > listeCarte[i].Cout)
                {
                    coutMini = listeCarte[i].Cout;
                }
            }
        }



        public int CoutMini { get { return this.coutMini; } set { this.coutMini = value; } }

        /*On ajoute une carte dans la liste + surcharge des enfants */
        public void ajouterCarte(List<Entite> listeEntite)
        {
            foreach (Entite e in listeEntite)
            {
                ajouterCarte(e);
            }
        }

        /*On ajoute une carte dans la liste + surcharge des enfants */
        public override void ajouterCarte(Entite uneCarte)
        {
            Debug.Log("ajouter main");
            uneCarte.Carte.create(conteneur);
            listeCarte.Add(uneCarte);
            placerDansConteneur();
            uneCarte.Carte.carte.AddComponent<MainAction>();
            uneCarte.Carte.carte.GetComponent<MainAction>().entite = uneCarte;
            calculCoutMini();       
        }

        /*On retire une carte de(s) la liste + surcharge des enfants */
        public override void retirerCarte(Entite uneCarte)
        {
            for(int i = 0; i < listeCarte.Count; i++)
            {
                Entite e = listeCarte[i];
                if (e.Carte.carte == uneCarte.Carte.carte)
                {
                    Debug.Log("retirer main");
                    //GameObject.Destroy(e.Carte.carte);
                    listeCarte.Remove(e);
                    //listeCarte.RemoveAt(i);
                    if (listeCarte.Count > 0)
                        calculCoutMini();
                }
            }
        }

        public override void placerDansConteneur()
        {
            int compteur = 0;
            int total = listeCarte.Count;
            foreach (Entite e in listeCarte)
            {
                //(conteneur.transform.GetChild(0).renderer.bounds.size.x / conteneur.transform.GetChild(compteur).renderer.bounds.size.x)
                compteur++;
                float y = (conteneur.transform.GetChild(0).renderer.bounds.size.x) 
                    - ((conteneur.transform.GetChild(0).renderer.bounds.size.x / total) * compteur)
                    - ( conteneur.transform.GetChild(1).renderer.bounds.size.y * 2);
                e.Carte.carte.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                e.Carte.carte.transform.localEulerAngles = new Vector3(0, 180, 90);
                e.Carte.carte.transform.localPosition = new Vector3(0, y, -1);
            }
        }

    }

    [Serializable]
    public class Pioche : InterfaceCarte
    {

        public Pioche(GameObject conteneur) : base (conteneur){}

        public override void ajouterCarte(Entite uneCarte)
        {
            throw new NotImplementedException();
        }

        public override void retirerCarte(Entite uneCarte)
        {
            throw new NotImplementedException();
        }

        /*On ajoute une carte dans la liste + surcharge des enfants */
        public void ajouterListe(List<Entite> listeEntite)
        {
            foreach (Entite e in listeEntite)
            {
                e.Carte.create(conteneur);
                listeCarte.Add(e);
                e.Carte.carte.AddComponent<PiocheAction>();
                e.Carte.carte.GetComponent<PiocheAction>().entite = e;
                placerDansConteneur();
            }
        }

        public void melanger()
        {
            System.Random random = new System.Random();
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
                Debug.Log(listeCarte[i].Nom);
            }

        }

        public override void placerDansConteneur()
        {
            float compteur = 0.0f;
            for(int i = listeCarte.Count-1; i >= 0; i--)
            {
                Entite e = listeCarte[i];
                e.Carte.carte.transform.localScale = new Vector3(1, 1, 1);
                e.Carte.carte.transform.localEulerAngles = new Vector3(0, 0, 0);
                e.Carte.carte.transform.localPosition = new Vector3(0, 0, (-1 - (compteur / 10)));
                compteur++;
            }
        }


        public Entite piocher()//on considere que la premiere carte est celle du dessus
        {
            Entite carte;

            carte = listeCarte[0];
            GameObject.Destroy(listeCarte[0].Carte.carte);
            listeCarte.RemoveAt(0);


            return carte;
        }

    }





    [Serializable]
    public class Carte
    {
        public Material material;
        public GameObject cartePrefab;
        public GameObject carte;
        private string nom;
        private int cout;

        public Carte(string nom, int cout)
        {
            this.nom = nom;
            this.cout = cout;
        }

        public void create(GameObject conteneur)
        {
            material = Resources.Load("object/carte" + nom + "_mat") as Material;
            cartePrefab = Resources.Load("object/carte_prefab") as GameObject;
            carte = GameObject.Instantiate(cartePrefab) as GameObject;
            carte.name = nom;
            carte.renderer.material = material;
            carte.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            carte.transform.parent = conteneur.transform;
        }

        public string Nom { get { return this.nom; } set { this.nom = value; } }
        public int Cout { get { return this.cout; } set { this.cout = value; } }

    }


    [Serializable]
    public class Layout
    {
        private int attaque;
        private int deplacement;
        private int vision;

        public Layout(int attaque, int deplacement, int vision)
        {
            this.attaque = attaque;
            this.deplacement = deplacement;
            this.vision = vision;
        }

    }


}
