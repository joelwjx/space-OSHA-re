using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flammable : MonoBehaviour
{
    public float IgnitionProbability;

    public bool IsIgniting;
    public bool IsBurning;
    public bool IsBroken;

    public float TimeToBurn;
    public float BurnTimer;

    public float TimeToBreak;
    public float BreakTimer;

    public float FixBurnMeter;
    public float FixBurnRating;
    public GameObject ProgressBar;

    public float GraceInterval;
    public float GraceTimer;

    public bool PlayerInArea = false;

    public delegate void BurnEvent();
    public static event BurnEvent StartBurning;

    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        IsIgniting = false;
        IsBurning = false;
        IsBroken = false;

        TimeToBurn = 10f;
        BurnTimer = 0;

        TimeToBreak = 13f;
        BreakTimer = 0;

        FixBurnRating = 3f;
        FixBurnMeter = 0;

        GraceInterval = 10f;
        GraceTimer = 0;

        sprite = GetComponent<SpriteRenderer>();

        ProgressBar.transform.localScale = new Vector3(0, 0.1f, 0);

    }

    // Update is called once per frame
    void Update()
    {
        if (IsBroken) return;

        if (GraceTimer >= 0f)
        {
            GraceTimer -= Time.deltaTime;
            return;
        }

        if (!IsIgniting && !IsBurning)
        {
            float checkIgnite = Random.Range(0, 100);
            if (checkIgnite < IgnitionProbability)
            {
                Ignite();
            }
        }
        else if (IsIgniting && !IsBurning)
        {
            if (BurnTimer > 0)
            {
                BurnTimer -= Time.deltaTime;
            }
            else
            {
                Burn();
            }

            if (Input.GetKeyDown("e") && PlayerInArea)
            {
                FixIgniting();
            }
        }
        else if (IsBurning)
        {
            if (BreakTimer > 0)
            {
                BreakTimer -= Time.deltaTime;
            }
            else
            {
                Break();
            }

            if (Input.GetKey("e") && PlayerInArea)
            {
                FixBurning();
            }
        }
    }

    void Ignite()
    {

        IsIgniting = true;
        BurnTimer = TimeToBurn;

        SetSpriteColor(Color.yellow);
    }

    void Burn()
    {
        Debug.Log("Burn");
        IsIgniting = false;
        IsBurning = true;
        BreakTimer = TimeToBreak;

        SetSpriteColor(Color.red);
    }

    void Break()
    {
        Debug.Log("Break");
        IsIgniting = false;
        IsBurning = false;
        IsBroken = true;
        // send break event
        SetSpriteColor(Color.black);
    }

    void FixIgniting()
    {
        Extinguish();
    }

    void FixBurning()
    {
        FixBurnMeter += Time.deltaTime;
        ProgressBar.transform.localScale = new Vector3(FixBurnMeter / FixBurnRating * 2, 0.1f, 0);
        if (FixBurnMeter >= FixBurnRating)
        {
            Extinguish();
        }
    }

    void Extinguish()
    {
        Debug.Log("Extinguish");
        IsBurning = false;
        IsIgniting = false;

        BurnTimer = 0;
        BreakTimer = 0;

        SetSpriteColor(Color.white);
        FixBurnMeter = 0;

        GraceTimer = GraceInterval;
        ProgressBar.transform.localScale = new Vector3(0, 0.1f, 0);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            PlayerInArea = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            PlayerInArea = false;
        }
    }

    void SetSpriteColor(Color newColor)
    {
        float originalAlpha = sprite.color.a;
        newColor.a = originalAlpha;
        sprite.color = newColor;
    }
}
