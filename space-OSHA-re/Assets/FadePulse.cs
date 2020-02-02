using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePulse : MonoBehaviour
{
    private Image sprite;
    private float val;

    // Start is called before the first frame update
    void Start()
    {
        val = 0;
        sprite = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        val += Time.deltaTime * 2;
        float newAlpha = Mathf.Abs(Mathf.Sin(val));
        Color newColor = sprite.color;
        newColor.a = newAlpha;
        sprite.color = newColor;
    }
}
