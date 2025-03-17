using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleInputMind : Mind
{

    public float speed = 5.0f;

    

    private Vector2 inputVec = Vector2.zero;

    private bool mouseWasDownLastFrame = false;
    private bool mouseIsDownThisFrame = false;

    private Vector3 mouseOnScreen = Vector2.zero;
    private Vector3 mouseIn3DSpace = Vector3.zero;
    private Vector2 mousePointer = Vector2.zero;

    private Vector2 facingDir = Vector2.zero;
    private Vector2 Position2D = Vector2.zero;



    private float angle;


    // Update is called once per frame
    void FixedUpdate()
    {
        //we're going to nab directional inputs and find their combined direction
        //(setting to 0 if no inputs)
        //get a vector of that direction and magnitude [speed]
        //and send that to updateTargetVelocity()

        inputVec.x = Input.GetAxis("Horizontal");
        inputVec.y = Input.GetAxis("Vertical");
        if (Mathf.Abs(inputVec.x) > 0.1 && Mathf.Abs(inputVec.y) > 0.1)
        {
            inputVec.Normalize();
        }

        inputVec *= speed;

        updateTargetVelocity(inputVec);

        updateMousePos();

        Position2D.x = transform.position.x;
        Position2D.y = transform.position.y;
        
        facingDir = mousePointer - Position2D;

        if (mousePointer.x < Position2D.x)
        {
            angle = Vector2.Angle(Vector2.up, facingDir);
        } else
        {
            angle = 0f - Vector2.Angle(Vector2.up, facingDir);
        }

        updateRotation(angle);


        bool clicked = checkForClick();

        if (clicked)
        {
            rock(angle);
        }
        

    }

    bool checkForClick()
    {
        mouseIsDownThisFrame = (Input.GetAxis("Fire1") > 0);

        bool checkVal = (!mouseWasDownLastFrame && mouseIsDownThisFrame);

        mouseWasDownLastFrame = mouseIsDownThisFrame;

        return checkVal;
    }

    void updateMousePos()
    {
        mouseOnScreen.x = Input.mousePosition.x;
        mouseOnScreen.y = Input.mousePosition.y;
        mouseOnScreen.z = 10;
        

        mouseIn3DSpace = mainCam.ScreenToWorldPoint(mouseOnScreen);
        

        mousePointer.x = mouseIn3DSpace.x;
        mousePointer.y = mouseIn3DSpace.y;
    }
}
