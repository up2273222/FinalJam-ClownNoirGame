using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerController : MonoBehaviour

{
    private static readonly int MinMaxRotation = Shader.PropertyToID("_MinMaxRotation");

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
    public Transform throwTarget;
    public int arcMaximumHeight = 10;
    
    
    
    
    private bool IsMoving()
    {
        return _movement.x != 0 || _movement.y != 0;
    }

 
    private void Start()
    {
        //Assign components to variables
        _rb = GetComponent<Rigidbody>();
        _material = GetComponent<Renderer>().material;
        canMove = true;
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
            throwNose();
            
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

    void throwNose()
    {
        GameObject Nose = Instantiate(clownNose, transform.position, Quaternion.identity);
        Rigidbody Nose_rb = Nose.GetComponent<Rigidbody>();

        Ray targetRay;
        RaycastHit targetHit;
        
        targetRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(targetRay, out targetHit))
        {
            throwTarget.position = new Vector3(targetHit.point.x, targetHit.point.y + 0.2f, targetHit.point.z);
            Nose_rb.velocity = CalculateThrowVelocity(throwTarget, Nose_rb);
        }
        
       

        



    }
    
    Vector3 CalculateThrowVelocity(Transform target, Rigidbody Nose_rb)
    {
        float displacementY = target.position.y - Nose_rb.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - Nose_rb.position.x,0,target.position.z - Nose_rb.position.z);
        
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * Physics.gravity.y * arcMaximumHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2*arcMaximumHeight/Physics.gravity.y) + Mathf.Sqrt(2*(displacementY - arcMaximumHeight)/Physics.gravity.y));

        return velocityXZ + velocityY;
    }
    
    

   
    
        

}
