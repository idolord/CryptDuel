using UnityEngine;
using System.Collections;
using Worldbuilder;

public class menu : MonoBehaviour {
    public Texture bkgt;
    public Environnement Gui;
    GUILayout menulayout;

    void OnMouseOver()
    {
        Gui.OverGUI = true;
    }

   

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
    void Update()
    {
        Gui.OverGUI = false;
	}

    void OnGUI()
    {

        GUI.DrawTexture(new Rect((Screen.width/2)-(bkgt.width/2),(Screen.height/2)-(bkgt.height/2),bkgt.width,bkgt.height), bkgt);
    }
}
