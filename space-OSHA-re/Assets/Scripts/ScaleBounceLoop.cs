using System.Collections;
using UnityEngine;

public class ScaleBounceLoop : MonoBehaviour
{
    [SerializeField] private bool playOnAwake;
    [SerializeField, Min(0)] private float loopTime = 0;
    [SerializeField] private AnimationCurve xScaleCurve;
    [SerializeField] private AnimationCurve yScaleCurve;
    [SerializeField, Min(0)] private float initialDelay = 0;
    [SerializeField, Min(0), Tooltip("The percentage (as a decimal) of the initial scale that will change per second when the loop is stopped")]
    private float stopPercentage;

    private bool stop;

    // Start is called before the first frame update
    void Start()
    {
        if(playOnAwake)
        {
            Play();
        }
    }

    public void Play()
    {
        StartCoroutine(PlayRoutine());
    }

    public void Stop()
    {
        stop = true;
    }

    private IEnumerator PlayRoutine()
    {
        float delayTimer = 0;
        while(delayTimer < initialDelay)
        {
            yield return null;
            delayTimer += Time.deltaTime;
        }

        float playTime = 0;
        Vector3 initialScale = transform.localScale;

        while (!stop)
        {
            float e = Mathf.Lerp(0, 1, playTime / loopTime);
            Vector3 normalizedScale = new Vector3(
                xScaleCurve.Evaluate(e),
                yScaleCurve.Evaluate(e),
                1);
            transform.localScale = initialScale.Mul(normalizedScale);
            yield return null;
            playTime += Time.deltaTime;
            playTime %= loopTime;
        }

        float stopTimer = 0;
        Vector3 stopScale = transform.localScale;

        float xPercent = Mathf.Abs(initialScale.x - stopScale.x) / initialScale.x;
        float yPercent = Mathf.Abs(initialScale.y - stopScale.y) / initialScale.y;
        float stopTime = Mathf.Max(xPercent, yPercent) / stopPercentage;

        while(stopTimer < stopTime)
        {
            transform.localScale = Vector3.Lerp(stopScale, initialScale, stopTimer / stopTime);
            yield return null;
            stopTimer += Time.deltaTime;
        }

        transform.localScale = initialScale;
    }
}
