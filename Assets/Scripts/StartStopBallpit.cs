using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStopBallpit : MonoBehaviour
{
  
  
  
  
  void OnMouseDown()
  {
    if (!GameManager.Instance.isInBallpit)
    {
      GameManager.Instance.isInBallpit = true;
      startMinigame();
    }
    else
    {
      GameManager.Instance.isInBallpit = false;
      stopMinigame();
    }
  }

  private void startMinigame()
  {
    GameManager.Instance.StartBallpitMinigame();
  }

  private void stopMinigame()
  {
    GameManager.Instance.EndBallpitMinigame();
  }
  
  
}
