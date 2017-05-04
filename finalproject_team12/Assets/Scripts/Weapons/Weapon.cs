using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class ProjectileSpawnPoints
{
    public GameObject[] projectileSpawnPoints;
}
public class Weapon : MonoBehaviour {

    public Projectile projectile;

    public string   weaponName = "Weapon_Name";
    public int      ammoCount = 0;
    public bool     infiniteAmmo = false;
    public float    rateOfFire = 0.3f;    
    
    private float nextFire;

    private void Start()
    {
        if (ammoCount < 0) ammoCount = 0;
        if (rateOfFire < 0f) rateOfFire = 0f;
        
        nextFire = 0f;
    }

    public void Fire(Transform spawnTransform)
    {
        if (canFire())
        {
            //NOTE to self: Refer to Unity tutorial on Object Pooling to see better management.
            
            Vector3 spawnPosition = spawnTransform.position;
            Quaternion spawnRotation = spawnTransform.rotation;
            
            //Set values for next projectile.
            if (ammoCount > 0) ammoCount--;
            nextFire = rateOfFire + Time.time;
            
            //Spawn the projectile.
            Instantiate(projectile, spawnPosition, spawnRotation); 
        }
    }

    public bool canFire()
    {
        return ((Time.time > nextFire) && (ammoCount > 0 || infiniteAmmo)) ;
    }

}
