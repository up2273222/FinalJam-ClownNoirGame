using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;
using Cinemachine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Set as instance
    public static GameManager Instance;
    //TextVariables
    public float GlobalTextWriteSpeed;
    //Game state
    public bool isInBallpit;
    public bool isInTornPhoto;
    //Ballpit
    public GameObject ballPrefab;
    public GameObject bodyPrefab;
    public int ballCount;
    public BoxCollider ballArea;
    public Transform ballPitCenter;
    private List<GameObject> _ballList = new List<GameObject>();
    private GameObject _bodyRef;


    
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

    public void StartBallpitMinigame()
    {
        CameraManager.Instance.BallpitVCam.Priority = 100;
        PopulateBallPit();
    }

    public void EndBallpitMinigame()
    {
        CameraManager.Instance.BallpitVCam.Priority = -100;
        ClearBallPit();
    }
    
    private void PopulateBallPit()
    {
        Vector3 pitSize = ballArea.bounds.size;
        for (int i = 0; i < ballCount; i++)
        {
            
            Vector3 randPos = new Vector3(
                Random.Range(-pitSize.x / 2, pitSize.x / 2),
                Random.Range(0, pitSize.y / 2),
                Random.Range(-pitSize.z / 2, pitSize.z / 2));

            Vector3 spawnPos = randPos + ballPitCenter.position;
            GameObject ball =  Instantiate(ballPrefab, spawnPos, Quaternion.identity);
            _ballList.Add(ball);
      
        }
        
        Vector3 randbodyPos = new Vector3(
            Random.Range(-pitSize.x / 2, pitSize.x / 2),
            -pitSize.y / 2,
            Random.Range(-pitSize.z / 2, pitSize.z / 2));
        
        Vector3 bodyspawnPos = randbodyPos + ballPitCenter.position; 
        _bodyRef = Instantiate(bodyPrefab, bodyspawnPos, Quaternion.Euler(90, 0, 0));
    }

    private void ClearBallPit()
    {
        for (int i = 0; i < _ballList.Count; i++)
        {
            Destroy(_ballList[i]);
        }
        Destroy(_bodyRef);

        
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
