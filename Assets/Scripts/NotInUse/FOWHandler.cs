using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOWHandler : MonoBehaviour
{
    private float transparency;
    private float maxDistance = 10f;


    private void Start()
    {

    }

    private void Update()
    {
        print(transparency);
        
        ChangeAlpha(gameObject.GetComponent<Renderer>().material, CalculateTransparency());
       
    }

    float CalculateTransparency()
    {
        float playerDist = Vector3.Distance(gameObject.transform.position, PlayerController.Instance.transform.position);
        transparency = Mathf.Lerp(0,1,playerDist / maxDistance);
        if (transparency < 0.15f)
        {
            return transparency = 0;
        }
        if (transparency > 0.9f)
        {
            return transparency = 1;
        }
        else
        {
            return transparency;
        }
        
    }
    
    
    void ChangeAlpha(Material mat, float alphaVal)
    {
        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
        mat.SetColor("_Color", newColor);
    }
}
