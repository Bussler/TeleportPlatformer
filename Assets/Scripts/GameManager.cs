using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour //Manages logic inside the game: highscores, death and reset of player...
{

    public Transform lastCheckpoint=null;

    GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    public void playerDie() // TODO: testing what else to reset
    {
        player.transform.position = lastCheckpoint.position;
    }

}
