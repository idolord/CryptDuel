using UnityEngine;
using System.Collections;

public class Library : MonoBehaviour {

    float smooth;

    void Start()
    {
        smooth = Time.deltaTime;
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animationPiocher();
        }
    }

    public void animationPiocher()
    {
        gameObject.transform.parent.localPosition = new Vector3(-20,-25,71);
        gameObject.transform.parent.localEulerAngles = new Vector3(180, 0, 180);
    }
}
