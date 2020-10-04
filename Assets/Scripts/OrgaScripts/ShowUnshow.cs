using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUnshow : MonoBehaviour
{
    [SerializeField]
    GameObject target = null;

    [SerializeField]
    float slowTime = 0.4f;

    void Update() // maybe better to use unity event system?
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (target.activeInHierarchy)
            {
                disableRay();
            }
            else
            {
                enableRay();
            }
        }
    }

    public void disableRay()
    {
        target.SetActive(false);
        Time.timeScale = 1f;
    }

    public void enableRay()
    {
        target.SetActive(true);
        Time.timeScale = slowTime;
    }

}
