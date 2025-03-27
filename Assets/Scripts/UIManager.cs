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

    private bool isMapOpen;
    public GameObject MapPanel;
    private Vector3 targetMapScale;
    
    public TextMeshProUGUI dialogueTextBox;
    public ScrollRect scrollRect;
    
    private Vector2 dialoguePanelStartPosition;
    private Vector2 dialoguePanelEndPosition;
    
    private Vector2 portaitPanelStartPosition;
    private Vector2 portaitPanelEndPosition;

    public RectTransform InventoryRectPos;
    private Vector2 TargetInvPos;
    private Vector2 InvStartPos;
    private bool IsInvOpen;
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
            
            targetMapScale = MapPanel.transform.localScale;
            MapPanel.transform.localScale = Vector3.zero;

            InvStartPos = InventoryRectPos.anchoredPosition;
            TargetInvPos = new Vector2(InventoryRectPos.anchoredPosition.x, InventoryRectPos.anchoredPosition.y + 110f);
            IsInvOpen = false;
            
            
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
            dialoguePanelRect.DOAnchorPos(dialoguePanelStartPosition, 0.5f).SetEase(Ease.InOutQuad);
            isDialoguePanelOpen = false;
        }
        else if (!isDialoguePanelOpen)
        {
            dialoguePanelRect.DOAnchorPos(dialoguePanelEndPosition, 0.5f).SetEase(Ease.InOutQuad);
            isDialoguePanelOpen = true;
        }
    }

    public void OpenCloseMap()
    {
        //InOutBack
        if (isMapOpen)
        {
            //Close map
            MapPanel.transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InBack);
            isMapOpen = !isMapOpen;
        }
        else if (!isMapOpen)
        {
            //Open map
            MapPanel.transform.DOScale(targetMapScale, 1f).SetEase(Ease.OutBack);
            isMapOpen = !isMapOpen;
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

    public void OpenCloseInventory()
    {
        if(IsInvOpen)
        {
            InventoryRectPos.DOAnchorPos(InvStartPos, 0.5f).SetEase(Ease.InOutQuad);
            IsInvOpen = false;
        }
        else if(!IsInvOpen)
        {
            InventoryRectPos.DOAnchorPos(TargetInvPos, 0.5f).SetEase(Ease.InOutQuad);
            IsInvOpen=true;
        }
    }
    
    private void Update()
    {

        //Open/Close dialogue box
        if(Input.GetKeyDown(KeyCode.Tab) && !isInDialogue)
        {
            OpenCloseDialoguePanel();
        }
        else if (Input.GetKeyDown(KeyCode.M) && !isInDialogue)
        {
            OpenCloseMap();
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            OpenCloseInventory();
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

    public void FastTravel()
    {
        CameraManager.Instance.isFading = true;
        DOTween.To(() => CameraManager.Instance.Brightness, x => CameraManager.Instance.Brightness = x, 0f,1f);
        OpenCloseMap();
        StartCoroutine(FadeToBlackDelay(1.5f));

    }

    IEnumerator FadeToBlackDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        DOTween.To(() => CameraManager.Instance.Brightness, x => CameraManager.Instance.Brightness = x, 1f,1f);
        yield return new WaitForSeconds(waitTime);
        CameraManager.Instance.isFading = false;
    }
}
