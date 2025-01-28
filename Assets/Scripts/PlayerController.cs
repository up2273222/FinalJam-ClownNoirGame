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
    
    
    [Header("Movement")]
    public float speed;
    public float walkAnimAngle;

 
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        GetMovementAxis();
        if (isMoving())
        {
            _material.SetFloat("_MinMaxRotation",GetSpriteRotation(walkAnimAngle));
        }
        
       
    
    }

    private void FixedUpdate()
    {
       
        _rb.position += new Vector3(_movement.x * speed, 0, _movement.y * speed);
    }

    private float GetSpriteRotation(float angle)
    {
        return Mathf.Sin(Time.time * 10) * angle;
    }

    private void GetMovementAxis()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
    }

    private bool isMoving()
    {
        return _movement.x != 0 || _movement.y != 0;
    }
    
        

}
