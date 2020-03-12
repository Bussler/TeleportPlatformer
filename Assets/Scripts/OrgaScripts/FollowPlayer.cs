using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    public Transform target;

    [SerializeField]
    float smoothSpeed = 100f;//0.125f;

    private void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPos = target.position;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPos;
    }
}
