using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using player_scope;

public class HealthBar : MonoBehaviour
{
    private Image[] healthBars;
    private Player player;

    private void Awake()
    {
        healthBars = this.GetComponentsInChildren<Image>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();
    }

    public void UpdateHealth()
    {
        for (int x = 0; x < 10; x+= 1)
        {
            if (player.info.currentHp <= x * 10)
            {
                healthBars[x].enabled = false;
            }
        }
    }
}
