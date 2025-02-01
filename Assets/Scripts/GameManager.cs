using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Set as instance
    public static GameManager Instance;
    
    //UI variables
    public GameObject UICanvas;
    public RectTransform dialoguePanelRect;
    public bool isDialoguePanelOpen;
    public TextMeshProUGUI dialogueTextBox;
    private Vector2 panelStartPosition;
    private Vector2 panelEndPosition;

    //Camera variables
    public CinemachineVirtualCamera GameplayVCam;
    public CinemachineVirtualCamera DialogueVCam;
    public Transform DialogueVCamFocus;
    
    private void Awake()
    {
        if (Instance != null)
        {
            //Ensures no duplicates
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //Set default values:
            //Dialogue start and end pos
            //Sets UI to be visible
            //Sets gameplay cam to active cam
            //Clear text box
            panelStartPosition = new Vector2(dialoguePanelRect.anchoredPosition.x, dialoguePanelRect.anchoredPosition.y);
            panelEndPosition =new Vector2(dialoguePanelRect.anchoredPosition.x - 300f, dialoguePanelRect.anchoredPosition.y);
            UICanvas.SetActive(true);
            GameplayVCam.Priority = 2;
            DialogueVCam.Priority = 1;
            dialogueTextBox.text = "";
        }
    }

    private void Update()
    {

        //HERE FOR DEBUG
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            OpenCloseDialoguePanel();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchVCam();
        }
    }


    public void OpenCloseDialoguePanel()
    {
        //If dialogue panel is open, close it. If closed, open it
        //Uses DOTween for smooth movement
        if (isDialoguePanelOpen)
        {
            dialoguePanelRect.DOAnchorPos(panelStartPosition, 1f).SetEase(Ease.InOutQuad);
            isDialoguePanelOpen = false;
        }
        else
        {
            dialoguePanelRect.DOAnchorPos(panelEndPosition, 1f).SetEase(Ease.InOutQuad);
            isDialoguePanelOpen = true;
        }
    }

    public void SwitchVCam()
    {
        //Used when talking to NPCs
        //If gameplay cam is active, set to dialogue cam and point to NPC. If dialogue cam active, set to gameplay cam
        if (GameplayVCam.Priority == 1)
        {
            GameplayVCam.Priority = 2;
            DialogueVCam.Priority = 1;
        }
        else if (GameplayVCam.Priority == 2)
        {
            DialogueVCam.Follow = DialogueVCamFocus;
            GameplayVCam.Priority = 1;
            DialogueVCam.Priority = 2;
        }
            
    }
}
