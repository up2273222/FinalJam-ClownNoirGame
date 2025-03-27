using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFastTravel : MonoBehaviour
{
 public Transform place1Point; 
 
 
 
 public void LoadArea1()
 {
  UIManager.Instance.FastTravel();
  StartCoroutine(LoadDelay(place1Point));
 }

 IEnumerator LoadDelay(Transform tpPosition)
 {
  yield return new WaitForSeconds(1f);
  PlayerController.Instance.transform.position = tpPosition.position;
 }
    
    
    
}
