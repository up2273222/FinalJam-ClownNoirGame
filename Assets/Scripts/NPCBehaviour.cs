using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class NPCBehaviour : MonoBehaviour
{
    public TextAsset NpcDialogue;
    private bool hasFinishedSpeaking;


    void OnMouseDown()
    {
        //If clicked, initiate or close dialogue
        if (GameManager.Instance.DialogueVCam.Priority != 2)
        {
            GameManager.Instance.DialogueVCamFocus = gameObject.transform;
            GameManager.Instance.SwitchVCam();
            PlayDialogue();
        }
        else if (GameManager.Instance.DialogueVCam.Priority != 2 & hasFinishedSpeaking)
        {
            GameManager.Instance.SwitchVCam();
        }

    }


    public void PlayDialogue()
    {
        string dialogue = NpcDialogue.ToString();
        string[] lines = dialogue.Split('^');
        for (int i = 0; i < lines.Length-1; i++)
        {
            string output = lines[i];
            Debug.Log(output);

        }

        
    }
}
