using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHeart : MonoBehaviour
{
    /*
     * The heart of movement.
     * Keeps and uses velocity.
     * Given a targetVel, accelerates vel towards it.
     * 
     */

    private Mover mover;
    public Vector2 vel;
    public Vector2 targetVel;

    private Vector2 deltaV;

    public float accel = 0.1f;
    public float decel = 0.1f;
    public float accelPower = 2f;

    private float accelRate = 0f;

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponent<Mover>();
        vel = Vector2.zero;
        targetVel = Vector2.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        deltaV = targetVel - vel;

        accelRate = (targetVel.magnitude > 0.01f) ? accel : decel;

        vel = vel + (deltaV * accelRate * Time.deltaTime);

        vel = (vel.magnitude > 0.01f) ? vel : Vector2.zero;

        mover.move(vel * Time.deltaTime);
    }

    public void updateTargetVel(Vector2 vel)
    {
        targetVel = vel;
    }
}
