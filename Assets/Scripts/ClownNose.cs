using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownNose : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip honk;
    public bool hasCollided;
    
   void OnCollisionEnter(Collision collision)
   {
       if (!hasCollided)
       {
           audioSource.PlayOneShot(honk);
           hasCollided = true;
           print("Collision");
           StartCoroutine(DestroyNose());
           Vector3 collisionPoint = collision.contacts[0].point;
       }
          


   }



   private IEnumerator DestroyNose()
   {
      yield return new WaitForSeconds(0.5f);
      Destroy(gameObject);
   }


}
