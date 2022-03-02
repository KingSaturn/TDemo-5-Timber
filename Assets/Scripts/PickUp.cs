using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Test to make it pickup the axe just by touching it.
    public Transform pickupCheck;
    public float radiusCheck;
    public LayerMask axeMask;

    // Testing efficiency?
    //bool pickupAxe;

    public float throwForce = 100;
    Vector3 objectPosition;
    float distance;

    public bool canHold = true;
    public GameObject item;
    public GameObject tempParent;
    public bool isHolding = false;

    //public Camera cam;

    //Trying to add a timer for the drop and pickup. 
    public float grabTimer = 2f;

    // Update is called once per frame
    void Update()
    {
        // Added a "variable"? for this to make it easier to read and take less time writing that text that is used frequently, aka for efficiency. 
        Rigidbody rbc = item.GetComponent<Rigidbody>();

        // not working the mouse position to throw
        //vector3 direction = cam.screenpointtoray(input.mouseposition).direction;

        // Checks the distance between the player and the object. 
        distance = Vector3.Distance(item.transform.position, tempParent.transform.position);

        // Drop item, not throw. 
        if (Input.GetKeyDown("q") || distance >= 2.4f)
        {
            isHolding = false;
        };

        // pickupAxe = Physics.CheckSphere(pickupCheck.position, radiusCheck, axeMask);

        if (Physics.CheckSphere(pickupCheck.position, radiusCheck, axeMask) && grabTimer <= 0)
        {
            isHolding = true;
            rbc.useGravity = false;
            rbc.detectCollisions = true;
            grabTimer = 2f;
        }

        //Check if its holding
        if (isHolding)
        {
            rbc.velocity = Vector3.zero;
            rbc.angularVelocity = Vector3.zero;
            item.transform.SetParent(tempParent.transform);

            if (Input.GetMouseButton(1))
            {
                //throw
                rbc.AddForce(tempParent.transform.position.x, tempParent.transform.position.y, tempParent.transform.position.z * throwForce * (Time.deltaTime + 0.5f));
                isHolding = false;
                rbc.useGravity = true;

                Debug.Log(tempParent.transform.position.x);
            }
        }
        else
        {
            objectPosition = item.transform.position;
            item.transform.SetParent(null);
            rbc.useGravity = true;
            item.transform.position = objectPosition;
            grabTimer -= Time.deltaTime;

            if (grabTimer <= 0)
            {
                grabTimer = 0f;
            }
        }
    }
}
