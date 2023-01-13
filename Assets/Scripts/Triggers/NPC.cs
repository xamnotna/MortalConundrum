using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public DialogTrigger trigger;
    public static bool canTalk = false;
    
 
    // Start is called before the first frame update
    /* void Start()
    {
        trigger = GetComponent<DialogTrigger>();
    }
 */
    // Update is called once per frame
    void Update()
    {
        if (canTalk == true && Input.GetKeyDown(KeyCode.E)  && DialogManager.isActive == false)
        {
            trigger.StartDialog();
            //canTalk = false;    
        }
        
    }
   
  /*  void LateUpdate()
    {
        //check if the player is in the trigger area
        if (canTalk == true && Input.GetKeyDown(KeyCode.E) && DialogManager.isActive == true)
        {
            DialogManager.instance.NextMessage();
        }
    } */

    //check if the player is in the collision area

 private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true  && DialogManager.isActive == false  && canTalk == false)
        {
            Debug.Log("Player can talk to NPC Again");
            canTalk = true;
            //trigger.StartDialog();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            Debug.Log("Player can talk to NPC");
            canTalk = true;
            //trigger.StartDialog();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
        {
            Debug.Log("Player can't talk to NPC");
            canTalk = false;
        }
    }



}
