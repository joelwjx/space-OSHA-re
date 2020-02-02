using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipEngineController : Subsystem
{
    public float DistanceTravelled;
    public float GoalDistance;
    public GameObject LevelDisplay;

    private SpriteRenderer sprite;
    
    // Start is called before the first frame update
    void Start()
    {
        isActivated = true;
        sprite = GetComponent<SpriteRenderer>();

        DistanceTravelled = 0f;
        GoalDistance = 100f;
        LevelDisplay.transform.localScale = new Vector3((DistanceTravelled / GoalDistance) * 9, 0.1f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            DistanceTravelled += Time.deltaTime;
            LevelDisplay.transform.localScale = new Vector3((DistanceTravelled / GoalDistance) * 9, 0.1f, 0);
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
