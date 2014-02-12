using UnityEngine;
using System.Collections;

public class MenuRecycle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int choix = 0;

    void OnGUI()
    {
        GUI.Label(new Rect(20, 10, 100, 20), "RECYCLE PHASE");

        if (GUI.Button(new Rect(20, 35, 100, 20), "Ok, je recycle"))
        {
            Debug.Log("recycle");
        }
        else if (GUI.Button(new Rect(20, 50, 100, 20), "Non, je passe"))
        {
        }
    }
}
