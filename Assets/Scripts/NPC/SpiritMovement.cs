using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritMovement : MonoBehaviour
{
    private float startingY;

    private void Start()
    {
        startingY = transform.position.y;
    }
    void Update()
    {
        float sinMovement = startingY + (Mathf.Sin(Time.realtimeSinceStartup * 4 + Random.Range(0,500) * 5));
        transform.position = new Vector3(transform.position.x, sinMovement, transform.position.z);
    }
}
