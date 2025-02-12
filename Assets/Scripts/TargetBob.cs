using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBob : MonoBehaviour
{
  private Transform target;
  private float rotSpeed = 50f;
  private float bobDistance = 0.01f;


  private void Start()
  {
    target = GetComponent<Transform>();
  }

  private void FixedUpdate()
  {
    target.rotation = Quaternion.Euler(target.rotation.eulerAngles.x, target.rotation.eulerAngles.y + Time.deltaTime * rotSpeed, target.rotation.eulerAngles.z);
    target.position = new Vector3(target.position.x,target.position.y + bobDistance * Mathf.Sin(Time.time * 2),target.position.z);
  }
}
