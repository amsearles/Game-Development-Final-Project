using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Jimmy He
 * CSC631
 * Team12
 * Final Project
 */
[System.Obsolete("This component serves too little purpose to satisfy its existence. Please delete it from use")]
public class OnDestroyEffectComponent : MonoBehaviour {

    public GameObject gameObjectEffect;

    private void OnDestroy()
    {
        if (gameObjectEffect != null)
            Instantiate(gameObjectEffect, transform.position, transform.rotation);

    }
}
