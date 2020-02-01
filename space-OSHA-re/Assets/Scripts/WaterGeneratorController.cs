using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterGeneratorController : MonoBehaviour
{
    public int WaterLevel;
    public int WaterDecrements;
    public Text DisplayText;

    public bool IsActivated;
    public bool IsOnFire;
    private SpriteRenderer sprite;

    public float Interval;
    private float TimeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        WaterLevel = 50;
        WaterDecrements = 2;

        IsActivated = true;
        IsOnFire = false;
        sprite = GetComponent<SpriteRenderer>();

        Interval = 0.5f;
        TimeElapsed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsActivated)
        {
            if (TimeElapsed <= Interval)
            {
                TimeElapsed += Time.deltaTime;
            }
            else
            {
                if (WaterLevel > 0) WaterLevel -= WaterDecrements;
                DisplayText.text = "Water Level: " + WaterLevel.ToString();
                TimeElapsed = 0f;
            }
        }
        else
        {
            if (TimeElapsed <= Interval)
            {
                TimeElapsed += Time.deltaTime;
            }
            else
            {
                if (WaterLevel < 100) WaterLevel += WaterDecrements;
                DisplayText.text = "Water Level: " + WaterLevel.ToString();
                float combustionCheck = Random.Range(0, 100);
                if (combustionCheck < 5f) SetFireState(true);
                TimeElapsed = 0f;
            }
        }

    }

    public void SetActiveState(bool value)
    {
        IsActivated = value;
        Color spriteColor = sprite.color;
        spriteColor.a = value ? 1 : 0.5f;
        sprite.color = spriteColor;
    }

    public void SetFireState(bool value)
    {
        IsOnFire = value;
        Color spriteColor = sprite.color;
        spriteColor = value ? Color.red : Color.white;
        sprite.color = spriteColor;
    }

    void OnTriggerStay2D(Collider2D collide)
    {
        if (collide.gameObject.tag == "Player" && IsOnFire && Input.GetKeyDown("e"))
        {
            SetFireState(false);
        }
    }
}

