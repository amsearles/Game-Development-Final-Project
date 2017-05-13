using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Jimmy He
 * CSC631
 * Team12
 * Final Project
 */

public class DamageComponent : MonoBehaviour {

    [SerializeField]
    [Tooltip("The collision damage dealt to other object striking it.")]
    private int _damage = 10;

    public int damage
    {
        get { return _damage; }
        set { _damage = (value > 0) ? value : 0; }
    }

}
