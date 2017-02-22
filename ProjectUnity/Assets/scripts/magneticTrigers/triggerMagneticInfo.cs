using UnityEngine;
using System.Collections;

public class triggerMagneticInfo : MonoBehaviour {

    public string[] canMagneticNames;
    private GameObject currentObject = null;
    public Vector2 clampAngleY = new Vector2(3, 7);


    void OnTriggerEnter(Collider col)
    {
        if (col.tag != "object_drag" || currentObject != null)
            return;
       // if (col.transform.rotation.y >= clampAngleY.x && col.transform.rotation.y <= clampAngleY.y)
      //  {


            currentObject = col.gameObject;

            

            currentObject.transform.position = transform.position;
            currentObject.transform.rotation = transform.rotation;
            currentObject.transform.parent = transform;

            //currentObject.GetComponent<Rigidbody>().isKinematic = true;
            currentObject.GetComponent<Collider>().isTrigger = true;
        Destroy(currentObject.GetComponent<Rigidbody>());
        //}
    }

    public bool isValidObject()
    {
        return (currentObject != null);
    }

    public void unWeld()
    {
        if (isValidObject() == false)
            return;

        //currentObject.AddComponent<Rigidbody>();
        //currentObject.GetComponent<Rigidbody>().isKinematic = false;
        //currentObject.GetComponent<Collider>().isTrigger = false;
        currentObject.transform.parent = null;
        currentObject = null;
    }
    

	
}
