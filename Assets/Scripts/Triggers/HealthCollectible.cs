using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public int healthToGive = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            if (controller.health < controller.maxHealth)
            {
                controller.ChangeHealth(healthToGive);
                Destroy(gameObject);
            }
        }
    }

    /* //if player gets close to the collectible, highlight it
    void OnTriggerEnter2D()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.yellow;
    } */

  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         
        
    }
}
