using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") // set latest checkpoint
        {
            GameObject.FindGameObjectWithTag("GManager").GetComponent<GameManager>().lastCheckpoint = this.transform;
        }
    }

}
