using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenGeneratorController : MonoBehaviour
{
    public int OxygenLevel;
    public int OxygenDecrements;
    public Text DisplayText;

    public bool IsActivated;
    public bool IsOnFire;
    private SpriteRenderer sprite;

    public float Interval;
    private float TimeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        OxygenLevel = 50;
        OxygenDecrements = 5;

        IsActivated = true;
        IsOnFire = false;
        sprite = GetComponent<SpriteRenderer>();

        Interval = 0.5f;
        TimeElapsed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsActivated || IsOnFire)
        {
            if (TimeElapsed <= Interval)
            {
                TimeElapsed += Time.deltaTime;
            }
            else
            {
                if (OxygenLevel > 0) OxygenLevel -= OxygenDecrements;
                DisplayText.text = "Oxygen Level: " + OxygenLevel.ToString();
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
                if (OxygenLevel < 100) OxygenLevel += OxygenDecrements;
                DisplayText.text = "Oxygen Level: " + OxygenLevel.ToString();
                float combustionCheck = Random.Range(0, 100);

                Debug.Log(combustionCheck);
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
            Debug.Log("Clicked");
            SetFireState(false);
        }
    }
}
