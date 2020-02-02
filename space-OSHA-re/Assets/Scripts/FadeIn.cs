using System.Collections;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    [SerializeField, Min(0)] private float fadeTime;
    [SerializeField] private UnityEngine.UI.Image fadeImage;
    [SerializeField] private AnimationCurve fadeCurve;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeRoutine());
    }

    private IEnumerator FadeRoutine()
    {
        Time.timeScale = 0;
        float timer = 0;
        Color c = fadeImage.color;
        fadeImage.gameObject.SetActive(true);

        while(timer < fadeTime)
        {
            fadeImage.color = new Color(c.r, c.g, c.b,
                fadeCurve.Evaluate( Mathf.Lerp(1, 0, timer / fadeTime)));
            yield return null;
            timer += Time.unscaledDeltaTime;
        }
        fadeImage.color = new Color(c.r, c.g, c.b, 0);
        fadeImage.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
