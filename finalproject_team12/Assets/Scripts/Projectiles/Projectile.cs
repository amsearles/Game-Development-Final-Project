using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/**
 * Jimmy He
 * CSC631
 * Team12
 * Final Project
 */

[RequireComponent(typeof(MoveSpeedComponent))]
[RequireComponent(typeof(DamageComponent))]
[DisallowMultipleComponent]
public abstract class Projectile : MonoBehaviour
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

    protected string _ownerTag = "";
    protected MoveSpeedComponent _moveSpeedComp;
    protected DamageComponent _damageComp;

    // *****************
    // 
    //  Properties
    //  Saved as quick references to avoid GetComponent every time.
    //
    // *****************

    /// <summary> 
    /// Owner who fired this Projectile. Used for collision testing. 
    /// </summary>
    public string ownerTag
    {
        get { return _ownerTag; }
        set { _ownerTag = value; }
    }

    public MoveSpeedComponent moveSpeedComponent
    {
        get
        {
            if (_moveSpeedComp == null)
                _moveSpeedComp = GetComponent<MoveSpeedComponent>();
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

    public void MoveFoward()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (rigidbody != null)
            rigidbody.velocity = transform.forward * moveSpeedComponent.speed * Time.deltaTime;
        else
            transform.Translate(transform.forward * moveSpeedComponent.speed * Time.deltaTime, Space.World);

    }

    // *****************
    // 
    //  Unity Methods
    //
    // *****************

    private void OnTriggerEnter(Collider other)
    {
        // Identify who fired this Projectile by its Tag name.
        bool isOwnerPlayer = ownerTag.Equals(Tags.Player);  //true
        bool isOwnerEnemy = ownerTag.Equals(Tags.Enemy);    //false
        
        
        // Deal damage to opposing faction.
        if (isOwnerPlayer || isOwnerEnemy)
        {
            // Get Tag name of 'other'.
            string otherTagName = other.tag;

            // If this Projectile is owned by Player and has struck a Enemy or vice versa.
            if ((isOwnerPlayer && otherTagName.Equals(Tags.Enemy))
                    || (isOwnerEnemy && otherTagName.Equals(Tags.Player)))
            {
                // Climb up the object to its root object find GameUnit in children.
                GameUnit collidedGameUnit = other.transform.root.GetComponentInChildren<GameUnit>();

                //Debug.Log("isOwnerPlayer: " + isOwnerPlayer + " and otherTagName is: " + otherTagName);
                if (collidedGameUnit != null)
                    collidedGameUnit.TakeDamage(damageComponent.damage);

                if (onDestructionEffect != null)
                    Instantiate(onDestructionEffect, transform.position, transform.rotation);

                Destroy(gameObject);
            }

        }
    }

}
