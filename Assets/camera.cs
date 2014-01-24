using UnityEngine;
using System.Collections;


//script for moving camera
public class camera : MonoBehaviour {

    public float distance;
    public float rotatespeed;
    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    public float xSpeed = 200.0f;
    public float ySpeed = 200.0f;
    public Quaternion currentRotation;
    private Quaternion desiredRotation;
    private Quaternion rotation;
    public float zoomDampening = 5.0f;
    public bool overgui = false;
        void Start ()
        {
            distance = 10;
            rotatespeed = 1.5f;
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetMouseButton(0))
                {
                    xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                    yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
                    yDeg = Mathf.Clamp(yDeg, 35, 90);
                    desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);
                    currentRotation = transform.rotation;
                    rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * zoomDampening);
                    transform.rotation = rotation;
                }
                
            }
        }

        public void OnGUI()
        {
        }
}
