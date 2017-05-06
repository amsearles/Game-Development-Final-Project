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
    [Tooltip("Speed of the rotation in degrees")]
    public float rotateSpeed = 120f;

    [Tooltip("Scanning radius to find a potential target.")]
    public float trackingRadius = 30f;

    [Tooltip("Number of seconds to spend following a target.")]
    public float trackingDuration = 3f;

    [Tooltip("Target to follow.")]
    public GameUnit trackedTarget;
    
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

    private void RotateToward(GameUnit target)
    {
        RotateToward(target.transform.position);
    }

    private void RotateToward(Vector3 position)
    {
        Quaternion direction = Quaternion.LookRotation(position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, direction, rotateSpeed * Time.deltaTime);
    }

    private GameUnit ScanForTargets()
    {
        GameUnit targetedUnit = null;

        if (trackingRadius > 0 && trackingDuration > 0)
        {

        }

        return targetedUnit;
    }

    // *****************
    // 
    //  Private Unity Methods
    //
    // *****************

    private void Start()
    {
        rotateSpeed = Mathf.Abs(rotateSpeed);
        trackingRadius = Mathf.Abs(trackingRadius);
        if (trackingDuration < 0f) trackingDuration = 0.0f;
    }

    private void FixedUpdate()
    {
        MoveFoward();
        
        RotateToward(trackedTarget);
    }

}
