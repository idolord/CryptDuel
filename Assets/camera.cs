using UnityEngine;
using System.Collections;


//script for moving camera
public class camera : MonoBehaviour {

    public float distance;
    public float rotatespeed;
    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    private float zDeg = 0.0f;
    public float xSpeed = 200.0f;
    public float ySpeed = 200.0f;
    public Quaternion currentRotation;
    private Quaternion desiredRotation;
    private Quaternion rotation;
    public Quaternion transrot;
    public float zoomDampening = 5.0f;
    public bool overgui = false;
    private float camrad;
    private float vert;
        void Start ()
        {
            distance = 10;
            rotatespeed = 1.5f;
            //zDeg = transform.localPosition.x;
            yDeg = transform.localEulerAngles.x;
            zDeg = 15;
            xDeg = transform.localEulerAngles.y;
            //yDeg = 87;
            //transform.rotation = Quaternion.Euler(87,0,0);
            transform.rotation = transform.localRotation;

        }

        void Update()
        {
            camrad = (yDeg) * Mathf.Deg2Rad;
            transform.Translate(Vector3.right * (Input.GetAxis("Horizontal")),Space.Self);
            vert = Input.GetAxis("Vertical");
            if (vert != 0)
            {
                Debug.Log(vert); 
            }
            if ((vert > 0)&&(yDeg < 89))
            {
                transform.Translate(Vector3.forward * (vert * Mathf.Tan(camrad) * vert), Space.Self); 
            }
            else if (vert < 0)
            {
                transform.Translate(Vector3.forward * (vert * Mathf.Tan(camrad) * vert * -1), Space.Self);
            }
            
            transform.position = new Vector3(transform.position.x, zDeg, transform.position.z);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetMouseButton(0))
                {
                    xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                    yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
                    xDeg = xDeg % 360;
                    //Debug.Log(xDeg);
                    yDeg = Mathf.Clamp(yDeg, 35, 87);
                    currentRotation = transform.rotation;
                    desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);
                    rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * zoomDampening);
                    transform.rotation = rotation;

                }
                else if (Input.GetMouseButton(1))
                {
                    zDeg += Input.GetAxis("Mouse Y") * Time.deltaTime;
                    zDeg = Mathf.Clamp(zDeg, 10, 20);
                    transform.position = new Vector3(transform.position.x, zDeg, transform.position.z);
                }
                
            }
        }
}
