using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    public float Interval;
    public float TimeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        TimeElapsed = 0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Tick()
    {
        if (TimeElapsed <= Interval)
        {
            TimeElapsed += Time.deltaTime;
        }
        else
        {
            TimeElapsed = 0f;
        }
    }
}
