using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHP = 100;
    private float timer = 0f;
    private float waitTime = 5f;

    // Getting the arrow from the ethics to change the rotation.
    public GameObject Ethic;

    int currentHP;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }
    
    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if(currentHP <= 0)
        {
            Died();
        }
    }
    void Died()
    {
        Debug.Log("AAA, died.");
        this.enabled = false;
        this.GetComponent<Collider>().enabled = false;

        // Not working for the moment
        Ethic.GetComponent<Ethics>().ethicsDown();

        // Future
        //timer += Time.deltaTime;
        //this.GetComponent<Transform>().localScale = Vector3.Lerp(this.GetComponent<Transform>().localScale, Vector3.zero, timer);
    }
}
