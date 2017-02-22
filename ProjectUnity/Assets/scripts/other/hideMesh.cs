using UnityEngine;
using System.Collections;

public class hideMesh : MonoBehaviour {

    void Awake()
    {

        GetComponent<MeshRenderer>().enabled = false;

    }
	
}
