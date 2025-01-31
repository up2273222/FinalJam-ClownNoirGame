using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject dialoguePanel;
    private Vector3 _dialoguePanelStartPosition;
    private Vector3 _dialoguePanelEndPosition;
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _dialoguePanelStartPosition = dialoguePanel.transform.position;
            _dialoguePanelEndPosition = new Vector3(dialoguePanel.transform.position.x - 160f, dialoguePanel.transform.position.y, dialoguePanel.transform.position.z);
        }
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (dialoguePanel.transform.position == _dialoguePanelEndPosition)
            {
                dialoguePanel.transform.position = _dialoguePanelStartPosition;
            }
            else
            {
                dialoguePanel.transform.position = _dialoguePanelEndPosition;
            }
        }
    }
}
