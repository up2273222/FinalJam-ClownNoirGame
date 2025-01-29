using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerController : MonoBehaviour

{
    private Rigidbody _rb;
    private Vector2 _movement; 
    private Material _material;

    private float animTimer;
    private float tempTimer;

  

    
    
    [Header("Movement")]
    public float speed;
    public float walkAnimAngle;
    
    
    private bool isMoving()
    {
        return _movement.x != 0 || _movement.y != 0;
    }

 
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        _material.SetFloat("_MinMaxRotation",GetSpriteRotation(walkAnimAngle));
        GetMovementAxis();
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
       
        _rb.position += new Vector3(_movement.x * speed, 0, _movement.y * speed);
    }

    private float GetSpriteRotation(float angle)
    {
        return Mathf.Sin((animTimer-(Mathf.PI)) * 10) * angle;

    }

    private void GetMovementAxis()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
    }

   
    
        

}
