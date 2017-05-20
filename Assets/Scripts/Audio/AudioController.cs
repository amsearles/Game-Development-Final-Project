/**
 * Elric Dang
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    void Start()
    {
        gameObject.GetComponent<UnityEngine.UI.Slider>().value = AudioListener.volume;
    }

    void Update()
    {
        AudioListener.volume = gameObject.GetComponent<UnityEngine.UI.Slider>().value;
    }

}
