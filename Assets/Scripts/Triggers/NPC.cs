using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public DialogTrigger trigger;
    public static bool canTalk = false;

    //Show child object when player is close
    public GameObject childObject;
    // use collider to detect player
    
    void Start()
    {
        childObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canTalk == true && Input.GetKeyDown(KeyCode.E)  && DialogManager.isActive == false)
        {
            trigger.StartDialog();
            childObject.SetActive(false);
            //canTalk = false;    
        }

    }

private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            Debug.Log("Player can talk to NPC");
            canTalk = true;
            childObject.SetActive(true);
            //trigger.StartDialog();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            Debug.Log("Player can't talk to NPC");
            canTalk = false;
            childObject.SetActive(false);
        }
    }

private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true  && DialogManager.isActive == false  && canTalk == false)
        {
            Debug.Log("Player can talk to NPC Again");
            canTalk = true;
            //trigger.StartDialog();
        }
    

    }
   

//  private void OnCollisionStay2D(Collision2D collision)
//     {
//         if (collision.gameObject.CompareTag("Player") == true  && DialogManager.isActive == false  && canTalk == false)
//         {
//             Debug.Log("Player can talk to NPC Again");
//             canTalk = true;
//             //trigger.StartDialog();
//         }
    

//     }


//     private void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (collision.gameObject.CompareTag("Player") == true)
//         {
//             Debug.Log("Player can talk to NPC");
//             canTalk = true;
//             childObject.SetActive(true);
//             //trigger.StartDialog();
//         }
//     }

//     private void OnCollisionExit2D(Collision2D collision)
//     {
//         if (collision.gameObject.CompareTag("Player") == true)
//         {
//             Debug.Log("Player can't talk to NPC");
//             canTalk = false;
//             childObject.SetActive(false);
//         }
//     }


}
