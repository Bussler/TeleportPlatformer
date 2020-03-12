using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerBehaviour : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject flamethrower;

    [SerializeField]
    Sprite open;

    [SerializeField]
    Sprite close;

    bool isflaming;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name=="Player")
        {
            isflaming = true;
            StartCoroutine(flaming());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isflaming = false;
            StopCoroutine(flaming());
        }
    }

    IEnumerator flaming()
    {
        while(isflaming)
        {
            animator.Play("Flame");

            yield return new WaitForSeconds(3);

            animator.Play("FlameClose");

            yield return new WaitForSeconds(2);
        }
        
    }


    public void openSprite()
    {
        flamethrower.SetActive(true);
        GetComponent<SpriteRenderer>().sprite = open;
    }

    public void closeSprite()
    {
        flamethrower.SetActive(false);
        GetComponent<SpriteRenderer>().sprite = close;
    }

}
