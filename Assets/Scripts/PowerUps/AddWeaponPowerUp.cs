using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * Jimmy He
 * CSC631
 * Team12
 * Final Project
 */

/// <summary>
/// Adds a single new weapon from its given list of Weapons that the Player doesn't have yet.
/// </summary>
public class AddWeaponPowerUp : PowerUp {

    [Tooltip("List of Weapons that will be choosen to be mounted onto the player.\n "
                + "The first missing Weapon found that the Player doesn't have and that this list does, will be mounted to the Player.\n"
                + "Every time this PowerUp is picked up it will mount possibly ONE additional Weapon that the Player doesn't have yet.\n "
                + "This means that if the Player already has all the Weapons listed here then the pickup will give nothing.\n")]
    public List<Weapon> weaponList;

    
    public override void HandlePickUp(PlayerUnit playerUnitComp)
    {
        int weaponIndex = GetNextWeapon(playerUnitComp);

        if (weaponIndex != -1)
        {
            Transform otherRootObject = playerUnitComp.transform.root;
            Weapon newWeapon = Instantiate(weaponList[weaponIndex], otherRootObject.position, otherRootObject.rotation, otherRootObject);

            newWeapon.gameObject.AddComponent<AIFiringStraight>();  // Attach auto firing script.
        }
    }

    /// <summary>
    /// Helper method for <see cref="HandlePickUp(PlayerUnit)"/>.
    /// It determines what weapon in the Weapon list of this object that the Player doesn't have yet.
    /// </summary>
    /// <param name="playerUnit">The PlayerUnit that has collided with this PowerUp.</param>
    /// <returns></returns>
    private int GetNextWeapon(PlayerUnit playerUnit)
    {
        Weapon[] playerWeapons = playerUnit.GetComponentsInChildren<Weapon>();
        List<string> playerWeaponNames = new List<string>();

        // Put all the names of the weapons the Player currently has into a list.
        foreach (Weapon x in playerWeapons)
            playerWeaponNames.Add(x.weaponName);

        // Get the index of the first weapon that the Player doesn't have yet.
        for (int i = 0; i < weaponList.Count; i++)
            if (!playerWeaponNames.Contains(weaponList[i].weaponName))
                return i;

        return -1;
    }

}
