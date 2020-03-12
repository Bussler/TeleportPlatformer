using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [SerializeField]
    public Transform target;

    [SerializeField]
    float smoothSpeed = 10f;//0.125f;

    [SerializeField]
    Vector3 offset = new Vector3(0,2,-15);

    private void FixedUpdate()// run after update
    {
        Vector3 targetPos = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, targetPos, smoothSpeed*Time.deltaTime);

        transform.position = smoothedPos;
    }

}
