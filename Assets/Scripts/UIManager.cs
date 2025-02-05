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
    public Sprite PlayerPortrait;
    public GameObject PortaitPanel;
    public RectTransform dialoguePanelRect;
    public bool isDialoguePanelOpen;
    public TextMeshProUGUI dialogueTextBox;
    private Vector2 panelStartPosition;
    private Vector2 panelEndPosition;
    public ScrollRect scrollRect;

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
            panelStartPosition = new Vector2(dialoguePanelRect.anchoredPosition.x, dialoguePanelRect.anchoredPosition.y);
            panelEndPosition =new Vector2(dialoguePanelRect.anchoredPosition.x - 300f, dialoguePanelRect.anchoredPosition.y);
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
            dialoguePanelRect.DOAnchorPos(panelStartPosition, 1f).SetEase(Ease.InOutQuad);
            isDialoguePanelOpen = false;
        }
        else
        {
            dialoguePanelRect.DOAnchorPos(panelEndPosition, 1f).SetEase(Ease.InOutQuad);
            isDialoguePanelOpen = true;
        }
    }
    
    private void Update()
    {

        //Open/Close dialogue box
        if(Input.GetKeyDown(KeyCode.Tab))
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
