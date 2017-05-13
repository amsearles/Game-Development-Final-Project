using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Jimmy He
 * CSC631
 * Team12
 * Final Project
 */

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class WeaponLockOnMissileLauncher : Weapon
{
    // ***************************************************
    //
    //              Variables
    //  
    // ***************************************************

    public bool isHomingMissile = true;
    public List<GameUnit> targetedUnitList;

    /// <summary>Check if the Projectile this Weapon has is indeed a ProjectileMissile.</summary>
    private bool hasMissile = false;
    private float triggerTimeDelay = 0.3f;
    private CapsuleCollider targetDetection;


    // ***************************************************
    //
    //              Private/Protected Methods
    //  
    // ***************************************************

    protected override Projectile Fire_HandleInstantiateProjectile(Transform spawnLocation)
    {
        ProjectileMissile firedProjectile;

        firedProjectile = (ProjectileMissile)base.Fire_HandleInstantiateProjectile(spawnLocation);

        if ((hasMissile) && (targetedUnitList.Count > 0))
        {
            GameUnit targetedUnit = targetedUnitList[0];
            firedProjectile.trackingTargetInfo.trackedTarget = targetedUnit;

            targetedUnitList.RemoveAt(0);
        }


        return firedProjectile;
    }

    private bool IsValidTarget(Transform otherRoot)
    {
        return ((this.ownerTag.Equals(Tags.Player) && otherRoot.CompareTag(Tags.Enemy))
                || (this.ownerTag.Equals(Tags.Enemy) && otherRoot.CompareTag(Tags.Player)));
    }

    private bool AddGameUnitToTargetList(GameUnit gameUnit)
    {
        bool result = false;

        if (gameUnit != null && !targetedUnitList.Contains(gameUnit))
        {
            result = true;
            targetedUnitList.Add(gameUnit);
        }

        return result;
    }

    // ***************************************************
    //
    //              Unity Methods
    //  
    // ***************************************************

    protected new void Start()
    {
        base.Start();

        targetedUnitList = new List<GameUnit>();
        targetDetection = GetComponent<CapsuleCollider>();

        // Ensuring these options are as they should be.
        targetDetection.isTrigger = true;

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        rigidbody.useGravity = false;
    }

    protected void OnValidate()
    {
        if (this.projectile.GetType() != typeof(ProjectileMissile))
            Debug.Log(this.projectile.ToString() + " is NOT a ProjectileMissile!");
        else
            hasMissile = true;
    }

    protected void OnTriggerEnter(Collider other)
    {
        
        Transform otherRoot = other.transform.root;

        if (hasMissile)
            if (IsValidTarget(otherRoot))
                AddGameUnitToTargetList(otherRoot.GetComponentInChildren<GameUnit>());

    }

    protected void OnTriggerStay(Collider other)
    {
        Transform otherRoot = other.transform.root;

        if (hasMissile && (Time.time > triggerTimeDelay))
        {
            if (IsValidTarget(otherRoot))
                AddGameUnitToTargetList(otherRoot.GetComponentInChildren<GameUnit>());

            triggerTimeDelay += Time.time;
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (hasMissile)
        {
            GameUnit otherGameUnit = other.transform.root.GetComponentInChildren<GameUnit>();

            // Remove GameUnit from targeted units list if they've exited range.
            if (otherGameUnit != null)
            {
                int index = targetedUnitList.IndexOf(otherGameUnit);

                if (index != -1)
                    targetedUnitList.RemoveAt(index);

            }
        }
    }
    
}
