using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue; //visual cue to show player that they can interact with the NPC

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON; //ink JSON file

    private bool playerInRange; //is the player in range of the NPC?

    /* private void Start()
    {
        visualCue.SetActive(false);
    } */

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                TriggerDialogue();
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    // Update the current trigger when the player enters
    /*  private void OnTriggerEnter(Collider other)
     {
         if (other.tag == "Player" && !other.isTrigger)
         {
             playerInRange = true;
             Debug.Log("Player can talk to NPC");
             visualCue.SetActive(true);
         }
     }

     // Clear the current trigger when the player exits
     private void OnTriggerExit(Collider other)
     {
         if (other.tag == "Player" && !other.isTrigger)
         {
             playerInRange = false;
             visualCue.SetActive(false);
         }
     } */

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().EnterDialogueMode(inkJSON);
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
            Debug.Log("Player can talk to NPC");
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

    /*   private void OnTriggerEnter2D(Collider2D other)
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
/* if (playerInRange && !DialogueManager.dialogueIsPlaying)
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
} */