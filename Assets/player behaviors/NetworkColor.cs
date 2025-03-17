using Unity.Netcode;
using UnityEngine;

public class NetworkColor : NetworkBehaviour
{
    /*
     * The purpose of this script is for each player to have a color which is associated with their state
     * (brown for rock, white for paper, gray for scissors)
     * and to synchronize these colors across clients.
     * 
     * When DefenseManager updates its state, it will inform this script.
     * This script will set an internal Color for itself, and send that information to clients.
     * The clients will set this gameObject to render as that color.
     */

    private Color currentColor;

    public Color rock = Color.cyan;
    public Color scissors = Color.gray;
    public Color paper = Color.white;

    //the thing to remember here is that ONLY the server needs to manage what color the thing is and ONLY the clients need to render it
    public override void OnNetworkSpawn()
    {
        //only the server needs to actually manage what color the thing is
        if (IsServer)
        {
            currentColor = rock;
        }
    }

    public void setColor(Color newCol)
    {
        if (IsServer)
        {
            changeColorRpc(newCol);
        }
    }


    //so, this will get run on This Object in All Clients when it's invoked
    //right, good
    [Rpc(SendTo.ClientsAndHost)]
    void changeColorRpc(Color newCol)
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.color = newCol;
    }
}
