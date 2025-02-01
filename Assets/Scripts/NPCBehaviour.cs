using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class NPCBehaviour : MonoBehaviour
{
    public TextAsset NpcDialogue;
    private int currentLineIndex;
    private bool isDialogueActive = false;
    private bool firstClickFix;
    private string[] lines;
    private string outputLine;



    void OnMouseDown()
    {
        //If clicked, initiate dialogue
        if (GameManager.Instance.DialogueVCam.Priority != 2)
        {
            GameManager.Instance.DialogueVCamFocus = gameObject.transform;
            GameManager.Instance.SwitchVCam();
            PlayDialogue();
        }


    }

    private void Update()
    {
        //Check for left click to advance dialogue
        if (isDialogueActive && Input.GetMouseButtonDown(0))
        {
            //Prevents printing 2 lines on the first click, probably a janky fix but it works lol
            if (!firstClickFix)
            {
                firstClickFix = true;
                return;
            }
            //Prints next line
            DisplayNextLine();
        }
    }


    public void PlayDialogue()
    {
        //Resets variables for start of dialogue
        string dialogue = NpcDialogue.ToString();
        lines = dialogue.Split("\n");
        currentLineIndex = 0;
        isDialogueActive = true;
        firstClickFix = false;
        if (GameManager.Instance.isDialoguePanelOpen)
        {
            DisplayNextLine();
        }
        else if (!GameManager.Instance.isDialoguePanelOpen)
        {
            GameManager.Instance.OpenCloseDialoguePanel();
            DisplayNextLine();
        }
        
        
    }

    void DisplayNextLine()
    {
        //If dialogue hasn't finished, print next line and increment index
        if (currentLineIndex < lines.Length)
        {
            outputLine = lines[currentLineIndex];
            GameManager.Instance.dialogueTextBox.text += "\n" + outputLine;
            GameManager.Instance.StartScroll();
            currentLineIndex++;
        }
        //If dialogue has finished, close dialogue
        else
        {
            isDialogueActive = false;
            Array.Clear(lines, 0, lines.Length);
            GameManager.Instance.dialogueTextBox.text += "\n";
            outputLine = "";
            GameManager.Instance.SwitchVCam();
            GameManager.Instance.OpenCloseDialoguePanel();
        }
    }

   
}
