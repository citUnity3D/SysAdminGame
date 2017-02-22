using UnityEngine;
using System.Collections;

public class mouseDetectController : MonoBehaviour {

    public Vector2 center;
    public float w;
    public float h;
    public Texture2D test;
    public float radius;
    public int countNum = 12;
	public GameObject selectCurObject = null;
	public float forceDown = 1f;
	public float rateUpdate = 0.3f;

	public void actionRotate ()
	{
		if (this.selectCurObject == null ) 
			return;
		if (this.rotateMode == "none")
			return;

		/*
		int i = 1;

		if (rotateMode == "left")
			i = -1;

		Vector3 oldVector = this.selectCurObject.transform.position;
		oldVector.y -= (forceDown/1000 * i);
		

		this.selectCurObject.transform.Rotate (0, i * forceDown*7.5f, 0);
		this.selectCurObject.transform.position = oldVector;
 */
	}

	Vector3 lerpVector = Vector3.zero;

	void Update ()
	{
		if (this.selectCurObject == null ) 
			return;
		if (this.rotateMode == "none")
			return;

		int i = 1;

		if (rotateMode == "left")
			i = -1;

		Vector3 oldVector = this.selectCurObject.transform.position;
		oldVector.y -= (forceDown/1000 * i);


		this.selectCurObject.transform.Rotate (0, i * forceDown*7.5f, 0);
		this.selectCurObject.transform.position = oldVector;

	}
    
    Vector2 PointOnCircle( float ang, float radius, float offX, float offY )
    {
        float x, y;

	ang = (ang/180)*Mathf.PI;
    x = Mathf.Cos(ang) * radius + offX;
    y = Mathf.Sin((int)ang) * radius + offY;
        
	return new Vector2(x,y);
    }

    Vector2[] getPseudoCircle(Vector2 center,float radius)
    {
        Vector2[] tbl = new Vector2[4];

        tbl[0] = new Vector2(center.x, center.y + radius);
        tbl[1] = new Vector2(center.x + radius, center.y);
        tbl[2] = new Vector2(center.x,center.y-radius);
        tbl[3] = new Vector2(center.x - radius, center.y);

        return tbl;
    }

    Vector2 getMousePosValid()
    {
        return new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
    }

    Vector2[] tt;


    public bool enab = false;
    public int firstSelect = -1;
    public string rotateMode = "none";
	Ray ray;
	RaycastHit hit;
	public Camera activeCamera;
    void OnGUI()
    {

       // for (float angle = 0; angle < 360; angle += 360 / countNum)
       // {
      //      Vector2 pos = PointOnCircle(angle, radius, center.x, center.y);

       //     GUI.DrawTexture(new Rect(pos.x, pos.y, 16, 16), test);
      //  }

		if (this.selectCurObject == null) {
			if (Input.GetMouseButtonDown (1)) {
				ray = activeCamera.ScreenPointToRay (Input.mousePosition);

				if (Physics.Raycast (ray, out hit)) {

					this.selectCurObject = hit.transform.gameObject;

				}

			}
		}

		if (this.selectCurObject == null)
			return;

        if (Input.GetMouseButtonDown(1))
        {
            enab = true;
            tt = getPseudoCircle(getMousePosValid(), radius);
        }
        if (Input.GetMouseButtonUp(1))
        {
            enab = false;
            firstSelect = -1;
            rotateMode = "none";
			this.selectCurObject = null;
        }

        if (enab)
        {
            
            for(int i=0;i<tt.Length;i++)
            {
                Vector2 mPos = getMousePosValid();

                if ((mPos.x > tt[i].x && mPos.x < tt[i].x + 8) && (mPos.y > tt[i].y && mPos.y < tt[i].y + 8))
                {
                    if (firstSelect == -1)
                    {
                        firstSelect = i;
                    }
                    else if (firstSelect != i)
                    {
                        if ((firstSelect > i && !(i == 0 && firstSelect == 3)) || (i == 3 && firstSelect == 0))
                        {
                            rotateMode = "right";
							actionRotate ();
							Invoke ("clearNUM", rateUpdate);
                        }
                        else if (firstSelect < i || (i == 0 && firstSelect == 3))
                        {
                            rotateMode = "left";
							actionRotate ();
							Invoke ("clearNUM", rateUpdate);
                        }
                    }


                }



                int size = 8;

                if (i == firstSelect)
                    size = 14;


                GUI.DrawTexture(new Rect(tt[i].x , tt[i].y , size, size), test);

            }

            
             
             


        }


    }
    

	void clearNUM ()
	{
		firstSelect = -1;
		rotateMode = "none";
	}




}
