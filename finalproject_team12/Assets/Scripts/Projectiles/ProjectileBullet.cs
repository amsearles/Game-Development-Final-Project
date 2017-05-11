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
    
}
