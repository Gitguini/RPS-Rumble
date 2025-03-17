using Unity.Netcode;
using UnityEngine;

public class RockAttack : Attack
{

    public GameObject rockPrefab;

    private Vector2 Vangle = Vector2.zero;

    public float throwStrength = 5f;

    private AudioPlayer sounds;

    void Start()
    {
        hitbox = null;
        GameObject mc = Camera.main.gameObject;
        sounds = mc.GetComponent<AudioPlayer>();
    }

    new public void initiateAttack(float angle)
    {
        angle += 90;
        angle *= Mathf.Deg2Rad;

        Vangle.x = Mathf.Cos(angle);
        Vangle.y = Mathf.Sin(angle);

        Vector3 rockSpawnPos = new Vector3(transform.position.x + (Vangle.x * 1.5f), transform.position.y + (Vangle.y * 1.5f), transform.position.z);
        Quaternion rockSpawnAngle = new Quaternion(0, 0, 0, 0);

        Vangle = Vangle * throwStrength;


        //instantiate new rock
        //get its rb component
        //set its velocity to vangle
        GameObject rock = Instantiate<GameObject>(rockPrefab, rockSpawnPos, rockSpawnAngle);

        //NEW step: now that the rock exists, Spawn it on the network by invoking its NetworkObject.Spawn()
        NetworkObject rockNO = rock.GetComponent<NetworkObject>();
        rockNO.Spawn();

        Rigidbody2D rockRB = rock.GetComponent<Rigidbody2D>();
        rockRB.linearVelocity = Vangle;
        sounds.playSound("rock");
    }


    void FixedUpdate()
    {
        
    }
}
