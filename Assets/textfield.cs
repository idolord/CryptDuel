using UnityEngine;
using System.Collections;

public class textfield : MonoBehaviour {

    string content = "lamsodozerkjnfdquh, iouaeghrfisdufh kjn;,qodfhpugfh";
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        GUI.TextArea(new Rect(10, 65, 100, 130), content);
    }
}
