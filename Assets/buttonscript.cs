using UnityEngine;
using System.Collections;

public class buttonscript : MonoBehaviour {

    public GameObject conteneur;
	

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 50), "I am a button"))
        {
            Destroy(conteneur);
        }   
    }
}
