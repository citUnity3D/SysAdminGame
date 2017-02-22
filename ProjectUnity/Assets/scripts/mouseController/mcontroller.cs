using UnityEngine;
using System.Collections;

public class mcontroller : MonoBehaviour {

    public Camera activeCamera;
    public GameObject _cursor;
    public float speed;
    private float Y_level = 4.849f;
    public float speedRotate = 1f;
    public bool modeRotate = false;

    public Vector3 movePoint = new Vector3(0, 0, 0);
    public Quaternion moveAngle = new Quaternion(0, 0, 0, 0);
    public Quaternion oldAngle;
    RaycastHit hitInfo2;


    void Awake()
    {
        oldAngle = _cursor.transform.rotation;
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            modeRotate = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            modeRotate = false;
        }

        if (Physics.Raycast(activeCamera.ScreenPointToRay(Input.mousePosition), out hitInfo2) && modeRotate == false)
        {
            movePoint = new Vector3(hitInfo2.point.x, 0, hitInfo2.point.z);
        }

        if (modeRotate)
        {
            float z = 0;

            //if (Input.GetAxis("Mouse X") > 0.5f && Input.GetAxis("Mouse Y") > 0.5f)
               // z = -1*(Input.GetAxis("Mouse X") * speedRotate);


            _cursor.transform.Rotate(Input.GetAxis("Mouse Y") * speedRotate, Input.GetAxis("Mouse X") * speedRotate * -1, z , Space.Self);

        }


        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Y_level += 0.025f;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Y_level -= 0.025f;
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            _cursor.transform.Rotate(Input.GetAxis("Vertical") * speedRotate * -1, 0, 0);
            
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            _cursor.transform.Rotate(0, Input.GetAxis("Horizontal") * speedRotate, 0);
            
        }

        if (Input.GetKey(KeyCode.Q))
        {
            _cursor.transform.Rotate(0, 0, Time.deltaTime * speedRotate * 22);

        }

        if (Input.GetKey(KeyCode.E))
        {
            _cursor.transform.Rotate(0, 0,-( Time.deltaTime * speedRotate * 22));

        }

        moveAngle = _cursor.transform.rotation;
        movePoint.y = Y_level;
        _cursor.transform.position = Vector3.Lerp(_cursor.transform.position, movePoint, speed * Time.deltaTime);
    }


	
}
