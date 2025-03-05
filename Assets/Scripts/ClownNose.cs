using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClownNose : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip honk;
    public bool hasCollided;

    public float enemyAlertRadius;
    
   void OnCollisionEnter(Collision collision)
   {
       if (!hasCollided)
       {
           audioSource.PlayOneShot(honk);
           hasCollided = true;
           Vector3 collisionPoint = collision.contacts[0].point;
           AlertEnemy(collisionPoint);
           
       }
   }


   private void AlertEnemy(Vector3 collisionPoint)
   {
       Collider[] collidersInRadius = Physics.OverlapSphere(collisionPoint, enemyAlertRadius);
       List<GameObject> npcsInRadius = new List<GameObject>();
       GameObject closestNPC = null;
       float closestDistance = Mathf.Infinity;
       if (collidersInRadius != null)
       {
          foreach (Collider collider in collidersInRadius)
          {
              if (collider.gameObject.CompareTag("NPC"))
              {
                  npcsInRadius.Add(collider.gameObject);
              }
          } 
       }

       if (npcsInRadius.Count > 0)
       {
           foreach (GameObject npc in npcsInRadius)
           {
               float distance = Vector3.Distance(collisionPoint, npc.transform.position);
               if (distance < closestDistance)
               {
                   closestNPC = npc;
                   closestDistance = distance;
               }
           }
           NPCBehaviour npcBehaviour = closestNPC.GetComponent<NPCBehaviour>();
           npcBehaviour.MoveToNose(collisionPoint);
       }
       StartCoroutine(DestroyNose());
       
       
   }
   


   private IEnumerator DestroyNose()
   {
       yield return new WaitForSeconds(0.5f);
       Destroy(gameObject);
       
   }


}
