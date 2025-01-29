using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerController : MonoBehaviour

{
    //Components
    private Rigidbody _rb;
    private Material _material;
    //Walk animation variables
    private float animTimer;
    private float walkAnimAngle = 0.3f;
    //Movement variables
    [Header("Movement")]
    public float speed;
    private Vector2 _movement; 
    private bool isMoving()
    {
        return _movement.x != 0 || _movement.y != 0;
    }

 
    private void Start()
    {
        //Assign components to variables
        _rb = GetComponent<Rigidbody>();
        _material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        //Finds player movement direction
        GetMovementAxis();
        //Sets the rotation of player UVs in shader
        _material.SetFloat("_MinMaxRotation",GetSpriteRotation(walkAnimAngle));
        //Handles the animation looping
        //TODO: Fix the Mathf.PI/5 so that it returns to 0 rather than going all the way to the right first
        if (isMoving())
        {
            animTimer += Time.deltaTime;
            if (animTimer > Mathf.PI/5)
            {
                animTimer = 0;
            }
        }
        else if (!isMoving())
        {
            if (animTimer > Mathf.PI/5 || animTimer == 0)
            {
                animTimer = 0;
            }
            else if (animTimer < Mathf.PI/5)
            {
                animTimer += Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
       //Moves the player
        _rb.position += new Vector3(_movement.x * speed, 0, _movement.y * speed);
    }

    private float GetSpriteRotation(float angle)
    {
        //Returns the angle to set the UVs in shader to
        return Mathf.Sin((animTimer-(Mathf.PI)) * 10) * angle;

    }

    private void GetMovementAxis()
    {
        //Returns movement axis values of either -1,0,1 with no intermediate steps
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
    }

   
    
        

}
