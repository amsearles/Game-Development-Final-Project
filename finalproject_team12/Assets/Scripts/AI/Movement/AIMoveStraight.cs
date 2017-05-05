using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameUnitShip))]
public class AIMoveStraight : MonoBehaviour {

    private GameUnitShip gameUnit;

    private void Start()
    {
        gameUnit = GetComponent<GameUnitShip>();
    }

    private void FixedUpdate()
    {
        Transform transform = GetComponent<Transform>();
        //transform.Translate(transform.forward * gameUnit.speed * Time.deltaTime);
        transform.position += transform.forward * gameUnit.speed * Time.deltaTime;

        //Rigidbody rigidbody = GetComponent<Rigidbody>();
        //rigidbody.MovePosition(transform.position + transform.forward * gameUnit.speed * Time.deltaTime);
    }

}
