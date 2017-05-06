using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Jimmy He & Elric Dang
 * CSC631
 * Team12
 * Final Project
 */

[DisallowMultipleComponent]
public class GameUnit : MonoBehaviour
{
    // *****************
    // 
    //  Variables
    //
    // *****************

    [Tooltip("Effect to instantiate upon colliding with another object.\n" +
                "Note that this is not required.")]
    public GameUnit onContactEffect;

    [Tooltip("Effect to instantiate upon reaching zero current health.\n" +
                "Note that this is not required.")]
    public GameUnit onDeathEffect;
    
    [Tooltip("Omni-directional movement speed.")]
    public float moveSpeed = 5;
    
    [Tooltip("The collision damage upon striking another GameUnit.")]
    public int damage = 10;
    
    [Tooltip("True means current health will not decrease.")]
    public bool invincible = false;

    [SerializeField]
    private int _currentHealth = 100;

    [SerializeField]
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
                _currentHealth = (value < 0) ? 0 : value;   // Disallow negatives.
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

    /// <summary>
    /// Instantiate <see cref="onContactEffect"/> at the position of the given GameUnit.
    /// <strong>Note that this only instantiate the effect and does nothing else.</strong>
    /// </summary>
    /// <param name="other">The GameUnit of which this object has collided with.</param>
    public void OnContactEffect(GameUnit other)
    {
        if (onContactEffect != null)
            Instantiate(onContactEffect, other.transform.position, other.transform.rotation);
    }

    /// <summary>
    /// Instantiate <see cref="onDeathEffect"/> at this object's location.
    /// <strong>Note that this does not destroy this current object.</strong>
    /// </summary>
    public void OnDeathEffect()
    {
        if (onDeathEffect != null)
            Instantiate(onDeathEffect, transform.position, transform.rotation);
    }

    /// <summary>
    /// Helper method to handle <see cref="OnTriggerEnter(Collider)"/> and
    /// <see cref="OnCollisionEnter(Collision collision)"/> as they share the same modifications.
    /// </summary>
    /// <param name="other">GameUnit that this object has collided with.</param>
    protected void OnTriggerCollisionEnter(GameUnit other)
    {
        if (other != null)
        {
            other.currentHealth -= damage;
        }
    }

    // *****************
    // 
    //  Private Unity Methods
    //
    // *****************

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerCollisionEnter(other.gameObject.GetComponent<GameUnit>());
    }

}
