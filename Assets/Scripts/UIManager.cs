using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    //UI variables
    public GameObject UICanvas;
    public GameObject PortaitPanel;
    public Sprite PlayerPortrait;
    public RectTransform playerPortraitRect;
    public RectTransform dialoguePanelRect;
    [HideInInspector]public bool isDialoguePanelOpen;
    [HideInInspector]public bool isInDialogue;
    public TextMeshProUGUI dialogueTextBox;
    public ScrollRect scrollRect;
    
    private Vector2 dialoguePanelStartPosition;
    private Vector2 dialoguePanelEndPosition;
    
    private Vector2 portaitPanelStartPosition;
    private Vector2 portaitPanelEndPosition;
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
            
            dialoguePanelStartPosition = dialoguePanelRect.anchoredPosition;
            dialoguePanelEndPosition =new Vector2(dialoguePanelRect.anchoredPosition.x - 300f, dialoguePanelRect.anchoredPosition.y);
            
            portaitPanelStartPosition = playerPortraitRect.anchoredPosition;
            portaitPanelEndPosition = new Vector2(playerPortraitRect.anchoredPosition.x, playerPortraitRect.anchoredPosition.y + 200f);
            
            
            UICanvas.SetActive(true);
            dialogueTextBox.text = "";
        }
    }
    
    public void OpenCloseDialoguePanel()
    {
        //If dialogue panel is open, close it. If closed, open it
        //Uses DOTween for smooth movement
        if (isDialoguePanelOpen)
        {   
            dialoguePanelRect.DOAnchorPos(dialoguePanelStartPosition, 1f).SetEase(Ease.InOutQuad);
            isDialoguePanelOpen = false;
        }
        else if (!isDialoguePanelOpen)
        {
            dialoguePanelRect.DOAnchorPos(dialoguePanelEndPosition, 1f).SetEase(Ease.InOutQuad);
            isDialoguePanelOpen = true;
        }
    }

    public void OpenClosePortraitPanel()
    {
        if (!isInDialogue)
        {
            playerPortraitRect.DOAnchorPos(portaitPanelStartPosition,1f).SetEase(Ease.InOutQuad);
        }
        else if (isInDialogue)
        {
            playerPortraitRect.DOAnchorPos(portaitPanelEndPosition,1f).SetEase(Ease.InOutQuad);
        }
    }
    
    private void Update()
    {

        //Open/Close dialogue box
        if(Input.GetKeyDown(KeyCode.Tab) && !isInDialogue)
        {
            OpenCloseDialoguePanel();
        }
        
    }
    
    public void StartScroll()
    {
        //Allows coroutine to be started from another script
        StartCoroutine(ScrollToBottom());
    }
    
    public IEnumerator ScrollToBottom()
    {
        //Scrolls dialogue text box to the bottom when called
        yield return new WaitForEndOfFrame();
        scrollRect.verticalNormalizedPosition = 0;
    }

    public void SetPortrait(Sprite image)
    {
        PortaitPanel.GetComponent<Image>().sprite = image;
    }
}
