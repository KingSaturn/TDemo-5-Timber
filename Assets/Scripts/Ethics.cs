using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ethics : MonoBehaviour
{
    public GameObject sprite;

    // Will be used to define the amount of rotation on the arrow and possibly more.
    private float ethicAmount = 0f;

    // Set a Min and Max amount. This can be confusing at first, but min is a positive number (+) and max is a negative number (-), their name come from being good and bad. In rotation values
    // is the other way around.
    public float minEthic, maxEthic;

    // Store current amount just in case to keep it in between loads?
    // private Quaternion currentQuaternion, oldQuaternion;

    // Start is called before the first frame update
    void Start()
    {
        // oldQuaternion = sprite.GetComponent<RectTransform>().rotation;
    }

    // Trying to change the Z on rotation 
    public void ethicsDown()
    {
        if(ethicAmount < maxEthic)
        {
            return;
        };
        ethicAmount -= 0.5f;
        sprite.GetComponent<RectTransform>().rotation = Quaternion.Euler(0.0f, 0.0f, ethicAmount);
    }

    public void ethicsUp()
    {
        if (ethicAmount > minEthic)
        {
            return;
        };
        ethicAmount += 0.5f;
        sprite.GetComponent<RectTransform>().rotation = Quaternion.Euler(0.0f, 0.0f, ethicAmount);
    }
}
