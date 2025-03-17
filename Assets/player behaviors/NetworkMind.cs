using Unity.Netcode;
using UnityEngine;

public class NetworkMind : NetworkBehaviour
{
    /*
     * ...so
     * first of all, makin a subclass of Mind isn't gonna work cuz mind is a monobehavior
     * second of all
     * what does this do
     * 
     * receive input from player
     * check if player is the owner of the gameobject this script runs on
     * if so, send the appropriate rpc to the server for the desired action
     */

    private RigidbodyControl heart;

    private AttackExecutor attacker;

    protected Camera mainCam;


    //for sending through rpcs for movement
    private Vector2 inputVec = Vector2.zero;

    //for detecting clicks/button presses
    private bool mouseWasDownLastFrame = false;
    private bool mouseIsDownThisFrame = false;

    private bool rMouseWasDownLastFrame = false;
    private bool rMouseIsDownThisFrame = false;

    //for detecting tha mouse
    private Vector3 mouseOnScreen = Vector2.zero;
    private Vector3 mouseIn3DSpace = Vector3.zero;
    private Vector2 mousePointer = Vector2.zero;

    //for calculating and sending looking-direction
    private Vector2 facingDir = Vector2.zero;
    private Vector2 Position2D = Vector2.zero;
    private float angle;


    public override void OnNetworkSpawn()
    {

        attacker = gameObject.GetComponent<AttackExecutor>();
        heart = gameObject.GetComponent<RigidbodyControl>();

        mainCam = Camera.main;
    }

    void FixedUpdate()
    {
        //basically everything's gonna be in this block i think
        if (IsOwner)
        {
            //get direction of movement input
            inputVec.x = Input.GetAxis("Horizontal");
            inputVec.y = Input.GetAxis("Vertical");
            if (Mathf.Abs(inputVec.x) > 0.1 && Mathf.Abs(inputVec.y) > 0.1)
            {
                inputVec.Normalize();
            }

            moveRPC(inputVec);

            //get location of mouse relative to us
            updateMousePos();

            Position2D.x = transform.position.x;
            Position2D.y = transform.position.y;

            facingDir = mousePointer - Position2D;

            if (mousePointer.x < Position2D.x)
            {
                angle = Vector2.Angle(Vector2.up, facingDir);
            }
            else
            {
                angle = 0f - Vector2.Angle(Vector2.up, facingDir);
            }

            //with that angle, we can ask to rotate
            rotationRPC(angle);

            //now, last request, attacking

            //we assign these to bools instead of just using the methods as bools because we need them to run
            //once and only once every frame (to update clickedLastFrame stuff)
            bool clicked = checkForClick();
            bool rClicked = checkForRightClick();

            if(clicked)
            {
                nextAtkRPC(angle);
            } else if (rClicked)
            {
                prevAtkRPC(angle);
            }

        }

    }

    //get mouse position in game space
    void updateMousePos()
    {
        mouseOnScreen.x = Input.mousePosition.x;
        mouseOnScreen.y = Input.mousePosition.y;
        mouseOnScreen.z = 10;


        mouseIn3DSpace = mainCam.ScreenToWorldPoint(mouseOnScreen);


        mousePointer.x = mouseIn3DSpace.x;
        mousePointer.y = mouseIn3DSpace.y;
    }

    bool checkForClick()
    {
        mouseIsDownThisFrame = (Input.GetAxis("Fire1") > 0);

        bool checkVal = (!mouseWasDownLastFrame && mouseIsDownThisFrame);

        mouseWasDownLastFrame = mouseIsDownThisFrame;

        return checkVal;
    }

    bool checkForRightClick()
    {
        rMouseIsDownThisFrame = (Input.GetAxis("Fire2") > 0);

        bool checkVal = (!rMouseWasDownLastFrame && rMouseIsDownThisFrame);

        rMouseWasDownLastFrame = rMouseIsDownThisFrame;

        return checkVal;
    }


    //here are our RPCs
    //sent by clients (note to self: make sure client only sends one if it's the owner of this object)
    //executed on the server
    //Move, SetRotation, paper attack, rock attack, scissors attack
    [Rpc(SendTo.Server)]
    void moveRPC(Vector2 vel)
    {
        heart.updateTargetVel(vel);
    }

    [Rpc(SendTo.Server)]
    void rotationRPC(float angle)
    {
        heart.setRotation(angle);
    }

    [Rpc(SendTo.Server)]
    void nextAtkRPC(float angle)
    {
        attacker.nextAttack(angle);
    }

    [Rpc(SendTo.Server)]
    void prevAtkRPC(float angle)
    {
        attacker.prevAttack(angle);
    }
}
