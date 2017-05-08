using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Jimmy He
 * CSC631
 * Team12
 * Final Project
 */

[RequireComponent(typeof(MoveSpeedComponent))]
public class AIMoveStraight : MonoBehaviour {

    // *****************
    // 
    //  Variables
    //
    // *****************

    private MoveSpeedComponent moveSpeedComp;

    // *****************
    // 
    //  Unity Methods
    //
    // *****************

    private void Start()
    {
        moveSpeedComp = GetComponent<MoveSpeedComponent>();
    }

    private void FixedUpdate()
    {
        Transform transform = GetComponent<Transform>();
        //transform.Translate(transform.forward * moveSpeedComp.speed * Time.deltaTime);
        transform.position += transform.forward * moveSpeedComp.speed * Time.deltaTime;

        //Rigidbody rigidbody = GetComponent<Rigidbody>();
        //rigidbody.MovePosition(transform.position + transform.forward * gameUnit.speed * Time.deltaTime);
    }

}
