using UnityEngine;
using System.Collections;
using Worldbuilder;

public class PiocheAction : MonoBehaviour {

    public bool isClicked;
    public static bool isActive;
    public Entite entite;

    void Start()
    {
        isClicked = false;
        isActive = true;
    }

    void OnMouseDown()
    {
        Debug.Log(isActive);
        if (!Input.GetKey(KeyCode.LeftShift) && isActive)
        {
            isClicked = true;
            Debug.Log("J'ai pioché!");
            Gestion.joueur1.Hand.ajouterCarte(entite);
            Gestion.joueur1.Pioche.ListeCarte.Remove(entite);
            GameObject.Destroy(this.gameObject);
        }
    }

}
