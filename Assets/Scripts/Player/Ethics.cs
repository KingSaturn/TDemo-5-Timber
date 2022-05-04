using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ethics : MonoBehaviour
{
    public GameObject sprite;
    RectTransform rectTransform;
    private PlayerInfo info;

    // Set a Min and Max amount. This can be confusing at first, but min is a positive number (+) and max is a negative number (-), their name come from being good and bad. In rotation values
    // is the other way around.
    public float minEthic, maxEthic;

    // Store current amount just in case to keep it in between loads?
    // private Quaternion currentQuaternion, oldQuaternion;

    void Start()
    {
        info = this.GetComponentInParent<PlayerInfo>();
        rectTransform = sprite.GetComponent<RectTransform>();
        updateRotation();
    }

    // Trying to change the Z on rotation 
    public void ethicsDown(float x)
    {
        //if(ethicAmount > minEthic)
        //{
        //    return;
        //};
        //ethicAmount -= 10f;
        //Debug.Log(ethicAmount);
        //sprite.transform.Rotate(new Vector3(0, 0, -0.5f));
        info.ethics -= x;
        updateRotation();
    }

    public void ethicsUp(float x)
    {
        //if (ethicAmount < maxEthic)
        //{
        //    return;
        //};
        //ethicAmount += 10f;
        //sprite.transform.Rotate(new Vector3(0, 0, 0.5f));

        info.ethics += x;
        updateRotation();
    }

    private void updateRotation()
    {
        float zRotation = (Mathf.Clamp(info.ethics, maxEthic, minEthic));
        rectTransform.rotation = Quaternion.Euler(0, 0, zRotation);
        Debug.Log(zRotation);
    }
}
