using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractTextDisplay : MonoBehaviour
{
    public TextMeshPro text;
    public TextAsset displayTextAsset;
    private int currentLineIndex = 0;
    
    private string[] lines;
    

    public void Initialise(TextAsset TextAsset)
    {
        displayTextAsset = TextAsset;
        WriteText(displayTextAsset);
    }


    public void WriteText(TextAsset displayText)
    {
       string displayString = displayText.ToString();
       lines = displayString.Split('\n');
       text.text = null;
       DisplayNextLine();
    }


    private void DisplayNextLine()
    {
        if (currentLineIndex < lines.Length)
        {
            StartCoroutine(WriteChar(lines[currentLineIndex]));
        }
        else
        {
            Destroy(gameObject);
        }
    }


    IEnumerator WriteChar(string line)
    {
        int charIndex = 0;
        foreach (char letter in line)
        {
            yield return new WaitForSeconds(GameManager.Instance.GlobalTextWriteSpeed);
            text.text += letter;
            charIndex++;
        }
        yield return new WaitForSeconds(1);
        text.text = null;
        currentLineIndex++;
        DisplayNextLine();
    }

    private void Update()
    {
        transform.position = new Vector3(PlayerController.Instance.transform.position.x, PlayerController.Instance.transform.position.y + 0.5f, PlayerController.Instance.transform.position.z);
    }
}
