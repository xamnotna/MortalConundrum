using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NonPlayerCharacter : MonoBehaviour
{
    public GameObject dialogBox;
    public TMPro.TextMeshProUGUI dialogText; 
    public string[] dialog;
    private int index;

    public GameObject contButton;
    public float wordsPerSecond;
    public bool isPlayerInRange;

    void Update()
    {
        if (isPlayerInRange)
        {
            if (Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.E)))
            {
                if (dialogBox.activeInHierarchy)
                {
                    NextLine();
                }
                else
                {
                    dialogBox.SetActive(true);
                    StartCoroutine(Typing());
                }
             
            }
        
        }

        if (dialogText.text == dialog[index])
        {
            contButton.SetActive(true);
        } 

       
       /*  else
        {
            contButton.SetActive(false);
        } */
    }

    public void ZeroText()
    {
        dialogText.text = "";
        index = 0;
        dialogBox.SetActive(false);

    }   

    IEnumerator Typing()
    {
        foreach (char letter in dialog[index].ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(wordsPerSecond);
        }
    }

    public void NextLine()
    {

        contButton.SetActive(false);

        if (index < dialog.Length - 1)
        {
            index++;
            dialogText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            ZeroText();
        }
    }

    public void DisplayDialog()
    {
        isPlayerInRange = true;
    }

    public void HideDialog()
    {
        isPlayerInRange = false;
        ZeroText();
    }

   /*  private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            ZeroText();
        }
    } */




}
    // public float displayTime = 4.0f;
    // public GameObject dialogBox;
    // float timerDisplay;
    
    // void Start()
    // {
    //     dialogBox.SetActive(false);
    //     timerDisplay = -1.0f;
    // }
    
    // void Update()
    // {
    //     if (timerDisplay >= 0)
    //     {
    //         timerDisplay -= Time.deltaTime;
    //         if (timerDisplay < 0)
    //         {
    //             dialogBox.SetActive(false);
    //         }

    //     } 
        


    // }

    
    // public void DisplayDialog()
    // {
    //     timerDisplay = displayTime;
    //     dialogBox.SetActive(true);
    // }