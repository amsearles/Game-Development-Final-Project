using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Jimmy He
 * CSC631
 * Team12
 * Final Project
 */

public class OnContactEffectComponent : MonoBehaviour {

    public GameObject instantiateEffect;

    /// <summary>
    /// Instantiate <see cref="onContactEffect"/> at the position of the given position.
    /// <strong>Note that this only instantiate the effect and does nothing else.</strong>
    /// </summary>
    /// <param name="transform">The point of contact of where this object has collided with.</param>
    public void OnContactEffect(Transform transform)
    {
        if (instantiateEffect != null)
            Instantiate(instantiateEffect, transform.position, transform.rotation);
    }

}
