using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfByTime : MonoBehaviour {

    [Tooltip("Destroy this game object after this given duration.")]
    public float delay = 5.0f;

	// Update is called once per frame
	void Update () {
        Destroy(gameObject, delay);
	}

}
