using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class viewManager : MonoBehaviour {

    public Transform model = null;
    public bool rotate = false;
    private string[] canshaders = new string[] { "Bloom" , "Blur" };
    private List<string> install_shaders = new List<string>();
    const float defaultfov = 60f;
    public int unique_id = -1;

    void Update ()
    {
        if (rotate)
        {
            if (model != null)
            model.transform.Rotate(0, 13 * Time.deltaTime, 0,Space.World);
        }

    }

    public Quaternion getAngleModel()
    {
        return this.model.rotation;
    }

    public void setAngleModel(Quaternion angle)
    {
        this.model.rotation = angle;
    }

    public void setFOV ( float fov )
    {
        transform.FindChild("Camera").GetComponent<Camera>().fieldOfView = fov;
    }

    public void resetFOV ()
    {
        transform.FindChild("Camera").GetComponent<Camera>().fieldOfView = defaultfov;
    }

    public void SetModel ( Transform model )
    {
       // ClearModel();

        Transform obj = (Transform)Instantiate(model, transform.FindChild("point_item").position, new Quaternion(0, 0, 0, 0));
        obj.gameObject.layer = 8;
        model = obj;
        model.name = "model";
        model.parent = transform;
        

        try { obj.GetComponent<Rigidbody>().isKinematic = true; }
        catch (System.Exception) { }

        
    }

    public void SetModel(Transform model , Quaternion angle)
    {

        Transform obj = (Transform)Instantiate(model, transform.FindChild("point_item").position, new Quaternion(0, 0, 0, 0));
        obj.gameObject.layer = 8;
        model = obj;
        model.name = "model";
        model.parent = transform;

        try { obj.GetComponent<Rigidbody>().isKinematic = true; }
        catch (System.Exception) { }
    }

    public void SetModel(Transform model, Vector3 addVector , Quaternion angle)
    {

        Transform obj = (Transform)Instantiate(model, transform.FindChild("point_item").position+addVector, new Quaternion(0, 0, 0, 0));
        obj.gameObject.layer = 8;
        model = obj;
        model.name = "model";
        model.parent = transform;
        for (int i = 0; i < obj.childCount; ++i)
            obj.GetChild(i).gameObject.layer = 8;

        try { obj.GetComponent<Rigidbody>().isKinematic = true; }
        catch (System.Exception) { }
    }

    public void ClearModel ()
    {

        try
        {
            Destroy(model.gameObject);
        }
        catch (System.Exception e)
        {
            model = null;
            Debug.Log(e);
        }


        model = null;
    }

    bool isValidShader (string shader)
    {
        foreach (string s in canshaders)
        {

            if (s == shader)
                return true;

        }

        return false;
    }

    public void AddShader ( string shadername )
    {
        if (isValidShader(shadername) == false)
            return;

        try
        {
           
        }
        catch (System.Exception)
        {
            return;
        }

    }

    public void RemoveShader ( string shadername )
    {

        try
        {

        }
        catch (System.Exception)
        {
            return;
        }

    }


}
