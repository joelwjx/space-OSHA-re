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
    public event BurnEvent OnBurn;

    public delegate void IgniteEvent();
    public event IgniteEvent OnIgnite;

    [SerializeField] private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        IsIgniting = false;
        IsBurning = false;
        IsBroken = false;

        TimeToBurn = 3f;
        BurnTimer = 0;

        TimeToBreak = 30f;
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
                    ResetFixProgress();
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
                    ResetFixProgress();
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
        ResetFixProgress();

        SetSpriteColor(Color.yellow);
        OnIgnite?.Invoke();
    }

    void Burn()
    {
        IsIgniting = false;
        IsBurning = true;
        ResetFixProgress();
        BreakTimer = TimeToBreak;
        if(FireIcon) FireIcon.gameObject.SetActive(true);

        SetSpriteColor(Color.red);
        OnBurn?.Invoke();
    }

    void Break()
    {
        IsIgniting = false;
        IsBurning = false;
        IsBroken = true;
        ResetFixProgress();

        // send break event
        SetSpriteColor(Color.black);
    }

    void ResetFixProgress()
    {
        FixMeter = 0;
        ProgressBar.transform.localScale = new Vector3(0, 2f, 0);
    }

    void FixIgniting()
    {
        FixMeter += Time.deltaTime;
        ProgressBar.transform.localScale = new Vector3(FixMeter / FixIgniteRating * 30f, 2f, 0);
        if (FixMeter >= FixIgniteRating)
        {
            Extinguish();
        }
    }

    void FixBurning()
    {
        FixMeter += Time.deltaTime;
        ProgressBar.transform.localScale = new Vector3(FixMeter / FixBurnRating * 30, 2f, 0);
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
        ResetFixProgress();

        GraceTimer = GraceInterval;
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
