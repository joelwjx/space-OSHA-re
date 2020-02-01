﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipEngineController : Subsystem
{
    public float DistanceTravelled;
    public Text DisplayText;
    public PowerGeneratorController generator;

    private SpriteRenderer sprite;
    
    // Start is called before the first frame update
    void Start()
    {
        isActivated = true;
        sprite = GetComponent<SpriteRenderer>();

        powerLevel = generator.PowerLevel;
        DistanceTravelled = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        powerLevel = generator.PowerLevel;
        if (isActivated)
        {
            DistanceTravelled += Time.deltaTime;
            DisplayText.text = ((int)DistanceTravelled).ToString("000");
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
