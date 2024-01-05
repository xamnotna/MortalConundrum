using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue; //visual cue to show player that they can interact with the NPC

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON; //ink JSON file

    private bool playerInRange; //check if player is in range of NPC

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    /*  private void Start()
     {
         playerInRange = false;
         visualCue.SetActive(false);
     } */

    private void Update()
    {
        if (playerInRange && !DialogueManager.dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                //DialogManager.GetInstance().EnterDialogueMode(inkJSON);
                FindObjectOfType<DialogueManager>().EnterDialogueMode(inkJSON);
                //Debug.Log(inkJSON.text);
            }
        }
        else
        {
            visualCue.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Player can talk to NPC");
            playerInRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Player can't talk to NPC");
            playerInRange = false;
        }

    }

    /*  private void OnTriggerEnter2D(Collider2D other)
     {
         if (other.gameObject.CompareTag("Player") && !other.isTrigger)
         {
             playerInRange = true;
             Debug.Log("Player can talk to NPC");
         }
     }

     private void OnTriggerExit2D(Collider2D other)
     {
         if (other.gameObject.CompareTag("Player") && !other.isTrigger)
         {
             playerInRange = false;
             Debug.Log("Player can't talk to NPC");
         }
     } */
}