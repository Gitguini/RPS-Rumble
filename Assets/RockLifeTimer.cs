using Unity.Netcode;
using UnityEngine;

public class RockLifeTimer : NetworkBehaviour
{
    public float secsAlive = 3f;
    private float timer;

    public override void OnNetworkSpawn()
    {
        //only the server needs to keep track of the rock timer
        if (IsServer)
        {
            timer = secsAlive;
        }
    }

    void FixedUpdate()
    {
        if (IsServer)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {

                GameObject.Destroy(gameObject);
            }
        }
    }
}
