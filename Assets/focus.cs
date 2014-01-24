using UnityEngine;
using System.Collections;

public class focus : MonoBehaviour {

    private Quaternion currentRotation;
    private Quaternion desiredRotation;
    private Quaternion rotation;
    private Transform hand;
	// Use this for initialization
    void Start()
    {
        hand = this.GetComponentInChildren<camera>().transform;
	
	}
	
	// Update is called once per frame
	void Update () {
        currentRotation = transform.rotation;
        desiredRotation = Quaternion.Euler(hand.rotation.x,hand.rotation.y,hand.rotation.z);
        rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime);
        transform.rotation = rotation;
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal"));
        transform.Translate(Vector3.forward * Input.GetAxis("Vertical"));
	}
}
