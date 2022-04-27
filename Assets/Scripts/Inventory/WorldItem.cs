using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timber.InventorySystem;
using player_scope;

public class WorldItem : MonoBehaviour
{
    public int id;
    public int amount;
    private float startingY;
    private Mesh mesh;
    private MeshFilter meshComponent;
    private float meshScale;
    private Material material;
    private MeshRenderer materialComponent;
    private GameObject player;
    private Inventory inventory;

    public void SetWorldItem (int passedId, int passedAmount)
    {
        ItemData data = ItemDatabase.GetItemData(passedId);
        InventoryItem itemToAdd = new InventoryItem(data);
        id = passedId;
        amount = passedAmount;
        mesh = data.model;
        material = data.material;
    }

    private void Awake()
    {
        startingY = transform.position.y;
        meshComponent = this.GetComponent<MeshFilter>();
        materialComponent = this.GetComponent<MeshRenderer>();
        if (mesh == null)
        {
            ItemData data = ItemDatabase.GetItemData(id);
            InventoryItem itemToAdd = new InventoryItem(data);
            mesh = data.model;
            material = data.material;
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();
        meshComponent.mesh = mesh;
        materialComponent.material = material;
    }

    private void Update()
    {
        if (PauseMenu.isPaused)
        {
            return;
        }
        float sinMovement = startingY + (Mathf.Sin(Time.realtimeSinceStartup * 2) * 10);
        transform.position = new Vector3(transform.position.x, sinMovement, transform.position.z);
        transform.Rotate(0, Time.deltaTime * 100, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Player.Has_axe)
        {
            inventory.AddItem(id, amount);
            Destroy(this.gameObject);
        }
    }
}
