using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using player_scope;

public class OakEnt_Interact : MonoBehaviour
{
    public string[] text;
    private int textPosition = 0;
    public bool hasAccept;
    private Canvas canvas;
    private Button acceptButton;
    private Text speakText;
    private Player player;
    private Ethics ethics;
    private InventoryUI inv;

    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
        acceptButton = canvas.GetComponentsInChildren<Button>()[1];
        speakText = this.GetComponentsInChildren<Text>()[0];
        canvas.enabled = false;
        acceptButton.enabled = false;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ethics = player.gameObject.GetComponentInChildren<Ethics>();
        inv = player.gameObject.GetComponentInChildren<InventoryUI>();
    }


    public void Interact()
    {
        if (PauseMenu.isPaused)
        {
            return;
        }
        speakText.text = text[0];
        PauseMenu.isPaused = true;
        canvas.enabled = true;
        if (hasAccept)
        {
            acceptButton.enabled = true;
        }
    }

    public void ConfirmButton()
    {
        textPosition += 1;
        if (text.Length == textPosition)
        {
            StopInteract();
        }
        else
        {
            speakText.text = text[textPosition];
        }
    }

    public void AcceptButton()
    {
        if (inv.inventory.GetItemCount(1) > 0)
        {
            inv.inventory.RemoveItem(1, 1);
            ethics.ethicsUp(2.0f);
        }

        if (inv.inventory.GetItemCount(2) > 0)
        {
            inv.inventory.RemoveItem(2, 1);
            ethics.ethicsUp(1.0f);
        }
    }

    private void StopInteract()
    {
        textPosition = 0;
        PauseMenu.isPaused = false;
        canvas.enabled = false;
    }
}
