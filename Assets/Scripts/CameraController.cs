using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public Transform player;

  public Material cameraMat;

  private void OnRenderImage(RenderTexture source, RenderTexture destination)
  {
    if (cameraMat != null)
    {
      Graphics.Blit(source, destination, cameraMat);
    }
    else
    {
      Graphics.Blit(source, destination);
    }
  }

  private void LateUpdate()
  {
    transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
  }
}
