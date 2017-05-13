using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Jimmy He
 * CSC631
 * Team12
 * Final Project
 */

[RequireComponent(typeof(RotateSpeedComponent))]
public class ProjectileMissile : Projectile
{
    // *****************
    // 
    //  Variables
    //
    // *****************

    [Tooltip("Provide values to turn this into a homing missile.")]
    public TrackingInfo trackingTargetInfo;
    // **** Private Variables ****
    // Speed of rotation when tracking a target.
    private RotateSpeedComponent _rotateSpeedComponent;
    // Elapsed time of tracking duration.
    private float elapsedTime = 0.0f;

    // *****************
    // 
    //  Properties
    //
    // *****************

    public RotateSpeedComponent rotateSpeedComp
    {
        get
        {
            if (_rotateSpeedComponent == null)
                _rotateSpeedComponent = GetComponent<RotateSpeedComponent>();
            return _rotateSpeedComponent;
        }
    }


    // *****************
    // 
    //  Private Methods
    //
    // *****************

    private bool IsTargetInRange(GameUnit target)
    {
        float degrees = Vector3.Angle(transform.forward,
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
        transform.rotation = Quaternion.RotateTowards(transform.rotation, direction, rotateSpeedComp.speed * Time.deltaTime);
    }

    // *****************
    // 
    //  Private Unity Methods
    //
    // *****************

    private void Start()
    {
        if (trackingTargetInfo.trackedTarget != null)
        {
            trackingTargetInfo.trackingAngle = Mathf.Abs(trackingTargetInfo.trackingAngle);
            trackingTargetInfo.trackingDuration = Mathf.Abs(trackingTargetInfo.trackingDuration);
        }

    }

    private void Update()
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

    [System.Serializable]
    public class TrackingInfo
    {
        [Tooltip("Target to follow. This turns this projectile into a homing missile.")]
        public GameUnit trackedTarget;

        [Tooltip("Maximum degrees from this object's forward position until it can no longer track the target.")]
        public float trackingAngle = 45f;

        [Tooltip("Number of seconds to spend following a target.")]
        public float trackingDuration = 3f;

        // Uses OverlapBox, unsure if necessary. Keeping this here.
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
