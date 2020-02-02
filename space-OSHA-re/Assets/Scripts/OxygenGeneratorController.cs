using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenGeneratorController : Subsystem
{
    public int OxygenLevel;
    public int OxygenDecrements;
    public int OxygenIncrements;
    public GameObject LevelDisplay;

    private SpriteRenderer sprite;

    public float Interval;
    private float TimeElapsed;

    public GameObject BurningIconPrefab;
    public GameObject WarningIconPrefab;
    private GameObject burningIcon;
    private GameObject warningIcon;

    // Start is called before the first frame update
    void Start()
    {
        OxygenLevel = 50;
        LevelDisplay.transform.localScale = new Vector3((OxygenLevel / 100f) * 2, 0.1f, 0);

        OxygenDecrements = 5;
        OxygenIncrements = 1;

        isActivated = true;
        sprite = GetComponent<SpriteRenderer>();

        Interval = 3f;
        TimeElapsed = 0f;

        GetComponent<Flammable>().OnBurn += StartBurning;
        GetComponent<Flammable>().OnIgnite += StartIgniting;
        GetComponent<Flammable>().OnExtinguish += Extinguish;
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
                LevelDisplay.transform.localScale = new Vector3((OxygenLevel / 100f) * 2, 0.1f, 0);
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
                LevelDisplay.transform.localScale = new Vector3((OxygenLevel / 100f) * 2, 0.1f, 0);
                TimeElapsed = 0f;
            }
        }
        if (OxygenLevel <= 0) GameStateManager.instance.InitiateLoseState();
    }

    public void SetActiveState(bool value)
    {
        isActivated = value;
        Color spriteColor = sprite.color;
        spriteColor.a = value ? 1 : 0.5f;
        sprite.color = spriteColor;
    }

    void StartBurning()
    {
        Destroy(warningIcon);
        burningIcon = Instantiate(BurningIconPrefab, LevelDisplay.transform.position + new Vector3(25, -4), Quaternion.identity, LevelDisplay.transform.parent);
    }

    void StartIgniting()
    {
        warningIcon = Instantiate(WarningIconPrefab, LevelDisplay.transform.position + new Vector3(25, -4), Quaternion.identity, LevelDisplay.transform.parent);
    }

    void Extinguish()
    {
        if (warningIcon != null) Destroy(warningIcon);
        if (burningIcon != null) Destroy(burningIcon);
    }
}
