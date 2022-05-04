using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using player_scope;

public class DeadEntController : MonoBehaviour
{
    public float interactDistance;
    private GameObject playerObject;
    private DeadEnt_Interact interactManager;
    private Player player;
    private Ethics ethics;
    private CharacterInfo info;

    private void Awake()
    {
        info = this.GetComponent<CharacterInfo>();
        interactManager = this.GetComponentInChildren<DeadEnt_Interact>();
        ethics = FindObjectOfType<Ethics>();
    }

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();
        if (player.deadEntGone)
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (info.currentHp <= 0)
        {
            ethics.ethicsDown(30.0f);
            player.deadEntGone = true;
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + 20, transform.position.z);
            ItemSpawner.SpawnItem(newPosition, 2, 20);
            Destroy(this.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            float distance = Vector3.Distance(transform.position, playerObject.transform.position);
            if (distance < interactDistance)
            {
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