using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : PowerUp
{

    public int increaseHealth;

    public override void HandlePickUp(PlayerUnit playerUnitComp)
    {
        playerUnitComp.healthComponent.currentHealth += increaseHealth;
    }
    
}
