using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class viewItem : MonoBehaviour {

    public bool createCamera;
    public Transform prefabCamera;
    public Vector3 vec;
    public Camera camMain;
    public List<RenderTexture> texts = new List<RenderTexture>();

    public Transform test;
    

    void Update ()
    {

        if (createCamera)
        {
            Transform obj = (Transform)Instantiate(prefabCamera, vec, new Quaternion(0, 0, 0, 0));
            obj.FindChild("Camera").GetComponent<Camera>().depth = -1f;
            RenderTexture text = new RenderTexture(250, 250, 0);
            obj.FindChild("Camera").GetComponent<Camera>().targetTexture = text;
            texts.Add(text);

            test.GetComponent<Renderer>().material.SetTexture("_MainTex", text); 

               // Camera.SetupCurrent(camMain);

            createCamera = false;
        }


    }


    private int ID = 0;
    public Vector3 view_start = new Vector3(0,-100,0);
    public Transform CreateAreaModel(Transform model, int IDItem)
    {
        Transform obj = (Transform)Instantiate(prefabCamera, view_start+ new Vector3(ID*15+ID,0,0), new Quaternion(0, 0, 0, 0));
        obj.name = "view_" + ID.ToString();
        obj.GetComponent<viewManager>().unique_id = ID;


        obj.FindChild("Camera").transform.position = obj.FindChild("Camera").transform.position;  
        obj.GetComponent<viewManager>().SetModel(model);
        obj.FindChild("Camera").GetComponent<Camera>().depth = -1f;
        RenderTexture text = new RenderTexture(250, 250, 0);
        obj.FindChild("Camera").GetComponent<Camera>().targetTexture = text;
        texts.Add(text);
        
        ID++;

        return obj;
    }

    public Transform CreateAreaModel()
    {
        Transform obj = (Transform)Instantiate(prefabCamera, view_start, new Quaternion(0, 0, 0, 0));
        obj.name = "view_" + ID.ToString();
        obj.GetComponent<viewManager>().unique_id = ID;
        obj.FindChild("Camera").GetComponent<Camera>().depth = -1f;
        RenderTexture text = new RenderTexture(Screen.width, Screen.height, 0);
        obj.FindChild("Camera").GetComponent<Camera>().targetTexture = text;
        texts.Add(text);

        ID++;

        return obj;
    }

    public void RemoveAreaModel (int ID)
    {
        try
        {
            texts.Remove(GameObject.Find("view_" + ID.ToString()).transform.FindChild("Camera").GetComponent<Camera>().targetTexture);
            Destroy(GameObject.Find("view_" + ID.ToString()));
        }
        catch (System.Exception)
        {
            return;
        }  
    }

   public void ResetViewID ()
    {
        for (int i = 0; i < ID; i++ )
        {
            RemoveAreaModel(i);
        }

        texts.Clear();
        ID = 0;
    }


}
