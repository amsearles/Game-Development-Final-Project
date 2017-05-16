/**
 * Elric Dang
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource source;
    private AudioClip clip;

    void PlayClip()
    {
        if (source != null && clip != null)
            source.PlayOneShot(clip);
    }

    void Start()
    {
        if (GetComponent<AudioSource>() != null)
            source = GetComponent<AudioSource>();
        if (GetComponent<AudioSource>().clip != null)
            clip = GetComponent<AudioSource>().clip;
    }

    void Update()
    {

    }

}
