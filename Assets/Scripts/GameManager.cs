using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Set as instance
    public static GameManager Instance;
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
        }
    }

  




    

 
    
   
}
