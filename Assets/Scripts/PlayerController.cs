using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;

    public int maxHealth = 100;

    public int health { get { return currentHealth; } }
    public int currentHealth;
    public VectorValue startingPosition;


    Rigidbody2D rigidbody2d;
    float horizontalInput;
    float verticalInput;
    bool dialogueActive = false;

    Animator animator;
    SpriteRenderer spriteRenderer;
    Vector2 lookDirection = new Vector2(1, 0);

    Vector2 lastMoveDir = new Vector2(1, 0);



    void Awake()
    {
        transform.position = startingPosition.initialValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
        dialogueActive = false;


    }


    // Update is called once per frame
    void Update()
    {

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        DialogueManager dialogueManager = DialogueManager.GetInstance();
        bool isDialoguePlaying = dialogueManager != null && dialogueManager.dialogueIsPlaying;

        Vector2 directionVector = new Vector2(horizontalInput, verticalInput);

        if ((!Mathf.Approximately(directionVector.x, 0.0f) || !Mathf.Approximately(directionVector.y, 0.0f)) && !isDialoguePlaying)
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

        if (directionVector.sqrMagnitude > 0)
        {
            lastMoveDir = lookDirection;
        }

        animator.SetFloat("xMove", lookDirection.x);
        animator.SetFloat("yMove", lookDirection.y);
        animator.SetFloat("Speed", directionVector.sqrMagnitude);
        animator.SetFloat("xLastMove", lookDirection.x);
        animator.SetFloat("yLastMove", lookDirection.y);
        animator.SetBool("Dialogue", dialogueActive);

        if (isDialoguePlaying)
        {
            dialogueActive = true;
            speed = 0.0f;
            return;
        }
        else
        {
            dialogueActive = false;
            speed = 3.0f;
        }

        if (GameMap.GetInstance().StationSelectPanel.activeSelf) // if StationSelectPanel is active then stop player from walking
        {
            dialogueActive = true;
            speed = 0.0f;
        }
        else
        {
            dialogueActive = false;
            speed = 3.0f;
        }
    }

    void FixedUpdate()
    {

        Vector2 position = rigidbody2d.position;
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