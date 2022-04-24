using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadZone : MonoBehaviour
{
    private GameObject player;
    private LevelLoader loader;
    public int levelIndex;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        loader = GameObject.FindObjectOfType<LevelLoader>();
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Player"))
        {
            loader.LoadLevel(levelIndex);
        }
    }



}
