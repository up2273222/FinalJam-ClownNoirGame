using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    private Collider areaTrigger;
    public GameObject interactPopup;
    public GameObject thoughtBubblePrefab;
    private bool isInRange;
    private GameObject ThoughtBubble;
    public TextAsset popupText;


    private void Start()
    {
        interactPopup.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactPopup.SetActive(true);
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactPopup.SetActive(false);
            isInRange = false;
        }
    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInRange && !ThoughtBubble)
        {
            ThoughtBubble = Instantiate(thoughtBubblePrefab);
            InteractTextDisplay objectScript = ThoughtBubble.GetComponent<InteractTextDisplay>();
            objectScript.Initialise(popupText);

        }

    }
}
