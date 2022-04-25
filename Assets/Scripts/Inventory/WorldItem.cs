using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timber.InventorySystem;

public class WorldItem : MonoBehaviour
{
    public int id;
    public int amount;
    private Mesh mesh;
    private MeshFilter meshComponent;
    private float meshScale;
    private Material material;
    private MeshRenderer materialComponent;
    private GameObject player;
    private Inventory inventory;

    WorldItem(int passedId, int passedAmount)
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
        meshComponent = this.GetComponent<MeshFilter>();
        materialComponent = this.GetComponent<MeshRenderer>();
        ItemData data = ItemDatabase.GetItemData(id);
        InventoryItem itemToAdd = new InventoryItem(data);
        mesh = data.model;
        material = data.material;
        
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
        transform.position = new Vector3(transform.position.x, transform.position.y + (Mathf.Sin(Time.realtimeSinceStartup * 2) * 0.1f), transform.position.z);
        transform.Rotate(0, Time.deltaTime * 100, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameObject.Find("Lumber_Jack/Armature/Hand_L/held_axe") != null)
        {
            inventory.AddItem(id, amount);
            Destroy(this.gameObject);
        }
    }
}
