using UnityEngine;

public class DefenseManager : MonoBehaviour
{
    private int currentState;

    public const int rock = 0;
    public const int paper = 1;
    public const int scissors = 2;

    private NetworkColor colorMan;

    void Start()
    {
        currentState = 0;

        colorMan = gameObject.GetComponent<NetworkColor>();
    }

    public void becomeRock()
    {
        currentState = rock;
        colorMan.setColor(colorMan.rock);
    }

    public void becomePaper()
    {
        currentState = paper;
        colorMan.setColor(colorMan.paper);
    }

    public void becomeScissors()
    {
        currentState = scissors;
        colorMan.setColor(colorMan.scissors);
    }

    public int getDefState()
    {
        return currentState;
    }
}
