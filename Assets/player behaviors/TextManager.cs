using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro; //implements the TMPro class
public class TextManager : NetworkBehaviour
{
    /*
     * the purpose of this is simple
     * when a gameobject Dies it tells this before it does.
     * run a client rpc
     * it will check whether you own this gameobject
     * if you do, it will display LOSE
     * if you don't, it will display WIN
     * 
     * I'll also handle the victory/defeat audio here
     * cuz this is a nice place to handle distinguishing between defeat and victory
     */

    private TextMeshProUGUI wintext;
    private TextMeshProUGUI losetext;

    private AudioPlayer sounds;

    void Start()
    {
        GameObject winTexGO = GameObject.Find("wintext");
        GameObject losTexGO = GameObject.Find("lostext");

        wintext = winTexGO.GetComponent<TextMeshProUGUI>();
        losetext = losTexGO.GetComponent<TextMeshProUGUI>();

        wintext.enabled = false;
        losetext.enabled = false;

        GameObject mc = Camera.main.gameObject;
        sounds = mc.GetComponent<AudioPlayer>();
    }

    public void displayDeathText()
    {
        displayDeathTextRpc();
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void displayDeathTextRpc()
    {
        if (IsOwner)
        {
            sounds.playSound("die");
            losetext.enabled = true;
        }
        else
        {
            sounds.playSound("win");
            wintext.enabled = true;
        }
    }
}
