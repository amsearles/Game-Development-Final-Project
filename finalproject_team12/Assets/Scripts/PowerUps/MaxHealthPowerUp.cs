using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthPowerUp : PowerUp
{
    public int increaseMaxHealth = 50;

    public override void HandlePickUp(PlayerUnit playerUnitComp)
    {
        HealthComponent playerHealthComp = playerUnitComp.healthComponent;

        if (playerHealthComp != null)
        {
            playerHealthComp.maxHealth += 50;
        }
    }

}
