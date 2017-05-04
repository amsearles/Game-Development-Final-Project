using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMissile : Projectile {

    public float rotateSpeed = 120f;
    public float trackingRadius = 30f;
    public float trackingDuration = 3f; //seconds
    public GameObject trackedTarget;
    
    private void FixedUpdate()
    {
        MoveFoward();
        RotateToward(trackedTarget);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isEnemy(collision.gameObject))
        {
            //TODO: Handle health decrease upon contact.
            Debug.Log("Projectile class requires handling health decrease upon OnCollisionEnter()");

            DieAndEffect();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isEnemy(other.gameObject))
        {
            //TODO: Handle health decrease upon contact.
            Debug.Log("Projectile class requires handling health decrease upon OnTriggerEnter()");

            DieAndEffect();
        }
    }

    public void RotateToward(GameObject target)
    {
        RotateToward(target.transform.position);
    }

    public void RotateToward(Vector3 position)
    {
        Quaternion direction = Quaternion.LookRotation(position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, direction, rotateSpeed * Time.deltaTime);
    }

}
