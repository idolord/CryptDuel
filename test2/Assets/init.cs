using UnityEngine;
using System.Collections;

public class init : MonoBehaviour {

    ConstructeurMenu nemu =new ConstructeurMenu();
	// Use this for initialization
	void Start () {
        nemu.initialise();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static void print(string s)
    {
        Debug.Log(s);
    }
}
