using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2d ;
    float horizontalInput;
    float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d  = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d .position;
        position.x = position.x + 3.0f * horizontalInput * Time.deltaTime;
        position.y = position.y + 3.0f * verticalInput * Time.deltaTime;

        rigidbody2d .MovePosition(position);
    }
}
