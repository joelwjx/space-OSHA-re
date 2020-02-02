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

    public float FixMeter;
    public float FixIgniteRating;
    public float FixBurnRating;
    public GameObject ProgressBarPrefab;
    private GameObject ProgressBar;
    public Image FireIcon;
    public float GraceInterval;
    public float GraceTimer;

    public bool PlayerInArea = false;

    public delegate void BurnEvent();
    public static event BurnEvent StartBurning;

    [SerializeField] private SpriteRenderer sprite;

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
        FixIgniteRating = 1f;
        FixMeter = 0;

        GraceInterval = 10f;
        GraceTimer = 0;

        if(!sprite) sprite = GetComponent<SpriteRenderer>();

        ProgressBar = Instantiate(ProgressBarPrefab, gameObject.transform, false);
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
                if (Input.GetKey("e") && PlayerInArea)
                {
                    FixIgniting();
                }
                else
                {
                    BurnTimer -= Time.deltaTime;
                }
            }
            else
            {
                Burn();
            }
        }
        else if (IsBurning)
        {
            if (BreakTimer > 0)
            {
                if (Input.GetKey("e") && PlayerInArea)
                {
                    FixBurning();
                }
                else
                {
                    BreakTimer -= Time.deltaTime;
                }
            }
            else
            {
                Break();
            }
        }
    }

    void Ignite()
    {
        IsIgniting = true;
        BurnTimer = TimeToBurn;
        FixMeter = 0;

        SetSpriteColor(Color.yellow);
    }

    void Burn()
    {
        IsIgniting = false;
        IsBurning = true;
        FixMeter = 0;
        BreakTimer = TimeToBreak;
        if(FireIcon) FireIcon.gameObject.SetActive(true);

        SetSpriteColor(Color.red);
    }

    void Break()
    {
        IsIgniting = false;
        IsBurning = false;
        IsBroken = true;
        FixMeter = 0;

        // send break event
        SetSpriteColor(Color.black);
    }

    void FixIgniting()
    {
        FixMeter += Time.deltaTime;
        ProgressBar.transform.localScale = new Vector3(FixMeter / FixIgniteRating * 2, 0.1f, 0);
        if (FixMeter >= FixIgniteRating)
        {
            Extinguish();
        }
    }

    void FixBurning()
    {
        FixMeter += Time.deltaTime;
        ProgressBar.transform.localScale = new Vector3(FixMeter / FixBurnRating * 2, 0.1f, 0);
        if (FixMeter >= FixBurnRating)
        {
            Extinguish();
        }
    }

    void Extinguish()
    {
        IsBurning = false;
        IsIgniting = false;
        if(FireIcon) FireIcon.gameObject.SetActive(false);
        BurnTimer = 0;
        BreakTimer = 0;

        SetSpriteColor(Color.white);
        FixMeter = 0;

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
