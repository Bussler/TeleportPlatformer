using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    private Color color;
    [SerializeField]
    float timeToFade = 0.2f;

    void Start()//grab the color of the renderer, but set its alpha to 0, in order to later interpolate them
    {
        color = gameObject.GetComponent<SpriteRenderer>().material.color;
        color.a = 0;
    }

    void FixedUpdate()
    {
        gameObject.GetComponent<SpriteRenderer>().material.color = Color.Lerp(gameObject.GetComponent<SpriteRenderer>().material.color, color, timeToFade);
    }
}
