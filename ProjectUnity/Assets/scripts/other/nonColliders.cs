using UnityEngine;
using System.Collections;

public class nonColliders : MonoBehaviour {

    public Transform[] objs;

    void Awake ()
    {

        foreach (Transform o in objs)
        {
            Physics.IgnoreCollision(o.GetComponent<Collider>(), transform.GetComponent<Collider>());
        }

    }


}
