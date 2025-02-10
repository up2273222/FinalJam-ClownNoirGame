using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownNose : MonoBehaviour
{
   void OnCollisionEnter(Collision collision)
   {
      
      
          //play squeak
          print("Collision");
          StartCoroutine(DestroyNose());
      
     
   }



   private IEnumerator DestroyNose()
   {
      yield return new WaitForSeconds(0.5f);
      Destroy(gameObject);
   }


}
