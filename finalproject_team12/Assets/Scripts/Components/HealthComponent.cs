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
    private GameObject invincibilitySphere;

    [SerializeField]
    [Tooltip("Current health at this moment. \n" +
                "Note that if invincible then current health can only be non-decreasing.")]
    private int _currentHealth = 100;

    [SerializeField]
    [Tooltip("Maximum health.")]
    private int _maxHealth = 100;

    // Instantiated sphere representing invincibility as active.
    private GameObject currentInvSphere;

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
            
            if (currentInvSphere != null)
                currentInvSphere.SetActive(_invincible);
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

    private Bounds CalculateShipBounds()
    {
        // Set up Invincibility Effect object to encompass this entire unit.
        Renderer[] renderers = transform.root.GetComponentsInChildren<Renderer>();
        Bounds bounds = new Bounds();
        //Collider[] colliders = transform.root.GetComponentsInChildren<Collider>();


        foreach (Renderer x in renderers)
        {
            if (x.CompareTag(transform.root.tag))
            {
                bounds.Encapsulate(x.bounds);
            }
        }

        return bounds;
    }

    private void SetInvincibilitySphere(Bounds bounds)
    {


        // Destroy existing effect if made prior.
        if (currentInvSphere != null)
            Destroy(currentInvSphere.gameObject);


        // Do nothing if no given effect.
        if (invincibilitySphere == null) return;

        // Instantiate the invincibility effect and have it attached to the unit.
        if (currentInvSphere == null)
            currentInvSphere = Instantiate(invincibilitySphere, transform.position,
                                                        transform.rotation, transform.root);
        
        // Scale the newly instantiated effect to encompass the game unit.
        if (currentInvSphere != null)
            currentInvSphere.transform.localScale = new Vector3(bounds.size.x + 2,
                                                                    bounds.size.x + 2,
                                                                    bounds.size.x + 2);
        
    }

    // *****************
    // 
    //  Unity Methods
    //
    // *****************

    private void Start()
    {
        Bounds bounds = CalculateShipBounds();

        SetInvincibilitySphere(bounds);

        if (currentInvSphere != null)
            currentInvSphere.SetActive(false);
    }

}
