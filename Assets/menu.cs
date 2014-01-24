using UnityEngine;
using System.Collections;
using Worldbuilder;

public class menu : MonoBehaviour {

    public Texture bkgt= (Texture)Resources.Load("ui/book");
    public env Gui;
    GUILayout menulayout;

    void OnMouseOver()
    {
        Gui.overGUI = true;
    }

   

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
    void Update()
    {
        Gui.overGUI = false;
	}

    void OnGUI()
    {

        GUI.DrawTexture(new Rect(Screen.width/2, Screen.height/2,bkgt.width,bkgt.height), bkgt, ScaleMode.ScaleToFit, true, 10.0F);
    }
}
