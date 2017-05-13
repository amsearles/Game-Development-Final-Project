using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Jimmy He
 * CSC631
 * Team12
 * Final Project
 */

public class HealthComponent : MonoBehaviour {

    // *****************
    // 
    //  Variables
    //
    // *****************

    [Tooltip("True means current health will not decrease.")]
    public bool invincible = false;

    [SerializeField]
    [Tooltip("Current health at this moment. \n" +
                "Note that if invincible then current health can only be non-decreasing.")]
    private int _currentHealth = 100;

    [SerializeField]
    [Tooltip("Maximum health.")]
    private int _maxHealth = 100;
    

    // *****************
    // 
    //  Properties
    //
    // *****************

    public int currentHealth
    {
        get { return _currentHealth; }
        set
        {
            // If invincible then allow increase in health only.
            if (invincible)
                _currentHealth = (value > _currentHealth) ? value : _currentHealth;
            else
            {
                _currentHealth = (value < 0) ? 0 : value;   // Disallow negatives.
            }

            // Upper bound current health to max health.
            if (_currentHealth >= _maxHealth) _currentHealth = _maxHealth;
        }
    }

    public int maxHealth
    {
        get { return _maxHealth; }
        set
        {
            // Constraint current health to be at or below max health.
            _maxHealth = (value < 0) ? 0 : value;
            if (_maxHealth < currentHealth) currentHealth = _maxHealth;
        }
    }

}
