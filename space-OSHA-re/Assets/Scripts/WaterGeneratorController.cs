using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterGeneratorController : Subsystem
{
    public int WaterLevel;
    public int WaterDecrements;
    public GameObject LevelDisplay;

    private SpriteRenderer sprite;

    public float Interval;
    private float TimeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        WaterLevel = 50;
        LevelDisplay.transform.localScale = new Vector3((WaterLevel / 100f) * 2, 0.1f, 0);

        WaterDecrements = 3;

        isActivated = true;
        sprite = GetComponent<SpriteRenderer>();

        Interval = 5;
        TimeElapsed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActivated)
        {
            if (TimeElapsed <= Interval)
            {
                TimeElapsed += Time.deltaTime;
            }
            else
            {
                if (WaterLevel > 0) WaterLevel -= WaterDecrements;
                LevelDisplay.transform.localScale = new Vector3((WaterLevel / 100f) * 2, 0.1f, 0);
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
                LevelDisplay.transform.localScale = new Vector3((WaterLevel / 100f) * 2, 0.1f, 0);
                TimeElapsed = 0f;
            }
        }
    }

    public void SetActiveState(bool value)
    {
        isActivated = value;
        Color spriteColor = sprite.color;
        spriteColor.a = value ? 1 : 0.5f;
        sprite.color = spriteColor;
    }
}

