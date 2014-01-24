using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {

    private Transform camtransf;
    public Transform camfocuss;
    public float distance;
    public float rotatespeed;
    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    public float xSpeed = 200.0f;
    public float ySpeed = 200.0f;
    private Quaternion currentRotation;
    private Quaternion desiredRotation;
    private Quaternion rotation;
    public float zoomDampening = 5.0f;
    Vector2 camRotation;
    public bool overgui = false;
        void Start ()
        {
            distance = 10;
            camtransf = transform;
            rotatespeed = 1.5f;
            transform.position = new Vector3(camfocuss.transform.position.x,camfocuss.transform.position.y + distance,camfocuss.transform.position.z-5);
            camtransf.LookAt(camfocuss);
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
