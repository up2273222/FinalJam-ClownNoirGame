using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public Transform player;

  public Material cameraMat;
  
  
  public bool UseFilmGrain;
  public bool UseVignette;
  [Range(0, 1)] [SerializeField] public float VignetteRadius;
  [Range(0, 1)] [SerializeField] public float VignetteFeathering;

  
  
  
  

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

  private void Update()
  {
    
    
    
    
  }
}
