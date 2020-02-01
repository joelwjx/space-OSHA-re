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
    private SpriteRenderer sprite;

    public float Interval;
    private float TimeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        WaterLevel = 50;
        WaterDecrements = 2;

        IsActivated = true;
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
}

