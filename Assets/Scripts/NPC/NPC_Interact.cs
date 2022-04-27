using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Interact : MonoBehaviour
{
    public string[] text;
    private int textPosition = 0;
    public bool hasAccept;
    private Canvas canvas;
    private Button acceptButton;
    private Text speakText;

    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
        acceptButton = canvas.GetComponentsInChildren<Button>()[1];
        speakText = this.GetComponentsInChildren<Text>()[0];
        canvas.enabled = false;
        acceptButton.enabled = false;
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
        // Accept something
        StopInteract();
    }

    private void StopInteract()
    {
        textPosition = 0;
        PauseMenu.isPaused = false;
        canvas.enabled = false;
    }
}
