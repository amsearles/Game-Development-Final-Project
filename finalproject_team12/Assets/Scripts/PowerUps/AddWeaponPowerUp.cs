using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddWeaponPowerUp : PowerUp {

    [Tooltip("List of Weapons that will be choosen to be mounted onto the player.\n "
                + "The first missing Weapon found that the Player doesn't have and that this list does, will be mounted to the Player.\n"
                + "Every time this game object is picked up it will mount only ONE additional unique Weapon to the Player.\n "
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

    private int GetNextWeapon(PlayerUnit playerUnit)
    {
        Weapon[] currentWeapons = playerUnit.GetComponentsInChildren<Weapon>();

        foreach (Weapon x in currentWeapons)
            for (int i = 0; i < weaponList.Count; i++)
                if (!x.name.Equals(weaponList[i].name))
                    return i;

        return -1;
    }

}
