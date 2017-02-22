using UnityEngine;
using System.Collections;

public class screenInfo : MonoBehaviour {

    const string version = "0.0.1(Alpha)";
    
    void OnGUI ()
    {

        GUI.Label(new Rect(10, 10, 200, 200), version + "\n" + "<TAB> магазин>");


    }
         

}
