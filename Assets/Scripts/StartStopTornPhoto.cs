using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStopTornPhoto : MonoBehaviour
{
 void OnMouseDown()
 {
  if (!GameManager.Instance.isInTornPhoto)
  {
   GameManager.Instance.isInTornPhoto = true;
   Destroy(this.gameObject);
   GameManager.Instance.StartTornPhotoMinigame();
  }
  else
  {
   GameManager.Instance.isInTornPhoto = false;
   GameManager.Instance.EndTornPhotoMinigame();
  }
  
 }
}
