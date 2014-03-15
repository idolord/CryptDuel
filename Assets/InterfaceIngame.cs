using UnityEngine;
using System.Collections;
using Worldbuilder;

public class InterfaceIngame
{

    private GameObject affichageGauche;
    private GameObject affichageDroit;
    private GameObject cimetiereObjet;
    private GameObject mainImage;
    private GameObject mainConteneur;
    private GameObject piocheObjet;
    
    float z;
    Camera cam;
    Vector3 p;

    // Use this for initialization
    public InterfaceIngame(string pseudo)
    {

        GameObject camera = new GameObject();

        camera.name = "Camera" + pseudo;
        camera.AddComponent<Camera>();
        camera.AddComponent<Light>();
        camera.GetComponent<Light>().type = LightType.Directional;
        camera.GetComponent<Light>().transform.localEulerAngles = new Vector3(0, 0, 180);
        camera.GetComponent<Light>().intensity = 0;

        cam = camera.camera;
        camera.tag = "MainCamera";
        cam.clearFlags = CameraClearFlags.Depth;
        camera.layer = 1;
        cam.cullingMask = 2;
        cam.orthographic = true;
        cam.orthographicSize = 10;
        cam.nearClipPlane = -10;
        cam.farClipPlane = 10;
        cam.depth = 0;
        cam.transform.localEulerAngles = new Vector3(0, 0, 0);
        p = cam.ViewportToWorldPoint(new Vector3(0, 1, 0));
        GameObject cartePrefab = Resources.Load("object/carte_prefab") as GameObject;
        if (cartePrefab != null)
        {
            /* Affichage des emplacements */
            affichageGauche = GameObject.Instantiate(cartePrefab) as GameObject;
            affichageDroit = GameObject.Instantiate(cartePrefab) as GameObject;
            cimetiereObjet = GameObject.Instantiate(cartePrefab) as GameObject;
            mainConteneur = GameObject.Instantiate(cartePrefab) as GameObject;
            piocheObjet = GameObject.Instantiate(cartePrefab) as GameObject;
            mainImage = GameObject.Instantiate(cartePrefab) as GameObject;

            z = 0;

            GameObject panel = new GameObject();
            panel.name = "Panel";
            panel.transform.parent = camera.transform;

            affichageGauche.transform.parent = panel.transform;
            affichageDroit.transform.parent = panel.transform;
            cimetiereObjet.transform.parent = panel.transform;
            mainConteneur.transform.parent = panel.transform;
            mainImage.transform.parent = mainConteneur.transform;
            piocheObjet.transform.parent = panel.transform;
                                    
            affichageGauche.name = "affichageGauche";
            affichageDroit.name = "affichageDroit";
            cimetiereObjet.name = "cimetiere";
            mainConteneur.name = "main";
            mainImage.name = "main image";
            piocheObjet.name = "pioche";

        }
    }

    //float originalWidth = 640.0f; // define here the original resolution
    //float originalHeight = 400.0f; // you used to create the GUI contents
    int pourcentage = 2;
    public void update()
    {
        
        float gaucheX = Mathf.Abs(p.x) - (affichageGauche.renderer.bounds.size.x / 2) - (Mathf.Abs(p.x) * pourcentage / 100);
        float gaucheY = Mathf.Abs(p.y) - (affichageGauche.renderer.bounds.size.y); ;

        float droitX = Mathf.Abs(p.x) - (affichageDroit.renderer.bounds.size.x / 2) - (Mathf.Abs(p.x) * pourcentage / 100);
        float droitY = Mathf.Abs(p.y) - (affichageDroit.renderer.bounds.size.y);

        float centreX = Mathf.Abs(0);
        float centreY = Mathf.Abs(p.y) - (mainConteneur.renderer.bounds.size.y / 2) - (Mathf.Abs(p.y) * pourcentage / 100);

        float piocheX = Mathf.Abs(p.x) - (piocheObjet.renderer.bounds.size.x / 2) - (Mathf.Abs(p.x) * pourcentage / 100);
        float piocheY = Mathf.Abs(p.y) - (piocheObjet.renderer.bounds.size.y / 2) - (Mathf.Abs(p.y) * pourcentage / 100);

        float cimetiereX = Mathf.Abs(p.x) - (cimetiereObjet.renderer.bounds.size.x / 2) - (Mathf.Abs(p.x) * pourcentage / 100);
        float cimetiereY = Mathf.Abs(p.y) - (cimetiereObjet.renderer.bounds.size.y) - (Mathf.Abs(p.y)*(pourcentage+1) / 100);

        float gaucheScale = 1f;
        float droitScale = 1f;
        float mainScaleX = 1.2f;
        float mainScaleY = mainScaleX * 3;
        float cimetiereScale = 0.6f;
        float piocheScale = 0.6f;
        
        affichageGauche.transform.position = new Vector3(-gaucheX, gaucheY, z);
        affichageGauche.transform.localEulerAngles = new Vector3(0, 0, 0);
        affichageGauche.transform.localScale = new Vector3(gaucheScale, gaucheScale, 1);

        affichageDroit.transform.localPosition = new Vector3(droitX, droitY, z);
        affichageDroit.transform.localEulerAngles = new Vector3(0, 0, 0);
        affichageDroit.transform.localScale = new Vector3(droitScale, droitScale, 1);

        mainConteneur.transform.localPosition = new Vector3(centreX, -centreY, z);
        mainConteneur.transform.localEulerAngles = new Vector3(0, 0, 90);
        mainConteneur.transform.localScale = new Vector3(1, 1, 1);

        mainImage.transform.localPosition = new Vector3(0, 0, 0);
        mainImage.transform.localEulerAngles = new Vector3(0, 0, 0);
        mainImage.transform.localScale = new Vector3(mainScaleX, mainScaleY, 1);


        cimetiereObjet.transform.localPosition = new Vector3(cimetiereX, -cimetiereY, z);
        cimetiereObjet.transform.localEulerAngles = new Vector3(0, 0, 90);
        cimetiereObjet.transform.localScale = new Vector3(cimetiereScale, cimetiereScale, 1);

        piocheObjet.transform.localPosition = new Vector3(piocheX, -piocheY, z);
        piocheObjet.transform.localEulerAngles = new Vector3(0, 0, 90);
        piocheObjet.transform.localScale = new Vector3(piocheScale, piocheScale, 1);
    }


    public GameObject AffichageGauche { get { return this.affichageGauche; } set { this.affichageGauche = value; } }
    public GameObject AffichageDroit { get { return this.affichageDroit; } set { this.affichageDroit = value; } }
    public GameObject MainConteneur { get { return this.mainConteneur; } set { this.mainConteneur = value; } }
    public GameObject CimetiereObjet { get { return this.cimetiereObjet; } set { this.cimetiereObjet = value; } }
    public GameObject PiocheObjet { get { return this.piocheObjet; } set { this.piocheObjet = value; } }

}
