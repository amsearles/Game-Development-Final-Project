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

    /// <summary>
    /// Handler method for dealing with trigger/collisions between Player and Enemy.
    /// Enemy and Player collisions are both dealt here.
    /// </summary>
    /// <param name="other">Object struck on impact.</param>
    private void OnContactEnter(GameObject other)
    {
        if (other.CompareTag(Tags.Player))  // Target Player directly.
        {
            PlayerUnit player = other.transform.root.GetComponentInChildren<PlayerUnit>();
            if (player != null)
            {
                // Damage dealt is the amount of health remaining on this Game Unit.
                int playerCurrentHealth = player.healthComponent.currentHealth;

                player.TakeDamage(healthComponent.currentHealth);
                this.TakeDamage(playerCurrentHealth);
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
