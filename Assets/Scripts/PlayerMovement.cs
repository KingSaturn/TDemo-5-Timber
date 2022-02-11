using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Collider boxCollider;

    public float speed = 10.0f;
    float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2 (direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle (transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
            controller.Move(speed * Time.deltaTime * direction);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            boxCollider.attachedRigidbody.AddForce(0, 200, 0);
        }    
}
}
