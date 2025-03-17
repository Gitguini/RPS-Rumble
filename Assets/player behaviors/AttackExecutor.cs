using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackExecutor : MonoBehaviour
{
    private ScissorsAttack scissors;
    private RockAttack rock;
    private PaperAttack paper;

    private DefenseManager def;

    private RigidbodyControl heart;

    public float attackCooldown = 0.5f;
    private float attackCounter;

    public float rockCooldown = 1.5f;
    private float rockCounter;

    public float paperCooldown = 1.5f;
    private float paperCounter;

    public float scissorsCooldown = 1.5f;
    private float scissorsCounter;



    // Start is called before the first frame update
    void Start()
    {
        heart = GetComponent<RigidbodyControl>();
        scissors = GetComponent<ScissorsAttack>();
        rock = GetComponent<RockAttack>();
        paper = GetComponent<PaperAttack>();

        def = GetComponent<DefenseManager>();

        attackCounter = 0;
        rockCounter = 0;
        paperCounter = 0;
        scissorsCounter = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;
        }

        if (rockCounter > 0)
        {
            rockCounter -= Time.deltaTime;
        }

        if (paperCounter > 0)
        {
            paperCounter -= Time.deltaTime;
        }

        if (scissorsCounter > 0)
        {
            scissorsCounter -= Time.deltaTime;
        }
    }

    public void nextAttack(float angle)
    {
        if (def.getDefState() == DefenseManager.rock)
        {
            paprAttack();
        } else if (def.getDefState() == DefenseManager.paper)
        {
            scisAttack(angle);
        } else if (def.getDefState() == DefenseManager.scissors)
        {
            rockAttack(angle);
        }
    }

    public void prevAttack(float angle)
    {
        if (def.getDefState() == DefenseManager.rock)
        {
            scisAttack(angle);
        }
        else if (def.getDefState() == DefenseManager.paper)
        {
            rockAttack(angle);
        }
        else if (def.getDefState() == DefenseManager.scissors)
        {
            paprAttack();
        }
    }

    public void scisAttack(float angle)
    {
        if (attackCounter <= 0 && scissorsCounter <= 0)
        {
            attackCounter = attackCooldown;
            scissorsCounter = scissorsCooldown;
            scissors.initiateAttack(angle);
            def.becomeScissors();
        }
    }

    public void paprAttack()
    {
        if (attackCounter <= 0 && paperCounter <= 0)
        {
            attackCounter = attackCooldown;
            paperCounter = paperCooldown;
            paper.initiateAttack(0f);
            def.becomePaper();
        }
    }

    public void rockAttack(float angle)
    {
        if (attackCounter <= 0 && rockCounter <= 0)
        {
            attackCounter = attackCooldown;
            rockCounter = rockCooldown;
            rock.initiateAttack(angle);
            def.becomeRock();
        }
    }
}
