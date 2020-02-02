using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// When spawning an object, have it grow in scale from (0,0,1) to (1,1,1)
public class GrowIn : MonoBehaviour
{
    [SerializeField] private float growTime;
    [SerializeField] private AnimationCurve xSizeCurve;
    [SerializeField] private AnimationCurve ySizeCurve;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GrowRoutine());
    }

    private IEnumerator GrowRoutine()
    {
        float timer = 0;
        Vector3 initialScale = transform.localScale;
        transform.localScale = new Vector3(0, 0, 1);

        while(timer < growTime)
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
        transform.localScale = initialScale;
    }
}
