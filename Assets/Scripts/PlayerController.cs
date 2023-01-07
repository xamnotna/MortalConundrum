using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;

    public int maxHealth = 100;

   public int health { get { return currentHealth; } }
    int currentHealth;

    Rigidbody2D rigidbody2d ;
    float horizontalInput;
    float verticalInput;

    Animator animator;
    SpriteRenderer spriteRenderer;
    Vector2 lookDirection = new Vector2(1, 0);
    // lastMoveDir is used to store the last direction the player was moving in
    //Vector3 lastMoveDir = Vector3.zero;


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
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

  
        // Vector2 directionVector = new Vector2(horizontalInput, verticalInput);
        Vector3 directionVector = new Vector3(horizontalInput, verticalInput, 0);
        

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

        
        animator.SetFloat("xMove", lookDirection.x);
        animator.SetFloat("yMove", lookDirection.y);
        animator.SetFloat("Speed", directionVector.magnitude);

        Debug.Log("x: " + lookDirection.x + " " +  "y: " + lookDirection.y);

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
