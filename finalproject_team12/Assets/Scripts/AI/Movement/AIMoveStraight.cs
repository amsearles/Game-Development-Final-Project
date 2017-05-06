using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameUnit))]
public class AIMoveStraight : MonoBehaviour {

    private GameUnit gameUnit;

    private void Start()
    {
        gameUnit = GetComponent<GameUnit>();
    }

    private void FixedUpdate()
    {
        Transform transform = GetComponent<Transform>();
        //transform.Translate(transform.forward * gameUnit.speed * Time.deltaTime);
        transform.position += transform.forward * gameUnit.moveSpeed * Time.deltaTime;

        //Rigidbody rigidbody = GetComponent<Rigidbody>();
        //rigidbody.MovePosition(transform.position + transform.forward * gameUnit.speed * Time.deltaTime);
    }

}
