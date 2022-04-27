using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using player_scope;

public class PeasentController : MonoBehaviour
{
    public float interactDistance;
    private float faceTargetTimer = 0.0f;
    private GameObject player;
    private NPC_Interact interactManager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        interactManager = this.GetComponentInChildren<NPC_Interact>();
    }

    private void Update()
    {
        if (faceTargetTimer > 0)
        {
            faceTargetTimer -= Time.deltaTime;
            Vector3 direction = (player.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3.5f);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < interactDistance)
            {
                faceTargetTimer = 1.0f;
                interactManager.Interact();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, interactDistance);
    }

}
