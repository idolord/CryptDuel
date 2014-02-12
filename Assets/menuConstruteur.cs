using UnityEngine;
using System.Collections;

public class menuConstructeur : ScriptableObject {

    public void gui1()
    {
        GameObject menu = (GameObject)Instantiate(new GameObject());
        menu.AddComponent<buttonscript>();
        menu.AddComponent<textfield>();
    }
}
