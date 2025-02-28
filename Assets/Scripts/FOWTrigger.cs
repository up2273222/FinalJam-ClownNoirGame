using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class FOWTrigger : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         foreach (Transform child in transform)
         {
            StartCoroutine(HideWall(child.gameObject));
         }
      }
   }

   private void OnTriggerExit(Collider other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         foreach (Transform child in transform)
         {
            StartCoroutine(ShowWall(child.gameObject));
         }
      }
   }


   IEnumerator HideWall(GameObject wall)
   {
      var mat = wall.GetComponent<Renderer>().material;
      Color col = mat.color;
      float timer = 0f;

      while (timer < 1f)
      {
         timer += Time.deltaTime;
         float alpha = Mathf.Lerp(1f, 0f, timer);
         mat.color = new Color(col.r,col.g,col.b,alpha);
         yield return null;
      }

   }
   
   IEnumerator ShowWall(GameObject wall)
   {
      var mat = wall.GetComponent<Renderer>().material;
      Color col = mat.color;
      float timer = 0f;

      while (timer < 1f)
      {
         timer += Time.deltaTime;
         float alpha = Mathf.Lerp(0f, 1f, timer);
         mat.color = new Color(col.r,col.g,col.b,alpha);
         yield return null;
      }

   }

  
   
   
   
   
   /*
   void ChangeAlpha(Material mat, float alphaVal)
   {
      
   }
   */
}
