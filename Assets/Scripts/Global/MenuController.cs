using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using player_scope;
public class MenuController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Player.Has_axe)
            {
                if (Player.inventory_canvas.enabled)
                {
                    Player.inventory_canvas.enabled = false;
                }
                else
                {
                    Player.inventory_canvas.enabled = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PauseMenu.isPaused)
            {
                PauseMenu.PauseGame();
            }
            else
            {
                PauseMenu.UnpauseGame();
            }
        }
    }
}
