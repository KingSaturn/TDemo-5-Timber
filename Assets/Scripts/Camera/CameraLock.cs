using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    private GameObject player;
    private Vector3 cameraOffset;
    // Start is called before the first frame update

    private void Awake()
    {
        player = (GameObject.FindGameObjectsWithTag("Player"))[0];
    }
    void Start()
    {
        cameraOffset = new Vector3(0.0f ,134.0f ,-120.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + cameraOffset;
    }
}
