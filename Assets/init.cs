using UnityEngine;
using System.Collections;

public class init : MonoBehaviour{

	// Use this for initialization
	void Start () {
        menuConstructeur gui = ScriptableObject.CreateInstance<menuConstructeur>();
        gui.gui1();
	}
	

	// Update is called once per frame
	void Update () {
	
	}
}
