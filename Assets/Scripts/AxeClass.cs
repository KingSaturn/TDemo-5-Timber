using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeClass : MonoBehaviour
{
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame

    public class Axe
    {
        public int damage;
        public float attackSpeed;
        public int range;

        public float chopSpeed;
        public float weight;

        Sprite Sprite;
        
        public Axe()
        {
            damage = 1;
            weight = 0.1f;
            range = 1;
            attackSpeed = 0.1f;
            chopSpeed = 0.1f;
            Sprite = null;
        }
    }
    void Update()
    {
        
    }
}
