using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponV2))]
public class AIFiringStraight : MonoBehaviour {

    
    private WeaponV2 weapon;                            // Weapon to use in firing and manipulating.
    //private Transform[] projectileSpawnLocations;       // Projectile Spawn Locations from Weapon.
    //private int projSpawnLocationIndex = 0;             // Iterates each Projectile spawn location if fireOnAllPorts is false;

	// Use this for initialization
	void Start () {
        weapon = GetComponent<WeaponV2>();
        //projectileSpawnLocations = weapon.GetProjectileSpawnLocations();

        //Debug.Log(projectileSpawnLocations.Length);
	}
	
	// Update is called once per frame
	void Update () {

        // If this ship has a weapon and have firing ports to spawn the projectiles...
        if (weapon != null)
        {
            weapon.Fire();
        }
    }

}
