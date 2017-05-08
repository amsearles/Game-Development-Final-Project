using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Jimmy He
 * CSC631
 * Team12
 * Final Project
 */

public class ProjectileBullet : Projectile
{

    // *****************
    // 
    //  Unity Methods
    //
    // *****************
    
    private void FixedUpdate()
    {
        MoveFoward();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (ownerTag.Equals(Tags.Player))
        {
            if (other.CompareTag(Tags.Enemy))
            {
                
            }
        }
        else if (ownerTag.Equals(Tags.Enemy))
        {

        }
        else
        {
            throw new System.Exception("This Projectile requires a ownerTag name");
        }
    }

}
