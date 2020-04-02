using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportRay : MonoBehaviour
{
    RaycastHit2D curHit;
    Transform curSelection;//transform of currently selected teleportable
    Vector2 targetPos = Vector2.zero; //position that ray hit

    GameObject curStick; // pos to stick at when teleporting, don't teleport to the same object twice!
    bool isSticking=false;

    [SerializeField]
    LayerMask target= 1<<8; // only layer 8

    [SerializeField]
    float RayTravelDistance = 10;

    [SerializeField]
    float teleportSpeed=12;
    [SerializeField]
    float teleportMultiplier = 3f;

    public float addedStepMultiplier = 1;//add the teleportMultiplier to accelerate the teleport over time

    [SerializeField]
    Material highlightMaterial=null;
    [SerializeField]
    Material defaultMaterial=null;

    [SerializeField]
    ParticleSystem clickPartSystem=null;

    [SerializeField]
    ParticleSystem playerTeleportPartSystem = null;

    [SerializeField]
    AudioClip teleportSound = null;

    [SerializeField]
    GameObject clone = null; //clone at old position when teleporting

    GameObject curClone = null;

    public static bool hasTarget = false; // check, if we are floating to a target

    private bool hasClicked = false;

    public bool teleportingThrough = false;//flag set by teleportThrough game object

    // Update is called once per frame
    void Update()
    {
        if(isSticking && Input.GetButtonDown("Jump"))//when sticking to a teleportable, disable sticking with jump
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation; // freeze at pos
            Move2D.isAllowedToMove = true; // enable movement again
            isSticking = false;
        }

        castRay();

        if (Input.GetMouseButtonDown(0) && (curStick==null || curHit.transform.gameObject != curStick) && !hasClicked)
            TeleportToHit(curHit);

        if(hasTarget)
            movePlayer(targetPos);

        if (teleportingThrough)
        {
            //check distance to targetPos
            float distance = Vector2.Distance(new Vector2(this.transform.position.x, this.transform.position.y), targetPos);
            if (distance <= 0.1f) resetStats();
        }

    }

    void castRay() // highlights possible teleportable gameobjects and safes lates Hit
    {
        Vector3 mousePos = Input.mousePosition;//gives mouse pos in screen space
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);//convert screen pos to world pos to look at
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, //create vector to face the mouse pos
                                        mousePos.y - transform.position.y);

        curHit = Physics2D.Raycast(transform.position, direction, RayTravelDistance, target); //casts a ray, stores result in curHit

        if (curHit && curHit.transform.gameObject != curStick) // highlighting current teleportable
        {
            if(curHit.transform!=curSelection && curSelection!=null) // set back material, if the new object is right besides the old one
                curSelection.GetComponent<SpriteRenderer>().material = defaultMaterial;

            SpriteRenderer selectRenderer = curHit.transform.GetComponent<SpriteRenderer>();
            curSelection = curHit.transform;
            if (selectRenderer != null)
                selectRenderer.material = highlightMaterial;
        }
        else //when we hit nothing set back material
        {
            if (curSelection != null)
            {
                curSelection.GetComponent<SpriteRenderer>().material = defaultMaterial;
                curSelection = null;
            }
        }

    }

    void TeleportToHit(RaycastHit2D RayHit)//Teleports to latest raycasthit
    {
        hasClicked = true;
        StartCoroutine(clickCountdown());

        if (RayHit)
        {
            //Update: For Teleport through, teleport behind target
            if (RayHit.transform.gameObject.GetComponent<TeleportThrough>())
            {
                //getting direction vector
                Vector3 mousePos = Input.mousePosition;//gives mouse pos in screen space
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);//convert screen pos to world pos to look at
                Vector2 direction = new Vector2(mousePos.x - transform.position.x, //create vector to face the mouse pos
                                                mousePos.y - transform.position.y);
                direction /= direction.magnitude; //normalisieren

                if(direction.x>=0) direction.x += 0.5f;
                else direction.x -= 0.5f;
                if(direction.y>=0) direction.y += 0.5f;
                else direction.y -= 0.5f;

                targetPos = RayHit.point+direction;
            }
            else
            {
                targetPos = RayHit.point;//for other teleport straight to hitpoint
            }

            //Sets intern logic: target to move to, unaffected by physic...
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll; //better than kinematic: make obj unaffected by physic but still enable triggers
            hasTarget = true;

            //starts visual feedback
            Instantiate(clickPartSystem, targetPos, Quaternion.identity); //particle system at click pos
            GetComponent<AudioSource>().PlayOneShot(teleportSound, 0.15f); //teleport sound

            gameObject.GetComponent<SpriteRenderer>().enabled = false;//disables renderer

            curClone = Instantiate(clone, this.transform.position, Quaternion.identity);//create clone/afterimage at prev position

            //Instantiate(playerTeleportPartSystem, targetPos, Quaternion.identity); ;//particle system that follows player
        }

    }

    IEnumerator clickCountdown()
    {
        yield return new WaitForSeconds(0.2f);
        hasClicked = false;
        StopCoroutine(clickCountdown());
    }

    void movePlayer(Vector2 target)//lerp player to position
    {
        addedStepMultiplier += teleportMultiplier*Time.deltaTime;//acceleration over time
        float step = teleportSpeed * addedStepMultiplier * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    void endTeleport() // set back the logic and visual feedback after teleporting
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;//enables renderer
        if(curClone!=null)
            Destroy(curClone, 0.2f);

        addedStepMultiplier = 1;
        hasTarget = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) // check, if we reached the target
    {

        if (hasTarget && collision.gameObject.tag == "Teleport") // wenn wir auf ein Teleport fliegen, kleben wir an diesem TODO: bei moving objects: parent des players setzen
        {
            Debug.Log("CorrectEnter");
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll; // freeze at pos
            curStick = collision.gameObject;// ignore current target for raycasting
            Move2D.isAllowedToMove = false; // disable movement input
            isSticking = true;

            endTeleport();
        }
        else if (hasTarget)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation; // make the gameobject affected by physics again. Freeze Rot to noch change the rb on the impact
            endTeleport();
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == curStick) // delete curStick restricktions when leaving the curr stick
        {
            Debug.Log("CorrectExit");
            curStick = null;
            isSticking = false;

        }

    }


    public void resetStats()
    {
        teleportingThrough = false;

        gameObject.GetComponent<SpriteRenderer>().enabled = true;//enables renderer
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation; // freeze at pos
        curStick = null;
        isSticking = false;
        Move2D.isAllowedToMove = true;

        gameObject.GetComponent<Move2D>().isGrounded = true; // enable jump after for double jump

        endTeleport();
    }

}
