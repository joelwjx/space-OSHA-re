using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipEngineController : MonoBehaviour
{
    public float DistanceTravelled;
    public Text DisplayText;

    public bool IsActivated;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        IsActivated = true;
        sprite = GetComponent<SpriteRenderer>();

        DistanceTravelled = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActivated)
        {
            DistanceTravelled += Time.deltaTime;
            DisplayText.text = "Distance: " + ((int)DistanceTravelled).ToString();
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
