using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerGeneratorController : MonoBehaviour
{
    public int PowerLevel;
    public GameObject LevelDisplay;

    public float powerDropInterval;
    public float powerDropTimer;

    public bool PlayerInArea;

    public float ChargeMeter;
    public float ChargeRate;

    public GameObject ProgressBarPrefab;
    private GameObject progressBar;

    // Start is called before the first frame update
    void Start()
    {
        PowerLevel = 100;

        powerDropInterval = 3f;
        powerDropTimer = powerDropInterval;

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

        LevelDisplay.transform.localScale = new Vector3((PowerLevel / 100f) * 2, 0.1f, 0);
        //DisplayText.text = PowerLevel.ToString("000");

        if (PlayerInArea && Input.GetKey("e"))
        {
            ChargeMeter += Time.deltaTime;
            progressBar.transform.localScale = new Vector3(ChargeMeter / ChargeRate * 30, 2, 0);
            if (ChargeMeter >= ChargeRate)
            {
                ChargeMeter = 0;
                PowerLevel += 50;
                if (PowerLevel >= 100) PowerLevel = 100;
            }
        }
        else
        {
            ChargeMeter = 0;
            progressBar.transform.localScale = new Vector3(0, 2, 0);
        }
    }


    void OnTriggerEnter2D(Collider2D collide)
    {
        if (collide.gameObject.tag == "Player")
        {
            PlayerInArea = true;
        }
    }

    void OnTriggerExit2D(Collider2D collide)
    {
        if (collide.gameObject.tag == "Player")
        {
            PlayerInArea = false;
        }
    }
}
