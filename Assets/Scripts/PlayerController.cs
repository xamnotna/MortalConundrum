using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;

    public int maxHealth = 100;

   public int health { get { return currentHealth; } }
    public int currentHealth; //test remove later public to make it private

    Rigidbody2D rigidbody2d ;
    float horizontalInput;
    float verticalInput;

    Animator animator;
    SpriteRenderer spriteRenderer;
    Vector2 lookDirection = new Vector2(1, 0);

    // lastMoveDir is used to store the last direction the player was moving in
   Vector2 lastMoveDir = new Vector2(1, 0);


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d  = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

       

        /* if (DialogManager.isActive == true)
        {
            return;
        } */
  
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

  
         Vector2 directionVector = new Vector2(horizontalInput, verticalInput);
        //Vector directionVector = new Vector3(horizontalInput, verticalInput, 0.0f);
      

        //Vector2 lookDirection = new Vector2(directionVector.x, directionVector.y);

        if (!Mathf.Approximately(directionVector.x, 0.0f) || !Mathf.Approximately(directionVector.y, 0.0f))
        {
            lookDirection.Set(directionVector.x, directionVector.y);
            lookDirection.Normalize();
        }
    


        if (directionVector.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (directionVector.x > 0)
        {
            spriteRenderer.flipX = true;
        }

        // if DialogManager.isActive == true then stop player from walking

        if (directionVector.magnitude > 0)
        {
            lastMoveDir = lookDirection;
        }

        

        animator.SetFloat("xMove", lookDirection.x);
        animator.SetFloat("yMove", lookDirection.y);
        animator.SetFloat("Speed", directionVector.magnitude);
        animator.SetFloat("xLastMove", lookDirection.x);
        animator.SetFloat("yLastMove", lookDirection.y);

       // if player is in dialog then stop players walking animation and walking speed to 0
       
       if (DialogManager.isActive == true)
        {
            animator.SetFloat("Speed", 0);
            speed = 0;
        }
        else
        {
            speed = 3.0f;
        }
        //xLastMove and yLastMove are used to store the last direction the player was moving in
        //animator.SetFloat("xLastMove", lastMoveDir.x);
        //animator.SetFloat("yLastMove", lastMoveDir.y);
        
    }




    void FixedUpdate()
    {

        Vector2 position = rigidbody2d .position;
        position.x = position.x + speed * horizontalInput * Time.deltaTime;
        position.y = position.y + speed * verticalInput * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
