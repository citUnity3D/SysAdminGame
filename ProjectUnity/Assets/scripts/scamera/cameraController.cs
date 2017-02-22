using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {

    public float borderSize = 15f;
    public float camSpeed;

    void Update()
    {
        if (GameObject.Find("GameController").GetComponent<mcontroller>().modeRotate)
            return;

        Vector2 mouseVec = Input.mousePosition;
        mouseVec.y = Screen.height - mouseVec.y;

        Vector3 move = new Vector3(0, 0, 0);

        if (mouseVec.x < borderSize)
        {
            move.z -= camSpeed * Time.deltaTime;
        }

        if (mouseVec.x > Screen.width - borderSize)
        {
            move.z += camSpeed * Time.deltaTime;
        }


        if (mouseVec.y < borderSize)
        {
            move.x -= camSpeed * Time.deltaTime;
        }

        if (mouseVec.y > Screen.height - borderSize)
        {
            move.x += camSpeed * Time.deltaTime;
        }

        

       

        GetComponent<CharacterController>().Move(move);
    }



}
