using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerGeneratorController : MonoBehaviour
{
    public int PowerLevel;
    public Text DisplayText;

    public float powerDropInterval;
    public float powerDropTimer;

    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        PowerLevel = 100;

        powerDropInterval = 0.5f;
        powerDropTimer = powerDropInterval;

        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (powerDropTimer >= 0f)
        {
            powerDropTimer -= Time.deltaTime;
        }
        else
        {
            if (PowerLevel > 0)
            {
                PowerLevel -= 5;
                Debug.Log(PowerLevel);
            }
            powerDropTimer = powerDropInterval;
        }

        DisplayText.text = "Power Level: " + PowerLevel.ToString();
    }

    void OnTriggerStay2D(Collider2D collide)
    {
        if (collide.gameObject.tag == "Player" && Input.GetKeyDown("e"))
        {
            PowerLevel = 100;
        }
    }

    void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.gameObject.tag == "Player")
        {
            Color spriteColor = sprite.color;
            spriteColor.a = 0.75f;
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
