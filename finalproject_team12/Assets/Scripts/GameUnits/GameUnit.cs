using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Jimmy He & Elric Dang
 * CSC631
 * Team12
 * Final Project
 */

///<summary>
///GameUnit are essentially the main units of our game, which are planes.
///<remarks>I suppose naming this to Planes or Unit would be more appropriate.</remarks>
///</summary>

[RequireComponent(typeof(HealthComponent))]
[RequireComponent(typeof(MoveSpeedComponent))]
[RequireComponent(typeof(DamageComponent))]
[DisallowMultipleComponent]
public abstract class GameUnit : MonoBehaviour
{

    // *****************
    // 
    //  Variables
    //
    // *****************

    /// <summary>
    /// Effect to instantiate upon destruction of this Projectile.
    /// </summary>
    [Tooltip("The GameObject to instantiate upon destruction of this Projectile.")]
    public GameObject onDestructionEffect;

    private HealthComponent _healthComp;
    private MoveSpeedComponent _moveSpeedComp;
    private DamageComponent _damageComp;

    // *****************
    // 
    //  Properties
    //  Saved as quick references to avoid GetComponent every time.
    //
    // *****************
    
    public HealthComponent healthComponent
    {
        get
        {
            if (_healthComp == null)
                _healthComp = GetComponent<HealthComponent>();
            return _healthComp;
        }
    }

    public MoveSpeedComponent moveSpeedComponent
    {
        get
        {
            if (_moveSpeedComp == null)
                _moveSpeedComp = GetComponent < MoveSpeedComponent>();
            return _moveSpeedComp;
        }
    }

    public DamageComponent damageComponent
    {
        get
        {
            if (_damageComp == null)
                _damageComp = GetComponent<DamageComponent>();
            return _damageComp;
        }
    }
    

    // *****************
    // 
    //  Public Methods
    //
    // *****************

    /// <summary>
    /// Take damage by subtracting the given damage from health component.
    /// It will instantiate the <see cref="onDestructionEffect"/> when
    /// this object's health reaching zero. Afterward this object is Destroyed.
    /// Override it if necessary.
    /// </summary>
    /// <param name="damage">Amount to subtract from HealthComponent</param>
    public virtual void TakeDamage(int damage)
    {
        healthComponent.currentHealth -= damage;

        if (healthComponent.currentHealth <= 0)
            Die();

    }

    /// <summary>
    /// Instantiate the given GameObject in <see cref="onDestructionEffect"/> and
    /// then destroy this current object.
    /// </summary>
    public virtual void Die()
    {
        if (onDestructionEffect != null)
        {
            Instantiate(onDestructionEffect, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }

}
