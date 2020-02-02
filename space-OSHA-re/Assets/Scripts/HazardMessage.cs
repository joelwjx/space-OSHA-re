using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HazardMessage : MonoBehaviour
{
    public static HazardMessage Instance { get; private set; }
    [SerializeField] private Text text;

    [SerializeField, Min(0)] private float displayTime;

    private Queue<string> messages = new Queue<string>();
    private bool isPlaying => gameObject.activeSelf;

    private void Awake()
    {
        if(Instance != null)
        {
            throw new System.Exception("More than one instance of HazardMessage is not allowed!");
        }
        Instance = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void PlayMessage(string message)
    {
        messages.Enqueue(message);
        if (isPlaying) return;

        Debug.Log("Playing");
        gameObject.SetActive(true);
        StartCoroutine(MessageRoutine());
    }

    private IEnumerator MessageRoutine()
    {
        while(messages.Count > 0)
        {
            text.text = messages.Dequeue();
            float displayTimer = 0;
            while(displayTimer < displayTime)
            {
                displayTimer += Time.deltaTime;
                yield return null;
            }
        }
        gameObject.SetActive(false);
    }
}
