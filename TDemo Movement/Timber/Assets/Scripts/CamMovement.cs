using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public float moveSensitivity = 100f;

    public Transform playerBody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float movementX = Input.GetAxis("Horizontal") * moveSensitivity * Time.deltaTime;
        float movementY = Input.GetAxis("Vertical") * moveSensitivity * Time.deltaTime;

        playerBody.Rotate(Vector3.up * movementX);
    }
}
