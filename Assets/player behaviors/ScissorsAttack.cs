using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScissorsAttack : Attack
{
    public float secsUp = 10f;
    private float upTime;
    private Vector2 Vangle = Vector2.zero;

    public float dashStrength = 10f;

    private AttackVisualCoordinator visuals;

    private AudioPlayer sounds;


    // Start is called before the first frame update
    void Start()
    {
        upTime = secsUp;
        hitbox = gameObject.transform.GetChild(2).gameObject.GetComponent<Collider2D>(); // get scissors hitbox

        visuals = GetComponent<AttackVisualCoordinator>();
        GameObject mc = Camera.main.gameObject;
        sounds = mc.GetComponent<AudioPlayer>();
    }

    new public void initiateAttack(float angle)
    {
        angle += 90;
        angle *= Mathf.Deg2Rad;
        hitbox.enabled = true;
        visuals.scissorNoseOn();
        sounds.playSound("scissors");
        upTime = 0;

        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        Vangle.x = Mathf.Cos(angle);
        Vangle.y = Mathf.Sin(angle);

        rb.linearVelocity = Vangle * dashStrength;

    }

    void FixedUpdate()
    {
        if (upTime < secsUp)
        {
            upTime += Time.deltaTime;
        } else
        {
            hitbox.enabled = false;
            visuals.scissorNoseOff();
        }
    }

}
