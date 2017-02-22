using UnityEngine;
using System.Collections;

public class objectDrag : MonoBehaviour {

    const string tag_mTrigger = "mTrigger";
    const string tag_drag_object = "object_drag";
    public Camera activeCamera;
    public float forceDrag = 1.0f;

    public GameObject dragCur = null;

    void Update()
    {
        if (dragCur != null)
            UpdateDrag();

        
        if (Input.GetMouseButtonDown(0) && dragCur == null)
        {
            RaycastHit hit;
            Ray ray = activeCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == tag_drag_object)
                {
                    dragCur = hit.transform.gameObject;
                    setModeDrag(true);
                }    
            }
           
        }

        if (Input.GetMouseButtonDown(1) && dragCur == null)
        {
            RaycastHit hit;
            Ray ray = activeCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("HIT " + hit.transform.ToString());

                if (hit.transform.tag == tag_mTrigger)
                {
                    
                    hit.transform.GetComponent<triggerMagneticInfo>().unWeld();
                }
            }

        }

        if (Input.GetMouseButtonUp(0) && dragCur != null)
        {
            setModeDrag(false);
            dragCur = null;
        }

        

    }

    void setModeDrag(bool flag)
    {
        if (dragCur == null)
            return;


        if (flag)
        {
            dragCur.GetComponent<Rigidbody>().useGravity = false;
            GetComponent<mcontroller>()._cursor.transform.rotation = dragCur.transform.rotation;
        }
        else
        {
            dragCur.GetComponent<Rigidbody>().useGravity = true;
            GetComponent<mcontroller>()._cursor.transform.rotation = GetComponent<mcontroller>().oldAngle;
        }

    }

    const float updateDelta = 0.05f;
    void UpdateDrag()
    {

        //dragCur.GetComponent<Rigidbody>().MovePosition();
      //  dragCur.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(1, 1, 1) * Time.deltaTime, GetComponent<mcontroller>().movePoint);

        try
        {
            dragCur.GetComponent<Rigidbody>().AddForce(-(dragCur.transform.position - GetComponent<mcontroller>().movePoint) * (forceDrag * updateDelta), ForceMode.VelocityChange);
           // dragCur.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(dragCur.transform.position, GetComponent<mcontroller>().movePoint, forceDrag * Time.deltaTime));
            dragCur.GetComponent<Rigidbody>().MoveRotation(Quaternion.Lerp(dragCur.transform.rotation, GetComponent<mcontroller>().moveAngle, forceDrag * updateDelta));
        }
        catch (System.Exception ex)
        {
            dragCur = null;
            Debug.LogError(ex.ToString());
            return;
        }

    }

	
}
