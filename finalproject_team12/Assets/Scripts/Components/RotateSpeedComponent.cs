using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Jimmy He
 * CSC631
 * Team12
 * Final Project
 */

public class RotateSpeedComponent : MonoBehaviour {

    [SerializeField]
    [Tooltip("Speed of the rotation in degrees")]
    private float _speed = 120f;

    /// <summary>
    /// Rotation speed in degrees. Any value is converted to positive.
    /// </summary>
    public float speed
    {
        get { return _speed; }
        set { _speed = Mathf.Abs(value); }
    }

}
