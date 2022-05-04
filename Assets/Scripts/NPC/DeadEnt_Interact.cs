using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using player_scope;

public class DeadEnt_Interact : MonoBehaviour
{
    public string[] text;
    private int textPosition = 0;
    public bool hasAccept;
    private Canvas canvas;
    private Button acceptButton;
    private Text speakText;
    private Player player;

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
        player.deadEntGone = true;
        this.GetComponentInParent<BoxCollider>().enabled = false;
        StopInteract();
    }

    private void StopInteract()
    {
        textPosition = 0;
        PauseMenu.isPaused = false;
        canvas.enabled = false;
    }
}
