using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axePickup : MonoBehaviour
{
    public GameObject axe;
    public GameObject tempParent;

    private Transform take;

    public float throwForce = 24f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            transform.parent = null;
            axe.GetComponent<Rigidbody>().AddForce(tempParent.transform.forward * throwForce);
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            take = col.transform;
            transform.parent = take;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            take = col.transform;
        }
    }
}
