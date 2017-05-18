using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityPowerUp : PowerUp {

    public float invicibilityDuration = 5.0f;

    public override void HandlePickUp(PlayerUnit playerUnitComp)
    {
        HealthComponent playerHealthComp = playerUnitComp.healthComponent;

        if (playerHealthComp != null)
            playerHealthComp.SetInvincible(true, invicibilityDuration);
    }
    
}
