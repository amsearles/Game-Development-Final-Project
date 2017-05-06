using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Jimmy He
 * CSC631
 * Team12
 * Final Project
 */

public class ProjectileMissile : GameUnit
{
    // *****************
    // 
    //  Variables
    //
    // *****************

    // TODO : This goes in WeaponMissileLauncher
    [Tooltip("Ignore this layer scaning for targets.")]
    public LayerMask targetedLayers;

    [Tooltip("Speed of the rotation in degrees")]
    public float rotateSpeed = 120f;

    [Tooltip("Provide values to turn this into a homing missile.")]
    public TrackingInfo trackingTargetInfo;
    
    // Elapsed time of tracking duration.
    private float elapsedTime = 0.0f;


    // *****************
    // 
    //  Private Methods
    //
    // *****************

    private void MoveFoward()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private bool IsTargetInRange(GameUnit target)
    {
        float degrees = Vector3.Angle(  transform.forward,
                                        target.transform.position - transform.position);

        return (trackingTargetInfo.trackingAngle > degrees);
    }

    private void RotateToward(GameUnit target)
    {
        RotateToward(target.transform.position);
    }

    private void RotateToward(Vector3 position)
    {
        Quaternion direction = Quaternion.LookRotation(position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, direction, rotateSpeed * Time.deltaTime);
    }
    
    // *****************
    // 
    //  Private Unity Methods
    //
    // *****************

    private void Start()
    {
        rotateSpeed = Mathf.Abs(rotateSpeed);

        if (trackingTargetInfo.trackedTarget != null)
        {
            trackingTargetInfo.trackingAngle = Mathf.Abs(trackingTargetInfo.trackingAngle);
            trackingTargetInfo.trackingDuration = Mathf.Abs(trackingTargetInfo.trackingDuration);
        }

    }

    private void FixedUpdate()
    {
        MoveFoward();

        if (trackingTargetInfo.trackedTarget != null)
        {
            if (IsTargetInRange(trackingTargetInfo.trackedTarget)
                        && (elapsedTime < trackingTargetInfo.trackingDuration))
            {
                RotateToward(trackingTargetInfo.trackedTarget);
            }
        }

        elapsedTime += Time.deltaTime;
    }


    // *****************
    // 
    //  Public Classes
    //
    // *****************

    public class TrackingInfo
    {
        [Tooltip("Target to follow. This turns this projectile into a homing missile.")]
        public GameUnit trackedTarget;

        [Tooltip("Maximum degrees from this object's forward position until it can no longer track the target.")]
        public float trackingAngle = 45f;

        [Tooltip("Number of seconds to spend following a target.")]
        public float trackingDuration = 3f;
        
        //public bool IsTrackedTargetInRange(Transform trackingUnit)
        //{
        //    bool isInRange = false;

        //    Collider[] targetsInRange = Physics.OverlapBox(trackingUnit.position,
        //                                                    new Vector3(trackingWidth,
        //                                                                0.0f,
        //                                                                trackingWidth),
        //                                                    trackingUnit.rotation);

        //    foreach (Collider x in targetsInRange)
        //    {
        //        GameUnit gameUnit = x.GetComponent<GameUnit>();

        //        if (gameUnit = trackedTarget)
        //        {
        //            isInRange = true;
        //            break;
        //        }
        //    }

        //    return isInRange;
        //}

    }

}
