using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDestroyPlayer : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")//kill player
        {
            TeleportRay TRay = collision.gameObject.GetComponent<TeleportRay>();
            if (TRay != null && TeleportRay.hasTarget)//if we are teleporting, we let them pass
                return;

            GameObject.FindGameObjectWithTag("GManager").GetComponent<GameManager>().playerDie();
        }
    }

}
