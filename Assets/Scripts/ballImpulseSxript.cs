using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ballImpulseSxript : MonoBehaviour
{
    public float radius = 3f;               // Radius of effect
    public float impulseStrength = 5f;      // How strong the force is
    public LayerMask ballLayer;             // Layer for balls only



    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitPoint;

            // Raycast to detect where the player clicked on the ball pit
            if (Physics.Raycast(ray, out hitPoint))
            {
                Vector3 clickPoint = hitPoint.point;

                // Find all nearby balls
                Collider[] hitBalls = Physics.OverlapSphere(clickPoint, radius, ballLayer);

                foreach (Collider col in hitBalls)
                {
                    Rigidbody rb = col.attachedRigidbody;
                    if (rb != null)
                    {
                        Vector3 direction = (rb.position - clickPoint).normalized;
                        rb.AddForce(direction * impulseStrength, ForceMode.Impulse);
                    }
                }
            }
        }
    }







}
