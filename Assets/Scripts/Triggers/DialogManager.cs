using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{

    public Image actorImage;
    //dialogue box source images
    Image m_Image;
    //dialogue box last message
    public Sprite endSprite;
    public Sprite backgroundSprite;
    
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    public static bool isActive = false;

    public void OpenDialog(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;
        //Debug.Log("Started Conversation! Loading first message... " + messages.Length);
        DisplayMessage();
        backgroundBox.LeanScale(Vector3.one, 0.5f);
    }

    void DisplayMessage() {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;

        AnimateTextColor();

    } 

    public void NextMessage() {
        
        activeMessage++;
        if (activeMessage < currentMessages.Length) {
            DisplayMessage();
        } else {
            //Debug.Log("End of conversation!");
            backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo();
            isActive = false;
            NPC.canTalk = false;
        }
        //show sprite 2 on last message
        if (activeMessage == currentMessages.Length - 1) {
            m_Image.sprite = backgroundSprite;
        } else
        {
            m_Image.sprite = endSprite;
        }


    }

    void AnimateTextColor() {
        LeanTween.textAlpha(messageText.rectTransform, 0.0f, 0.0f);
        LeanTween.textAlpha(messageText.rectTransform, 1.0f, 0.5f);
    }

    // Start is called before the first frame update
    void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
        //Fetch the Image from the GameObject
        m_Image = GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isActive == true || Input.GetKeyDown(KeyCode.E) && isActive == true )
            {
                NextMessage();
            }   


    }
}
