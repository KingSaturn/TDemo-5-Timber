using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    private GameObject player;
    private Vector3 cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        player = (GameObject.FindGameObjectsWithTag("Player"))[0];
        cameraOffset = new Vector3(80.0f ,134.0f ,-90.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + cameraOffset;
    }
}
