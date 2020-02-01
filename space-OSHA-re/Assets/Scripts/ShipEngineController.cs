using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipEngineController : MonoBehaviour
{
    public float DistanceTravelled;
    public Text DisplayText;

    public bool IsActivated;
    public bool IsOnFire;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        IsActivated = true;
        IsOnFire = false;
        sprite = GetComponent<SpriteRenderer>();

        DistanceTravelled = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActivated)
        {
            if (!IsOnFire) DistanceTravelled += Time.deltaTime;
            DisplayText.text = "Distance: " + ((int)DistanceTravelled).ToString();

            float combustionCheck = Random.Range(0, 1000);
            if (combustionCheck < 0.5) SetFireState(true);
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
