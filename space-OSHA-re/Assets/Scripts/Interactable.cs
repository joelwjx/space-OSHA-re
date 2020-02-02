using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.gameObject.tag == "Player")
        {
            Color spriteColor = sprite.color;
            spriteColor.a = 0.5f;
            sprite.color = spriteColor;
        }
    }

    void OnTriggerExit2D(Collider2D collide)
    {
        if (collide.gameObject.tag == "Player")
        {
            Color spriteColor = sprite.color;
            spriteColor.a = 1;
            sprite.color = spriteColor;
        }
    }
}
