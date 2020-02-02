using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Text))]
public class HazardCounter : MonoBehaviour
{
    private UnityEngine.UI.Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<UnityEngine.UI.Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = Hazard.hazardsFound.ToString("000");
    }
}
