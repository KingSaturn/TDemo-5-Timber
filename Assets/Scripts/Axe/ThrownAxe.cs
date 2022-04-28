using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownAxe : MonoBehaviour 
{
    private Rigidbody rb;
    public bool inWall = false;
    private bool canDamage = true;
    private PlayerInfo playerInfo;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10);
        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Wall"))
            {
                inWall = true;
            }    
        }
        
    }
    private void Start()
    {
        transform.Rotate(new Vector3(90, 0, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterInfo>() != null)
        {
            if (other.name == "Lumber_Jack")
            {
                return;
            }
            if (other.name == "Mushroom")
            {
                if (other == other.GetComponent<BoxCollider>())
                {
                    damageEnemy(other);
                }    
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -20)
        {
            transform.position = new Vector3(transform.position.x, 100, transform.position.z);
            rb.velocity = Vector3.zero;
        }
    }

    private void damageEnemy(Collider other)
    {
        float speed = (Mathf.Sqrt(rb.velocity.x * rb.velocity.x));
        if (speed > 10 && canDamage)
        {
            CharacterInfo info = other.GetComponent<CharacterInfo>();
            if (speed > 50)
            {
                if (speed > 100)
                {
                info.TakeDamage(playerInfo.attack.GetValue() * 4);
                }
                else
                {
                info.TakeDamage(playerInfo.attack.GetValue() * 2);
                }
            }
            else
            {
                info.TakeDamage(playerInfo.attack.GetValue());
            }
            canDamage = false;
        }
    }
}
