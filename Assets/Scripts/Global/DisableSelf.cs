using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSelf : MonoBehaviour
{
    private void Awake()
    {
        this.GetComponent<Canvas>().enabled = false;
    }
}
