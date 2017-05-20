using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveSpeedComponent))]
[RequireComponent(typeof(RotateSpeedComponent))]
public class AIMoveRotateAround : MonoBehaviour {
    
    public GameObject target;

    // *****************
    // 
    //  Variables
    //
    // *****************

    private MoveSpeedComponent moveSpeedComponent;
    private RotateSpeedComponent rotateSpeedComponent;

    // *****************
    // 
    //  Properties
    //
    // *****************

    public float moveSpeed
    {
        get { return moveSpeedComponent.speed;  }
        set { moveSpeedComponent.speed = value; }
    }

    public float rotateSpeed

    {
        get { return rotateSpeedComponent.speed; }
        set { rotateSpeedComponent.speed = value; }
    }


    // *****************
    // 
    //  Unity Methods
    //
    // *****************

    private void Awake()
    {
        moveSpeedComponent = GetComponent<MoveSpeedComponent>();
        rotateSpeedComponent = GetComponent<RotateSpeedComponent>();

        if (moveSpeedComponent == null)
            moveSpeedComponent = gameObject.AddComponent<MoveSpeedComponent>();

        if (rotateSpeedComponent == null)
            rotateSpeedComponent = gameObject.AddComponent<RotateSpeedComponent>();
    }

    // *****************
    // 
    //  Public Methods
    //
    // *****************

    /*
     * Note: This code is copy pasted from ProjectileMissile and should be placed 
     * somewhere appropriate and reusable.
     */
    public void MoveFoward()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (rigidbody != null)
            rigidbody.velocity = transform.forward * moveSpeed * Time.deltaTime;
        else
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
        
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



}
