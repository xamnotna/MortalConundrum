using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel; //dialogue UI
    [SerializeField] private TextMeshProUGUI dialogueText; //text to display dialogue
    [SerializeField] private TextMeshProUGUI displayNameText; //text to display name of speaker
    [SerializeField] private Animator portraitAnimator; //animator to control portrait

    private Animator layoutAnimator; //animator to control layout

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices; //choices UI

    private TextMeshProUGUI[] choicesTexts; //text to display choices

    private Story currentStory; //ink story
    private bool isExitingDialogue;
    private bool waitingForAdvanceKeyRelease;

    public bool dialogueIsPlaying { get; private set; }

    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker"; //tag to identify speaker in ink JSON file
    private const string PORTRAIT_TAG = "portrait"; //tag to identify portraits in ink JSON file
    private const string LAYOUT_TAG = "layout"; //tag to identify layout in ink JSON file

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {

        dialoguePanel.SetActive(false);
        dialogueIsPlaying = false;
        isExitingDialogue = false;
        waitingForAdvanceKeyRelease = false;

        layoutAnimator = dialoguePanel.GetComponent<Animator>();

        // get all the choice text objects and store them in an array
        //hide all the choices if dialogue is not playing

        foreach (GameObject choice in choices)
        {
            choice.SetActive(false);
        }
        choicesTexts = new TextMeshProUGUI[choices.Length];

        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesTexts[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        // return if dialogue is not playing
        if (!dialogueIsPlaying || currentStory == null)
        {
            return;
        }

        // Ignore advance input until the key used to start dialogue is released.
        // This avoids consuming the start press as an immediate "continue".
        if (waitingForAdvanceKeyRelease)
        {
            if (!Input.GetKey(KeyCode.E))
            {
                waitingForAdvanceKeyRelease = false;
            }
            return;
        }

        // prevent choice deselection while choice prompts are active
        if (currentStory.currentChoices.Count > 0 && EventSystem.current != null && EventSystem.current.currentSelectedGameObject == null)
        {
            StartCoroutine(selectFirstChoice());
        }


        if (currentStory.currentChoices.Count == 0 && Input.GetKeyDown(KeyCode.E))
        {
            ContinueStory();
        }
        else if (currentStory.currentChoices.Count > 0 && Input.GetKeyDown(KeyCode.E))
        {
            int selectableChoiceCount = Mathf.Min(currentStory.currentChoices.Count, choices.Length);
            GameObject selectedChoice = EventSystem.current != null ? EventSystem.current.currentSelectedGameObject : null;

            if (selectedChoice == null)
            {
                for (int i = 0; i < selectableChoiceCount; i++)
                {
                    if (choices[i] != null && choices[i].activeSelf)
                    {
                        currentStory.ChooseChoiceIndex(i);
                        ContinueStory();
                        return;
                    }
                }
            }

            for (int i = 0; i < selectableChoiceCount; i++)
            {
                if (choices[i] != null && choices[i].activeSelf && selectedChoice == choices[i])
                {
                    currentStory.ChooseChoiceIndex(i);
                    ContinueStory();
                    break;
                }

            }
        }


    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        waitingForAdvanceKeyRelease = Input.GetKey(KeyCode.E);
        dialoguePanel.SetActive(true);

        displayNameText.text = "???";
        portraitAnimator.Play("default");
        layoutAnimator.Play("right");


        ContinueStory();
    }

    public IEnumerator ExitDialogueMode()
    {
        isExitingDialogue = true;
        yield return new WaitForSeconds(0.2f);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        currentStory = null;
        waitingForAdvanceKeyRelease = false;
        isExitingDialogue = false;

        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

        foreach (GameObject choice in choices)
        {
            choice.SetActive(false);
        }
    }

    private void ContinueStory()
    {
        if (currentStory == null)
        {
            Debug.LogWarning("ContinueStory called without an active story.");
            return;
        }

        if (currentStory.canContinue)
        {
            // set text for the current line of dialogue
            dialogueText.text = currentStory.Continue();
            // display choices, if any, for this dialogue line
            DisplayChoices();
            // handle tags
            HandleTags(currentStory.currentTags);
        }
        else
        {
            if (!isExitingDialogue)
            {
                StartCoroutine(ExitDialogueMode());
            }
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        // Loop through each tag and handle it accordingly
        foreach (string tag in currentTags)
        {
            // parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be approptiatley parsed: " + tag);
                continue;
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            // handle the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    // set the speaker name
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    // set the portrait
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    // set the layout
                    layoutAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not correctly formatted: " + tag);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure there are no more choices than the UI can support
        int choiceCountToDisplay = Mathf.Min(currentChoices.Count, choices.Length);
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: "
            + currentChoices.Count);
        }

        // enable and initialize the choices up to the amount of choices for this line of dialogue
        for (int i = 0; i < choiceCountToDisplay; i++)
        {
            choices[i].gameObject.SetActive(true);
            choicesTexts[i].text = currentChoices[i].text;
        }

        // go through the rest of the choices and disable them
        for (int i = choiceCountToDisplay; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(selectFirstChoice());
    }

    private IEnumerator selectFirstChoice()
    {
        EventSystem currentEventSystem = EventSystem.current;
        if (currentEventSystem == null)
        {
            yield break;
        }

        // Event System requires that we clear it first, then wait
        // for at least one frame before we can set the selected object
        currentEventSystem.SetSelectedGameObject(null);
        yield return null; // Wait one frame - more efficient than WaitForEndOfFrame

        if (!dialogueIsPlaying || currentStory == null || currentStory.currentChoices.Count == 0)
        {
            yield break;
        }

        // Only set selected object if there are choices available
        for (int i = 0; i < choices.Length; i++)
        {
            if (choices[i] != null && choices[i].activeInHierarchy)
            {
                currentEventSystem.SetSelectedGameObject(choices[i].gameObject);
                yield break;
            }
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        if (!dialogueIsPlaying || currentStory == null)
        {
            Debug.LogWarning("MakeChoice called without an active dialogue.");
            return;
        }

        if (choiceIndex < 0 || choiceIndex >= currentStory.currentChoices.Count)
        {
            Debug.LogError("MakeChoice received an out-of-range index: " + choiceIndex);
            return;
        }

        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

}
