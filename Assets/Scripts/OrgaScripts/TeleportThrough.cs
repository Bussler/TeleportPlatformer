using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportThrough : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject player = collision.gameObject;
        if (player.name == "Player")
        {
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;

            player.GetComponent<TeleportRay>().resetStats();

            StartCoroutine("respawn");
        }
    }
   

    IEnumerator respawn()
    {
        yield return new WaitForSeconds(3);

        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;

        StopCoroutine("respawn");
    }


}
