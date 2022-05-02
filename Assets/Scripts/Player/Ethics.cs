using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ethics : MonoBehaviour
{
    public GameObject sprite;
    RectTransform rectTransform;

    // Will be used to define the amount of rotation on the arrow and possibly more.
    private float ethicAmount = 0f;

    // Set a Min and Max amount. This can be confusing at first, but min is a positive number (+) and max is a negative number (-), their name come from being good and bad. In rotation values
    // is the other way around.
    public float minEthic, maxEthic;

    // Store current amount just in case to keep it in between loads?
    // private Quaternion currentQuaternion, oldQuaternion;

    void Start()
    {
        rectTransform = sprite.GetComponent<RectTransform>();
    }

    // Trying to change the Z on rotation 
    public void ethicsDown()
    {
        //if(ethicAmount > minEthic)
        //{
        //    return;
        //};
        //ethicAmount -= 10f;
        //Debug.Log(ethicAmount);
        //sprite.transform.Rotate(new Vector3(0, 0, -0.5f));
        ethicAmount -= 10f;
        updateRotation();
        Debug.Log(ethicAmount);
    }

    public void ethicsUp()
    {
        //if (ethicAmount < maxEthic)
        //{
        //    return;
        //};
        //ethicAmount += 10f;
        //sprite.transform.Rotate(new Vector3(0, 0, 0.5f));

        ethicAmount += 10f;
        updateRotation();
    }

    private void updateRotation()
    {
        float zRotation = Mathf.Lerp(-74, 74, (ethicAmount / maxEthic)) + 74;
        zRotation = -(Mathf.Clamp(zRotation, maxEthic, minEthic));
        rectTransform.rotation = Quaternion.Euler(0, 0, zRotation);
        if(zRotation == minEthic)
        {
            ethicAmount -= 10f;
        } else if (zRotation == maxEthic)
        {
            ethicAmount += 10f;
        }
        Debug.Log(zRotation);
    }
}
