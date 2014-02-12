using UnityEngine;
using System.Collections;

public class MenuShift : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int choix = 0;
    void OnGUI()
    {
        GUI.Label(new Rect(20, 10, 100, 20), "SHIFT PHASE");

        if (GUI.Button(new Rect(20, 35, 100, 20), "Ok, je shift"))
        {
            Debug.Log("shift");
            choix = 1;
        }
        else if (GUI.Button(new Rect(20, 60, 100, 20), "Non, je passe"))
        {
            choix = 2;            
        }


    }
}
