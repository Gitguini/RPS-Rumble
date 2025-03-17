using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mind : MonoBehaviour
{

    private RigidbodyControl heart;

    private ScissorsAttack scissors;
    private RockAttack rocc;
    private PaperAttack paper;

    protected Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        heart = GetComponent<RigidbodyControl>();
        scissors = GetComponent<ScissorsAttack>();
        rocc = GetComponent<RockAttack>();
        paper = GetComponent<PaperAttack>();

        mainCam = Camera.main;
    }

    protected void updateTargetVelocity(Vector2 vel) {

        heart.updateTargetVel(vel);

    }

    protected void updateRotation(float angle)
    {
        heart.setRotation(angle);
    }

    protected void scis(float angle)
    {
        scissors.initiateAttack(angle);
    }
    protected void papr()
    {
        paper.initiateAttack(0f);
    }
    protected void rock(float angle)
    {
        rocc.initiateAttack(angle);
    }

}
