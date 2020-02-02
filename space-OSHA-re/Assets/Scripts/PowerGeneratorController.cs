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
    public bool PlayerInArea;

    public float ChargeMeter;
    public float ChargeRate;

    public GameObject ProgressBarPrefab;
    private GameObject progressBar;

    // Start is called before the first frame update
    void Start()
    {
        PowerLevel = 100;

        powerDropInterval = 2f;
        powerDropTimer = powerDropInterval;

        sprite = GetComponent<SpriteRenderer>();

        ChargeMeter = 0f;
        ChargeRate = 5f;

        progressBar = Instantiate(ProgressBarPrefab, gameObject.transform, false);
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
            }
            powerDropTimer = powerDropInterval;
        }

        DisplayText.text = PowerLevel.ToString("000");

        if (PlayerInArea && Input.GetKey("e"))
        {
            ChargeMeter += Time.deltaTime;
            progressBar.transform.localScale = new Vector3(ChargeMeter / ChargeRate, 0.1f, 0);
            if (ChargeMeter >= ChargeRate)
            {
                ChargeMeter = 0;
                PowerLevel += 25;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.gameObject.tag == "Player")
        {
            PlayerInArea = true;
            Color spriteColor = sprite.color;
            spriteColor.a = 0.75f;
            sprite.color = spriteColor;
        }
    }

    void OnTriggerExit2D(Collider2D collide)
    {
        if (collide.gameObject.tag == "Player")
        {
            PlayerInArea = false;
            Color spriteColor = sprite.color;
            spriteColor.a = 1;
            sprite.color = spriteColor;
        }
    }

}
