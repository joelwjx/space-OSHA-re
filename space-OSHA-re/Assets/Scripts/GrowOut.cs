using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Probably should be called "shrink out", but I like the consistency more
public class GrowOut : MonoBehaviour
{
    [SerializeField] private float growTime;
    [SerializeField] private float initialDelay;
    [SerializeField] private AnimationCurve xSizeCurve;
    [SerializeField] private AnimationCurve ySizeCurve;

    public UnityEngine.Events.UnityEvent onGrowEnd;

    public void StartGrow()
    {
        StartCoroutine(GrowRoutine());
    }

    private IEnumerator GrowRoutine()
    {
        float delayTimer = 0;
        while(delayTimer < initialDelay)
        {
            yield return null;
            delayTimer += Time.deltaTime;
        }

        float timer = 0;
        Vector3 initialScale = transform.localScale;

        while (timer < growTime)
        {
            float e = Mathf.Lerp(0, 1, timer / growTime);
            Vector3 normalizedScale = new Vector3(
                xSizeCurve.Evaluate(e),
                ySizeCurve.Evaluate(e),
                1);
            transform.localScale = initialScale.Mul(normalizedScale);
            yield return null;
            timer += Time.deltaTime;
        }
        transform.localScale = new Vector3(0, 0, 1);
        onGrowEnd.Invoke();
    }
}
