using UnityEngine;

public class PaperAttack : Attack
{

    public float secsUp = 10f;
    private float upTime;

    private AttackVisualCoordinator visuals;
    private AudioPlayer sounds;

    void Start()
    {
        upTime = secsUp;
        hitbox = gameObject.transform.GetChild(3).gameObject.GetComponent<Collider2D>(); // get paper hitbox

        visuals = GetComponent<AttackVisualCoordinator>();
        GameObject mc = Camera.main.gameObject;
        sounds = mc.GetComponent<AudioPlayer>();
    }

    new public void initiateAttack(float angle)
    {
        hitbox.enabled = true;
        visuals.paperOn();
        sounds.playSound("paper");
        upTime = 0;
    }

    void FixedUpdate()
    {
        if (upTime < secsUp)
        {
            upTime += Time.deltaTime;
        }
        else
        {
            hitbox.enabled = false;
            visuals.paperOff();
        }
    }
}
