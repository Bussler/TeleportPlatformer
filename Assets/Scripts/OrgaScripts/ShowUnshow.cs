using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUnshow : MonoBehaviour
{
    [SerializeField]
    GameObject target = null;

    void Update() // maybe better to use unity event system?
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (target.activeInHierarchy)
            {
                target.SetActive(false);
            }
            else
            {
                target.SetActive(true);
            }
        }
    }
}
