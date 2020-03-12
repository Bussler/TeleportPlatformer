using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{

    void Update()
    {
        lookMouse();
    }

    void lookMouse()
    {
        Vector3 mousePos = Input.mousePosition;//gives mouse pos in screen space
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);//convert screen pos to world pos to look at

        Vector2 direction = new Vector2(mousePos.x-transform.position.x, //create vector to face the mouse pos
                                        mousePos.y-transform.position.y);


        /*Vector3 direction = new Vector3(mousePos.x - transform.position.x, //create vector to face the mouse pos
                                         mousePos.y - transform.position.y,
                                        0);*/

        transform.right = direction; //up direction of transform should point to mouse
    }

}
