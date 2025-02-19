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

    public float GlobalTextWriteSpeed;
    
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
            SetDefaultValues();
            
        }
    }

    public void SetTypeSpeed(float speed)
    {
        GlobalTextWriteSpeed = 0.5f - (speed - 1) * (0.05f);
    }


    private void SetDefaultValues()
    {
        GlobalTextWriteSpeed = 0.1f;
    }


    private void Update()
    {
        //DEBUG - remove before building
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CameraManager.Instance.EnableDisableVignette();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CameraManager.Instance.EnableDisableFilmGrain();
        }
       
    }
}
