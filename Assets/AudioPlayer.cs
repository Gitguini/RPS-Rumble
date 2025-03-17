using Unity.Netcode;
using UnityEngine;

public class AudioPlayer : NetworkBehaviour
{
    /*
     * this is used by things which occur on the server, to play sounds to the clients
     * it will have various sound methods, which have associated sounds which they will play
     * via client RPCs.
     */

    public AudioClip rock;
    public AudioClip paper;
    public AudioClip scissors;
    public AudioClip hit;
    public AudioClip die;
    public AudioClip win;

    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void playSound(string soundName)
    {
        playSoundRpc(soundName);
    }

    [Rpc(SendTo.ClientsAndHost)]
    void playSoundRpc(string soundName)
    {
        if (soundName == "rock")
        {
            source.PlayOneShot(rock);
        } else if (soundName == "paper")
        {
            source.PlayOneShot(paper);
        } else if (soundName == "scissors")
        {
            source.PlayOneShot(scissors);
        } else if (soundName == "hit")
        {
            source.PlayOneShot(hit, 0.5f); //half volume because vine boom is LOUD
        } else if (soundName == "die")
        {
            source.PlayOneShot(die);
        } else if (soundName == "win")
        {
            source.PlayOneShot(win);
        } else
        {
            LogIncorrectSoundNameRpc();
        }
    }

    [Rpc(SendTo.Server)] 
    void LogIncorrectSoundNameRpc()
    {
        Debug.Log("Invalid sound name!");
    }
}
