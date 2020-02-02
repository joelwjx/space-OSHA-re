﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenGeneratorController : Subsystem
{
    public int OxygenLevel;
    public int OxygenDecrements;
    public int OxygenIncrements;
    public Text DisplayText;
    public PowerGeneratorController generator;

    private SpriteRenderer sprite;

    public float Interval;
    private float TimeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        OxygenLevel = 50;
        OxygenDecrements = 5;
        OxygenIncrements = 1;

        isActivated = true;
        powerLevel = generator.PowerLevel;
        sprite = GetComponent<SpriteRenderer>();

        Interval = 0.5f;
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
                if (OxygenLevel > 0) OxygenLevel -= OxygenDecrements;
                if (OxygenLevel <= 0) GameStateManager.instance.InitiateLoseState();
                DisplayText.text = OxygenLevel.ToString("000");
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
                if (OxygenLevel < 100) OxygenLevel += OxygenIncrements;
                DisplayText.text = OxygenLevel.ToString("000");

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
