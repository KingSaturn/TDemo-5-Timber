using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using player_scope;

public class OakEntController : MonoBehaviour
{
    public float interactDistance;
    private GameObject playerObject;
    private OakEnt_Interact interactManager;
    private Player player;
    private Ethics ethics;
    private CharacterInfo info;

    private void Awake()
    {
        info = this.GetComponent<CharacterInfo>();
        interactManager = this.GetComponentInChildren<OakEnt_Interact>();
    }

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();
        ethics = FindObjectOfType<Ethics>();
        if (player.oakEntGone)
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (info.currentHp <= 0)
        {
            ethics.ethicsDown(70.0f);
            player.oakEntGone = true;
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + 20, transform.position.z);
            ItemSpawner.SpawnItem(newPosition, 1, 20);
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
