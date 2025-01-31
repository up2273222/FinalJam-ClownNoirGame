using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public RectTransform dialoguePanelRect;
    private bool isDialoguePanelOpen;
    private Vector2 panelStartPosition;
    private Vector2 panelEndPosition;
    
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
            panelEndPosition =new Vector2(dialoguePanelRect.anchoredPosition.x - 160f, dialoguePanelRect.anchoredPosition.y);
        }
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
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
    }
}
