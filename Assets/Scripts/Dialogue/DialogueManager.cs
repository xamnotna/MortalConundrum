using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    //public bool dialogueIsPlaying {get; private set;} //check if dialogue is playing
    //public static bool dialogueIsPlaying = false;
    public bool dialogueIsPlaying { get; private set; }

    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker"; //tag to identify speaker in ink JSON file
    private const string PORTRAIT_TAG = "portrait"; //tag to identify portraits in ink JSON file
    private const string LAYOUT_TAG = "layout"; //tag to identify layout in ink JSON file

    EventSystem evt;

    private void Awake()
    {
        /* if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else // if there is already an instance of this object, destroy it
        {
            Destroy(gameObject);
        } */

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
        evt = EventSystem.current;

        layoutAnimator = dialoguePanel.GetComponent<Animator>();

        // get all the choice text objects and store them in an array
        //hide all the choices if dialogue is not playing

        foreach (GameObject choice in choices)
        {
            choice.SetActive(false);
        }
        choicesTexts = new TextMeshProUGUI[choices.Length];
        // for (int i = 0; i < choices.Length; i++)
        // {
        //     choicesTexts[i] = choices[i].GetComponentInChildren<TextMeshProUGUI>();
        // }

        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesTexts[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }




    }
    //GameObject sel;
    private void Update()
    {
        // return if dialogue is not playing
        if (!dialogueIsPlaying)
        {
            return;
        }


        // prevent deselection of choices
        if (currentStory.currentChoices.Count > 0)
        {
            if (evt.currentSelectedGameObject == null)
            {
                evt.SetSelectedGameObject(choices[0]);
            }
        }

        // if (evt.currentSelectedGameObject != null && evt.currentSelectedGameObject != sel)
        // {
        //     sel = evt.currentSelectedGameObject;
        // }
        // else if (sel != null && evt.currentSelectedGameObject == null)
        // {
        //     evt.SetSelectedGameObject(sel);
        // }


        if (currentStory.currentChoices.Count == 0 && Input.GetKeyDown(KeyCode.E))
        {
            ContinueStory();
        }
        else if (currentStory.currentChoices.Count > 0 && Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < choices.Length; i++)
            {
                if (choices[i].gameObject.activeSelf && EventSystem.current.currentSelectedGameObject == choices[i])
                {
                    currentStory.ChooseChoiceIndex(i);
                    ContinueStory();
                    break;
                }
            }
        }

        /*  if (Input.GetKeyDown(KeyCode.E) && currentStory.currentChoices.Count > 0)
         {
             if (currentStory.currentChoices.Count > 0)
             {
                 for (int i = 0; i < choices.Length; i++)
                 {
                     if (choices[i].gameObject.activeSelf && EventSystem.current.currentSelectedGameObject == choices[i])
                     {
                         currentStory.ChooseChoiceIndex(i);
                         ContinueStory();
                         break;
                     }
                 }
             }
              else
              {
                  ContinueStory();
              }
         }   
         else if (Input.GetKeyDown(KeyCode.E))
         {
             ContinueStory();
         } */



        /* if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentStory.currentChoices.Count > 0)
            {
                for (int i = 0; i < choices.Length; i++)
                {
                    if (choices[i].gameObject.activeSelf && EventSystem.current.currentSelectedGameObject == choices[i])
                    {
                        currentStory.ChooseChoiceIndex(i);
                        ContinueStory();
                        break;
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                ContinueStory();
            }
        } */


        // handle selecting a choice when submit is pressed
        //  if (Input.GetKeyDown(KeyCode.E))
        // {
        //     if (currentStory.currentChoices.Count > 0)
        //     {
        //     for (int i = 0; i < choices.Length; i++)
        //     {
        //         if (choices[i].gameObject.activeSelf && EventSystem.current.currentSelectedGameObject == choices[i])
        //         {
        //             currentStory.ChooseChoiceIndex(i);
        //             ContinueStory();
        //             break;
        //         }
        //     }
        //     } 
        //     else
        //     {
        //         ContinueStory();
        //     }
        // }

    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        displayNameText.text = "???";
        portraitAnimator.Play("default");
        layoutAnimator.Play("right");


        ContinueStory();
    }

    public IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
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
            StartCoroutine(ExitDialogueMode());
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
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: "
            + currentChoices.Count);
            //return; // return if there are more choices than the UI can support
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesTexts[index].text = choice.text;
            index++;
        }
        // go through the rest of the choices and disable them
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        // select the first choice if there are any
        /* if (currentChoices.Count > 0)
        {
            StartCoroutine(selectFirstChoice());
        } */

        StartCoroutine(selectFirstChoice());


    }

    private IEnumerator selectFirstChoice()
    {
        // Event Systems is requires we clear it first, then wait
        // for at least one frame before we can set the selected object
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

}
