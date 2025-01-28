using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector2 _movement; 
    
    [Header("Movement")]
    public float speed;
    

    
    


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        print(_rb.velocity);
    }

    private void FixedUpdate()
    {
        _rb.position += new Vector3(_movement.x * speed, 0, _movement.y * speed);
    }
}
