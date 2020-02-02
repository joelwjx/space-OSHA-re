using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayAudio : MonoBehaviour
{
    [SerializeField] private AudioClip clipToPlay;
    [SerializeField] private float delayTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayRoutine());
    }

    private IEnumerator PlayRoutine()
    {
        yield return new WaitForSeconds(delayTime);
        var source = GetComponent<AudioSource>();
        source.clip = clipToPlay;
        source.Play();
    }
}
