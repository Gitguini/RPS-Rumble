using Unity.Netcode;
using UnityEngine;

public class AttackVisualCoordinator : NetworkBehaviour
{
    /*
     * this script will simply take signals from the AttackExecutor
     * and turn them into client RPCs to visually represent attacks
     * (by enabling and disabling sprite rendering components)
     */

    public void scissorNoseOn()
    {
        if (IsServer)
        {
            scissorNoseOnRpc();
        }
    }

    public void scissorNoseOff()
    {
        if (IsServer)
        {
            scissorNoseOffRpc();
        }
    }

    public void paperOn()
    {
        if (IsServer)
        {
            paperOnRpc();
        }
    }

    public void paperOff()
    {
        if (IsServer)
        {
            paperOffRpc();
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void scissorNoseOnRpc()
    {
        SpriteRenderer nose = gameObject.transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>();
        nose.enabled = true;
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void scissorNoseOffRpc()
    {
        SpriteRenderer nose = gameObject.transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>();
        nose.enabled = false;
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void paperOnRpc()
    {
        SpriteRenderer paper = gameObject.transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>();
        paper.enabled = true;
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void paperOffRpc()
    {
        SpriteRenderer paper = gameObject.transform.GetChild(5).gameObject.GetComponent<SpriteRenderer>();
        paper.enabled = false;
    }
}
