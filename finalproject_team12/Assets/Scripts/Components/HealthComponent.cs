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
    [SerializeField]
    private bool _invincible = false;

    [Tooltip("Effect to attach onto the Player to indicate invincibility is in effect.")]
    [SerializeField]
    private GameObject invincibilityEffect;

    [SerializeField]
    [Tooltip("Current health at this moment. \n" +
                "Note that if invincible then current health can only be non-decreasing.")]
    private int _currentHealth = 100;

    [SerializeField]
    [Tooltip("Maximum health.")]
    private int _maxHealth = 100;

    // Instantiated sphere representing invincibility as active.
    private GameObject invincibilitySphere;

    // *****************
    // 
    //  Properties
    //
    // *****************

    public bool invincible
    {
        get { return _invincible; }
        set
        {
            _invincible = value;
            
            if (invincibilitySphere != null)
                invincibilitySphere.SetActive(_invincible);
        }
    }

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
    
    // *****************
    // 
    //  Public Methods
    //
    // *****************

    public bool isInvincible()
    {
        return invincible;
    }

    /// <summary>Set the state of <see cref="invincible"/> to the given value.</summary>
    /// <param name="value"></param>
    public void SetInvincible(bool value)
    {
        invincible = value;
    }

    /// <summary>
    /// Set the state of invincibility for a set duration and then return to its previous state. 
    /// </summary>
    /// <param name="value">Boolean of what to set <see cref="invincible"/>.</param>
    /// <param name="duration">Duration in seconds. -1 means indefinite.</param>
    public void SetInvincible(bool value, float duration)
    {
        // Don't execute if invincible is false and the value given is false.
        if (duration < 0 || !value)
        {
            invincible = value;
            StopCoroutine(TimedInvincibility(value, duration));
        }
        else  // Proper scenario of duration >= 0 and value == true.
        {
            // Stop similar coroutine from running more than once.
            StopCoroutine(TimedInvincibility(value, duration));
            StartCoroutine(TimedInvincibility(value, duration));
        }
    }
    

    // *****************
    // 
    //  Private Methods
    //
    // *****************

    private IEnumerator TimedInvincibility(bool value, float duration)
    {
        invincible = value;

        yield return new WaitForSeconds(duration);
        
        invincible = !invincible;
    }

    private void Start()
    {
        // Set up Invincibility Effect object to encompass this entire unit.
        Renderer[] renderers = transform.root.GetComponentsInChildren<Renderer>();
        Bounds bounds = new Bounds();

        foreach (Renderer x in renderers)
            bounds.Encapsulate(x.bounds);

        // Instantiate the invincibilityEffect and have it attached to the unit.
        if (invincibilityEffect != null)
        {

            invincibilitySphere = Instantiate(invincibilityEffect, transform.position,
                                                        transform.rotation, transform.root);

            invincibilitySphere.transform.localScale = new Vector3(bounds.extents.x,
                                                                    bounds.extents.x,
                                                                    bounds.extents.x);

            invincibilitySphere.SetActive(false);
        }
        
    }

}
