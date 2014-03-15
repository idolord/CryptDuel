using UnityEngine;
using System.Collections;
using Worldbuilder;

public class MainAction : MonoBehaviour {

    public static bool isClicked;
    public Entite entite;

    void Start()
    {
        isClicked = false;
    }
    void OnMouseOver()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetMouseButtonDown(0))
            {
                isClicked = true;
                Debug.Log("destroy");
                GameObject.Find("Gestion").SendMessage("mainClicked", entite);
                Gestion.joueur1.Cimetiere.ajouterCarte(entite);
                Gestion.joueur1.Hand.ListeCarte.Remove(entite);
                GameObject.Destroy(this.gameObject);
            }
            else if(Input.GetMouseButtonDown(1))
            {
                isClicked = false;
            }
        }
    }

}
