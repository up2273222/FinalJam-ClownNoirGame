using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    //Camera variables
    public CinemachineVirtualCamera GameplayVCam;
    public CinemachineVirtualCamera DialogueVCam;
    public Transform DialogueVCamFocus;
    //Shader variables
    public Material cameraMat;
    private bool UseVignette = true;
    private bool UseFilmGrain = true;
    private float VignetteRadius;
    private float VignetteFeathering;
    private float Brightness;

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
            GameplayVCam.Priority = 2;
            DialogueVCam.Priority = 1;
            
            
        }
    }

    private void LateUpdate()
    {
        GameplayVCam.transform.position = new Vector3(PlayerController.Instance.transform.position.x, GameplayVCam.transform.position.y,GameplayVCam.transform.position.z);
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

    public void EnableDisableVignette()
    {
        if (UseVignette)
        {
            cameraMat.SetFloat("_UseVignette", 0);
            UseVignette = false;
        }
        else
        {
            cameraMat.SetFloat("_UseVignette", 1);
            UseVignette = true;
        }
        
    }

    public void EnableDisableFilmGrain()
    {
        if (UseFilmGrain)
        {
            cameraMat.SetFloat("_UseFilmGrain", 0);
            UseFilmGrain = false;
        }
        else
        {
            cameraMat.SetFloat("_UseFilmGrain", 1);
            UseFilmGrain = true;
        }
    }

    public void SetFeathering(float feathering)
    {
        cameraMat.SetFloat("_VignetteFeather", feathering);
    }

    public void SetVignetteRadius(float vignetteRadius)
    {
        cameraMat.SetFloat("_VignetteRadius", vignetteRadius);
    
    }

    public void SetBrightness(float brightness)
    {
        cameraMat.SetFloat("_Brightness", brightness);
    }

    private void SetShaderDefaults()
    {
        cameraMat.SetFloat("_UseFilmGrain", 1);
        cameraMat.SetFloat("_UseVignette", 1);
        cameraMat.SetFloat("_VignetteRadius", 0.6f);
        cameraMat.SetFloat("_VignetteFeather", 0.8f);
    }
    


}
