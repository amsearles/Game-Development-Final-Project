using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyUnit : GameUnit {

    // *****************
    // 
    //  Private Methods
    //
    // *****************

    /// <summary>Handler method for dealing with trigger/collisions between Player and Enemy.</summary>
    /// <param name="other">Object struck on impact.</param>
    private void OnContactEnter(GameObject other)
    {
        if (other.CompareTag(Tags.Player))  // Target Player directly.
        {
            PlayerUnit player = other.GetComponent<PlayerUnit>();
            if (player != null)
            {
                player.TakeDamage(damageComponent.damage);
            }
        }
    }

    // *****************
    // 
    //  Unity Methods
    //
    // *****************

    private void OnCollisionEnter(Collision collision)
    {
        OnContactEnter(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnContactEnter(other.gameObject);
    }

}
