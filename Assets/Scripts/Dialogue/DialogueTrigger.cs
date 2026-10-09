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

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        DialogueManager dialogueManager = DialogueManager.GetInstance();
        bool isDialoguePlaying = dialogueManager != null && dialogueManager.dialogueIsPlaying;

        if (playerInRange && !isDialoguePlaying)
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

    public void TriggerDialogue()
    {
        DialogueManager dialogueManager = DialogueManager.GetInstance();
        if (dialogueManager == null)
        {
            dialogueManager = FindAnyObjectByType<DialogueManager>();
        }

        if (dialogueManager == null)
        {
            Debug.LogError("DialogueTrigger could not find a DialogueManager instance.");
            return;
        }

        dialogueManager.EnterDialogueMode(inkJSON);
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
}