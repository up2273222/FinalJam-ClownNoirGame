using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Serialization;



public class PlayerController : MonoBehaviour

{
    private static readonly int MinMaxRotation = Shader.PropertyToID("_MinMaxRotation");
    public static PlayerController Instance;
    
    //Components
    private Rigidbody _rb;
    private Material _material;
    //Walk animation variables
    private float _animTimer;
    private const float WalkAnimAngle = 0.3f;

    //Movement variables
    [Header("Movement")]
    public float speed;
    private Vector2 _movement;
    private bool canMove;
    
    //Clown nose throw variables
    public GameObject clownNose;
    private Rigidbody Nose_rb;
    public Transform throwTarget;
    public int arcMaximumHeight = 10;
    public bool throwInitiated;
    public bool throwCompleted;
    public LineRenderer trajectoryRenderer;

    
    
    
    private bool IsMoving()
    {
        return _movement.x != 0 || _movement.y != 0;
    }

 
    private void Start()
    {
        //Assign components to variables
        _rb = GetComponent<Rigidbody>();
        _material = GetComponent<Renderer>().material;
        trajectoryRenderer = GetComponent<LineRenderer>();
        Instance = this;
        canMove = true;
        throwTarget.gameObject.SetActive(false);
    }

    private void Update()
    {
        //Finds player movement direction
        GetMovementAxis();
        //Sets the rotation of player UVs in shader
        _material.SetFloat(MinMaxRotation,GetSpriteRotation(WalkAnimAngle));
        //Handles the animation looping
        //TODO: Fix the Mathf.PI/5 so that it returns to 0 rather than going all the way to the right first
        if (IsMoving())
        {
            _animTimer += Time.deltaTime;
            if (_animTimer > Mathf.PI/5)
            {
                _animTimer = 0;
            }
        }
        else if (!IsMoving())
        {
            if (_animTimer > Mathf.PI/5 || _animTimer == 0)
            {
                _animTimer = 0;
            }
            else if (_animTimer < Mathf.PI/5)
            {
                _animTimer += Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!throwInitiated)
            {
                if (Nose_rb == null)
                {
                    throwInitiated = true;
                    throwCompleted = false;
                    GameObject nose = Instantiate(clownNose,transform.position,Quaternion.identity);
                    Nose_rb = nose.GetComponent<Rigidbody>();
                    
                }
            }
            else if (throwInitiated)
            {
                throwCompleted = true;
            }
        }
        
        
        
        
        if (throwInitiated && !throwCompleted)
        {
            throwTarget.gameObject.SetActive(true);
            Nose_rb.useGravity = false;
            Ray targetRay;
            RaycastHit targetHit;
            targetRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(targetRay, out targetHit))
            {
                throwTarget.position = new Vector3(targetHit.point.x, targetHit.point.y + 0.2f, targetHit.point.z);
            }
            Nose_rb.position = transform.position;
            DrawNoseTrajectory(Nose_rb.position,CalculateThrowVelocity(throwTarget,Nose_rb));
            
        }
        else if (throwInitiated && throwCompleted)
        {
            Nose_rb.useGravity = true;
            throwNose(Nose_rb);
            throwTarget.gameObject.SetActive(false);
        } 
    }

    private void FixedUpdate()
    {
       //Moves the player
       if (canMove)
       {
           _rb.position += new Vector3(_movement.x * speed, 0, _movement.y * speed);
       }
       
    }

    private float GetSpriteRotation(float angle)
    {
        //Returns the angle to set the UVs in shader to
        return Mathf.Sin((_animTimer-(Mathf.PI)) * 10) * angle;

    }

    private void GetMovementAxis()
    {
        //Returns movement axis values of either -1,0,1 with no intermediate steps
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
    }
    
    
    //Handle clown nose throwing
    void throwNose(Rigidbody Nose_rb)
    {
        Nose_rb.velocity = CalculateThrowVelocity(throwTarget, Nose_rb);
        trajectoryRenderer.positionCount = 0;
        throwInitiated = false;
        throwCompleted = false;
        
    }
    
    Vector3 CalculateThrowVelocity(Transform target, Rigidbody Nose_rb)
    {
        float displacementY = target.position.y - Nose_rb.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - Nose_rb.position.x,0,target.position.z - Nose_rb.position.z);
        
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * Physics.gravity.y * arcMaximumHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2*arcMaximumHeight/Physics.gravity.y) + Mathf.Sqrt(2*(displacementY - arcMaximumHeight)/Physics.gravity.y));

        return velocityXZ + velocityY;
    }

    void DrawNoseTrajectory(Vector3 startPosition, Vector3 initialVelocity)
    {
        trajectoryRenderer.positionCount = 100;
        float travelTime = 1.5f;
        Vector3 currentPosition = startPosition;
        Vector3 currentVelocity = initialVelocity;
        float timeStep = travelTime/trajectoryRenderer.positionCount;

        for (int i = 0; i < trajectoryRenderer.positionCount; i++)
        {
            trajectoryRenderer.SetPosition(i, currentPosition);
            currentVelocity += new Vector3(0, Physics.gravity.y * timeStep, 0);
            currentPosition += currentVelocity * timeStep;

        }

    }

   

   
    
   
    
    

   
    
        

}
