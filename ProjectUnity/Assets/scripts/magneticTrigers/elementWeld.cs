using UnityEngine;
using System.Collections;

public class elementWeld : MonoBehaviour {

    protected bool[] welds = new bool[4];

    void Awake()
    {
        Initial();
    }

    void Initial()
    {
        for (int i = 0; i < this.welds.Length; i++)
        {
            this.welds[i] = false;
        }
    }

    protected Transform waldComponent = null;
    void setWeld(int i,bool value)
    {
        if (i > this.welds.Length || this.waldComponent == null)
            return;
        

            this.welds[i] = value;
    }


    void setWeldComponent(Transform comp)
    {
        if (this.waldComponent != null)
            return;


        this.waldComponent = comp;
    }



}
