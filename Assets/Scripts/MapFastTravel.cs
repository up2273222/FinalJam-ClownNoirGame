using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFastTravel : MonoBehaviour
{
 public Transform Office;
 public Transform PlayPlace;
 public Transform Bar;
 
 
 
 public void LoadOffice()
 {
  UIManager.Instance.FastTravel();
  StartCoroutine(LoadDelay(Office));
 }

 public void LoadPlayPlace()
 {
  UIManager.Instance.FastTravel();
  StartCoroutine(LoadDelay(PlayPlace));
 }
 
 public void LoadBar()
 {
  UIManager.Instance.FastTravel();
  StartCoroutine(LoadDelay(Bar));
 }
 IEnumerator LoadDelay(Transform tpPosition)
 {
  yield return new WaitForSeconds(1f);
  PlayerController.Instance.transform.position = tpPosition.position;
 }
    
    
    
}
