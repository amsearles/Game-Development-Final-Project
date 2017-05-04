using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponV2 : MonoBehaviour {

    // ----------- Variables -----------

    public string weaponName = "Weapon_Name";

    [Tooltip("Drag the GameObject with the Projectile component here.")]
    public Projectile   projectile;

    [Tooltip("Drag the GameObjects representing the spawn locations for the projectiles here.")]
    public Transform[]  projectileSpawnLocations;

    [Tooltip("True = Fire projectiles from every projectile spawn location when able.\n" +
             "False = Fire one projectile at a time while cycling through each Weapon's projectile spawn point.")]
    public bool fireOnAllSpawns = true;

    [Tooltip("If inifinite ammo is true then ammo count will not decrease.")]
    public bool     infiniteAmmo = true;

    [Tooltip("The number of times this weapon can fire.")]
    public int      ammoCount = 10;

    [Tooltip("The delay in seconds between being able to fire.")]
    public float    rateOfFire = 3.0f;

    private float   nextFire = 0.0f;
    private int     projSpawnLocationIndex;


    // ----------- Methods -----------

    private void Start()
    {
        if (ammoCount < 0) ammoCount = 0;
        if (rateOfFire < 0f) rateOfFire = 0f;

        nextFire = 0f;
        projSpawnLocationIndex = 0;
    }

    public bool canFire()
    {
        return ((Time.time > nextFire)  && (ammoCount > 0 || infiniteAmmo) 
                                        && (projectileSpawnLocations.Length > 0));
    }

    /// <summary>Instantiate this Weapon's Projectile at the specified Transform position.</summary>
    /// <param name="spawnTransform">Projectile's spawn location.</param>
    public void Fire()
    {
        if (canFire())
        {
            Vector3 spawnPosition;
            Quaternion spawnRotation;

            // Set values for next projectile.
            if (ammoCount > 0 && !infiniteAmmo) ammoCount--;

            nextFire = rateOfFire + Time.time;

            // Spawn the projectile.
            if (fireOnAllSpawns)
            {
                Debug.Log("TODO: Resolve Projectile identify friend or foe.");
                foreach (Transform x in projectileSpawnLocations)
                {
                    spawnPosition = x.position;
                    spawnRotation = x.rotation;
                    
                    Instantiate(projectile, spawnPosition, spawnRotation);
                }
            }
            else
            {
                spawnPosition = projectileSpawnLocations[projSpawnLocationIndex].position;
                spawnRotation = projectileSpawnLocations[projSpawnLocationIndex].rotation;

                // Increment / loop the index.
                projSpawnLocationIndex = (projSpawnLocationIndex + 1) % projectileSpawnLocations.Length;

                Debug.Log("TODO: Resolve Projectile identify friend or foe.");
                Instantiate(projectile, spawnPosition, spawnRotation);
            }
            
        }
    }

    public Transform[] GetProjectileSpawnLocations()
    {
        return projectileSpawnLocations;
    }
    
}
