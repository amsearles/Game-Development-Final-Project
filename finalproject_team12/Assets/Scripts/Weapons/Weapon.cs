using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/**
 * Jimmy He
 * CSC631
 * Team12
 * Final Project
 */

/// <summary>
/// Weapon is used for spawning Projectile objects. Weapon handles the Projectile
/// spawning locations, firing rate, and instantiation.
/// </summary>
public class Weapon : MonoBehaviour {

    // -----------
    //
    // Variables
    //  
    // -----------

    public string weaponName = "Weapon_Name";

    [Tooltip("Drag the GameObject with the Projectile component here.")]
    public ProjectileBullet   projectile;

    [Tooltip("Drag the GameObjects representing the spawn locations for the projectiles here.")]
    public List<Transform>  projectileSpawnLocations;

    [Tooltip("True = Fire projectiles from every given projectile spawn location when able.\n" +
             "False = Fire one projectile at a time while cycling through each given projectile spawn location.")]
    public bool fireOnAllSpawns = true;

    [SerializeField]
    [Tooltip("Fire projectiles in set bursts and then wait.")]
    BurstFire burstFire;

    [Tooltip("If inifinite ammo is true then ammo count will not decrease upon firing.")]
    public bool     infiniteAmmo = true;

    [Tooltip("The number of times this weapon can fire.\n" +
                "Disregard this if infinite ammo is set to true.")]
    public int      ammoCount = 10;

    [Tooltip("The delay in seconds between firing projectiles.")]
    public float    rateOfFire = 3.0f;

    private float   nextFire = 0.0f;
    private int     projSpawnLocationIndex;


    // -----------
    //
    // Public Methods
    //  
    // -----------

    public List<Transform> GetProjectileSpawnLocations()
    {
        return projectileSpawnLocations;
    }

    public bool canFire()
    {
        return ((Time.time > nextFire)  && (ammoCount > 0 || infiniteAmmo) 
                                        && (projectileSpawnLocations.Count > 0));
    }

    /// <summary>Instantiate this Weapon's Projectile at the specified Transform position.</summary>
    /// <param name="spawnTransform">Projectile's spawn location.</param>
    public void Fire()
    {
        if (canFire())
        {
            // Set values for next projectile.
            if (ammoCount > 0 && !infiniteAmmo)
                ammoCount--;

            Fire_HandleFiringRate();

            Fire_HandleFiringProjectiles();
        }
    }

    // -----------
    //
    // Private Unity Methods
    //  
    // -----------

    /// <summary>
    /// Helper method to <see cref="Fire()"/>. 
    /// It handles the fire rate and timing the next available shot.
    /// </summary>
    private void Fire_HandleFiringRate()
    {
        // Burst Fire salvo counter greater than zero implies YES to use burst fire.
        // Determine whether to set next time of firing by burst fire delay or by rate of fire.
        if (burstFire.salvoCount > 0)
        {

            burstFire.currentSalvoCount++;

            if (burstFire.currentSalvoCount == 0)
                nextFire = burstFire.afterSalvoDelay + Time.time;
            else
                nextFire = rateOfFire + Time.time;

        }
        else
        {
            nextFire = rateOfFire + Time.time;
        }
    }

    /// <summary>
    /// Helper method to <see cref="Fire()"/>.
    /// It handles instantiating the Projectiles at the given spawn locations.
    /// </summary>
    private void Fire_HandleFiringProjectiles()
    {
        Vector3 spawnPosition;
        Quaternion spawnRotation;

        // Spawn the projectile.
        if (fireOnAllSpawns)
        {
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
            projSpawnLocationIndex = (projSpawnLocationIndex + 1) % projectileSpawnLocations.Count;

            Instantiate(projectile, spawnPosition, spawnRotation);
        }
    }

    // -----------
    //
    // Private Unity Methods
    //  
    // -----------

    private void Start()
    {
        if (ammoCount < 0) ammoCount = 0;
        if (rateOfFire < 0f) rateOfFire = 0f;
        if (burstFire.afterSalvoDelay < rateOfFire)
            burstFire.afterSalvoDelay = rateOfFire;

        nextFire = 0f;
        projSpawnLocationIndex = 0;
    }

    // -----------
    //
    // Helper Class
    //  
    // -----------

    [Serializable]
    private class BurstFire
    {
        [Tooltip("Fire off this number of times in succession.\n" +
                    "Note that zero indicates NOT to use burst fire.")]
        public int salvoCount = 0;
        [Tooltip("Delay in seconds until next salvo can be fired.")]
        public float afterSalvoDelay = 3.0f;

        public int currentSalvoCount
        {
            get { return _currentSalvoCount; }
            set { _currentSalvoCount = value % salvoCount; }
        }

        private int _currentSalvoCount = 0;
    }
    
}
