using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyControl : MonoBehaviour
{
    /*
     * ...trying to homespin movement isn't working
     * here's an easier idea: rigidbody.
     * It'll probably actually use the layer mask correctly (grumble grumble)
     * basically, we're gonna have it be a dynamic body with no gravity
     * and a continuous force applied towards its Target Velocity
     * (proportional to the difference between current velocity and target velocity)
     * ready? ok
     * 
     */

    public Vector2 targetVel;

    //private Vector2 deltaV;

    //public float accel = 10f;
    //public float decel = 10f;
    //public float accelPower = 2f;

    //private float accelRate = 0f;

    private Rigidbody2D body;

    public float rotation = 0f;

    public float walkspeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.freezeRotation = true;


        targetVel = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //deltaV = targetVel - body.velocity;
        //Debug.Log("deltaV is " + deltaV);

        //accelRate = (targetVel.magnitude > 0.01f) ? accel : decel;

        //apply force proportional to deltaV.magnitude, accelRate, and Time.deltaTime(?)
        
        /*
            Gaze upon these works, ye mighty, and despair
            at all this nonsense i wrote before realizing i could just
            use rigidbody's built-in drag
            is it a little messier to move around? Could it be improved to be more configurable?
            yes but i really don't have the time to perfect every detail. C'mon, me.
        */
        body.AddForce(targetVel);

        //if velocity is sufficiently close to 0 and no force is being applied, zero it
        //if (body.velocity.magnitude < 0.01f && accelRate == decel)
        //{
        //body.velocity = Vector2.zero;
        //}

        body.rotation = rotation;

    }

    public void updateTargetVel(Vector2 vel)
    {
        vel.Normalize();
        targetVel = vel * walkspeed;

    }

    public void setRotation(float newVal)
    {
        rotation = newVal;
    }
    
}
