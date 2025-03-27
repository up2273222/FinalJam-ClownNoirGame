using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFastTravel : MonoBehaviour
{
 public GameObject place1Point; 
 
 
 
 public void LoadArea1()
 {
  UIManager.Instance.FastTravel();
  PlayerController.Instance.transform.position = place1Point.transform.position;
 }
    
    
    
}
