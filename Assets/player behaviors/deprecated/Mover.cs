using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    /*
     * moveHeart is the heart of movement, this is the legs.
     * given a vector to move, checks for collisions and moves if none exist.
     * if some exist, carries out the collided objects CollideBehavior
     * 
     */


    private RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    private int hitCount;

    private ContactFilter2D contactFilter;

    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;

    }

    public void move(Vector2 vel)
    {
        //given a vector, cast our hitbox that way.
        //if we don't hit something, translate our position that way.

        //if we hit something, we want configurable behavior based on what it hit.
        //usually, just "don't move."
        //for certain attacks, "damage hit object" or "this object"
        //so... if we hit a hitbox, find the struck object, look for a CollisionBehavior?

        hitCount = body.Cast(vel, hitBuffer, vel.magnitude);
        if (hitCount > 0) //if we hit something
        {
            RaycastHit2D thisHit;

            for (int i = 0; i < hitCount; ++i)
            {

                thisHit = hitBuffer[i];
                findAndExecuteHitBehavior(thisHit.collider.gameObject);

            }

        } else
        {
            proceedWithMovement(vel);
        }
    }

    void findAndExecuteHitBehavior(GameObject hitThing)
    {

    }

    void proceedWithMovement(Vector2 vel)
    {
        //this is for successful uninterrupted movement
        //simply translate by Vel
        transform.Translate(vel);
    }

}
