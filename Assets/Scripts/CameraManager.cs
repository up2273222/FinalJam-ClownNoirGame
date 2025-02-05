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
