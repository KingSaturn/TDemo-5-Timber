using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownAxe : MonoBehaviour
{
    private Rigidbody rigidbody;
    private bool canDamage = true;

    private void Awake()
    {
        rigidbody = this.GetComponent<Rigidbody>();
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
        
    }

    private void damageEnemy(Collider other)
    {
        float speed = (Mathf.Sqrt(rigidbody.velocity.x * rigidbody.velocity.x));
        if (speed > 10 && canDamage)
        {
            CharacterInfo info = other.GetComponent<CharacterInfo>();
            info.TakeDamage(10);
            Debug.Log(other.name + "took 10 Damage!");
            canDamage = false;
        }
    }
}
