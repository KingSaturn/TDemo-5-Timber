using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject player;
    public Camera mainCam;
    public PlayerInfo info;
    public AxeClass axe;

    // Speed will change depending on having axe or not and if we add anything else that affects it. 
    public float jumpHeight = 20f;

    // Gravity ground 
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    //public Transform axePosition;
    Vector3 velocity;
    bool isGrounded;

    // Just to have an idea
    // bool axePicked;

    // Attack components
    public Transform enemyCheck;
    public LayerMask enemyMask;

    // Update is called once per frame
    void Update()
    {
        // Rotation attemp 2... This one worked.
        Aim();

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * info.speed.GetValue() * Time.deltaTime);

        velocity.y += info.gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * info.gravity);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Attack();
        }
    }

    // Attacking function
    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(enemyCheck.position, (info.attackRange.GetValue() + axe.range.GetValue()), enemyMask);

        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(info.attack.GetValue() + axe.damage.GetValue());
            Debug.Log("Hit");
        }
    }

    // Check weapon range
    private void OnDrawGizmosSelected()
    {
        if (enemyCheck == null)
            return;

        Gizmos.DrawSphere(enemyCheck.position, (info.attackRange.GetValue() + axe.range.GetValue()));
    }

    // Rotation functions
    private (bool success, Vector3 position) GetMousePosition()
    {
        // Creates a ray variable to store the info from the ray, it defines the parameters to 'shoot', being from the camara to, in this case, the mouse position.
        var ray = mainCam.ScreenPointToRay(Input.mousePosition);

        // With Physics the ray gets actually casts so that information can be obtained from it, it takes the parameters of the ray, being the camara to the mouse. Then
        // it creates a variable to store the Vector3 that results from the colision of the ray. I am guessing mathf infinity is to set the limit distance of the ray, in this case
        // infinite. Finally groundMask is to set the objects the ray can collide with. In our game being the ground layer.
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            // The Raycast hit something, return with the position
            return (success: true, position: hitInfo.point);
        }
        else
        {
            // The Raycast did not hit anything, return Vector3(0, 0, 0) and false so that Aim() doesn't happen. 
            return (success: false, position: Vector3.zero);
        }
    }

    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            // In the case it gets a success, then store the position of the mouse - the player current position. This gives the direction to look at... for some reason.
            // Comment from video: Calculate the distance
            var direction = position - player.transform.position;

            // Ignore the height difference
            direction.y = 0;

            // Make the player look at the direction.
            player.transform.forward = direction;
        }
    }
}
