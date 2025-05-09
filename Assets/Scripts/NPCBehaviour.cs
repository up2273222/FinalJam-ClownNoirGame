using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;



public class NPCBehaviour : MonoBehaviour
{
    
    private static readonly int MinMaxRotation = Shader.PropertyToID("_MinMaxRotation");
    public TextAsset NpcDialogue;
    private int currentLineIndex;
    private bool isDialogueActive = false;
    private bool firstClickFix;
    private bool isTyping;
    private string[] lines;
    private string whoIsTalking;
    public Sprite npcPortrait;
    
    private Coroutine typingCoroutine;
    
    private Collider npcCollider;

    private string unfinishedLine;

    private NavMeshAgent navAgent;
    
    private Material npcMaterial;

    private float _animTimer;
    private const float WalkAnimAngle = 0.3f;
    


    public bool FollowNose;

    private void Start()
    {
        npcMaterial = GetComponent<Renderer>().material;
        navAgent = GetComponent<NavMeshAgent>();
        npcCollider = GetComponent<Collider>();
        if (navAgent)
        {
            navAgent.updateUpAxis = false;
            navAgent.updateRotation = false;
            navAgent.baseOffset = npcCollider.bounds.size.y / transform.localScale.y * 0.5f;
        }
    }


    void OnMouseDown()
    {
        //If clicked, initiate dialogue
        if (CameraManager.Instance.DialogueVCam.Priority != 2)
        {
            CameraManager.Instance.DialogueVCamFocus = gameObject.transform;
            CameraManager.Instance.SwitchVCam();

            PlayDialogue();
        }
    }

    private void Update()
    {
        //Update animation
        npcMaterial.SetFloat(MinMaxRotation,GetSpriteRotation(WalkAnimAngle));
        //Check for left click to advance dialogue
        if (isDialogueActive && Input.GetMouseButtonDown(0))
        {
            //Prevents printing 2 lines on the first click
            if (!firstClickFix)
            {
                firstClickFix = true;
                return;
            }
            
            if(isTyping)
            {
                StopCoroutine(typingCoroutine);
                UIManager.Instance.dialogueTextBox.text += unfinishedLine;
                isTyping = false;
            }
            else
            {
                DisplayNextLine();
            }
        }
        if (navAgent)
        {
           if (navAgent.velocity.magnitude > 0.1f)
           {
               _animTimer += Time.deltaTime;
               if (_animTimer > Mathf.PI/5)
               {
                   _animTimer = 0;
               }
           }
           else if (navAgent.velocity.magnitude < 0.1f)
           {
               if (_animTimer > Mathf.PI/5 || _animTimer == 0)
               {
                   _animTimer = 0;
               }
               else if (_animTimer < Mathf.PI/5)
               {
                   _animTimer += Time.deltaTime;
               }
           } 
        }
    }
    
  


    public void PlayDialogue()
    {
        //Resets variables for start of dialogue
        string dialogue = NpcDialogue.ToString();
        lines = dialogue.Split("\n");
        currentLineIndex = 0;
        isDialogueActive = true;
        UIManager.Instance.isInDialogue = isDialogueActive;
        UIManager.Instance.OpenClosePortraitPanel();
        firstClickFix = false;
      
        if (UIManager.Instance.isDialoguePanelOpen)
        {
            DisplayNextLine();
        }
        else if (!UIManager.Instance.isDialoguePanelOpen)
        {
            UIManager.Instance.OpenCloseDialoguePanel();
            DisplayNextLine();
        }
    }

    void DisplayNextLine()
    {
        //If dialogue hasn't finished, print next line and increment index
        if (currentLineIndex < lines.Length)
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            typingCoroutine = StartCoroutine(TypeCharacter(lines[currentLineIndex]));
            
            //Changes portrait
            whoIsTalking = lines[currentLineIndex].Split(":")[0];
            SetPortrait(whoIsTalking);
            
            
            UIManager.Instance.StartScroll();
            currentLineIndex++;
        }
        
        //If dialogue has finished, close dialogue
        else
        {
            isDialogueActive = false;
            Array.Clear(lines, 0, lines.Length);
            
            CameraManager.Instance.SwitchVCam();
            
            UIManager.Instance.dialogueTextBox.text += "\n";
            UIManager.Instance.OpenCloseDialoguePanel();
            UIManager.Instance.isInDialogue = isDialogueActive;
            UIManager.Instance.OpenClosePortraitPanel();
        } 
    }

    IEnumerator TypeCharacter(string line)
    {
        isTyping = true;
        int charIndex = 0;

        foreach (char letter in line.ToCharArray())
        {
            yield return new WaitForSeconds(GameManager.Instance.GlobalTextWriteSpeed);
            UIManager.Instance.dialogueTextBox.text += letter;
            charIndex++;
            unfinishedLine = line.Substring(charIndex,line.Length - charIndex) + "\n";
        }
        UIManager.Instance.dialogueTextBox.text += "\n";
        isTyping = false;
    }


    public void MoveToNose(Vector3 targetLocation)
    {
        if (FollowNose && navAgent != null)
        {
            navAgent.SetDestination(targetLocation);
        }
        
    }
    private float GetSpriteRotation(float angle)
    {
        //Returns the angle to set the UVs in shader to
        return Mathf.Sin((_animTimer-(Mathf.PI)) * 10) * angle;
    }
    public void SetPortrait(string speakerName)
    {
        Sprite portrait = Resources.Load<Sprite>($"Portraits/{speakerName}");
        if (portrait)
        {
            UIManager.Instance.SetPortrait(portrait);
        }
        else
        {
           portrait = Resources.Load<Sprite>($"Portraits/MISSING");
           UIManager.Instance.SetPortrait(portrait);
        }
    }


}
