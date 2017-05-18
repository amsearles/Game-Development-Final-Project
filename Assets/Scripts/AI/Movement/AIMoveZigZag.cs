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
public class AIMoveZigZag : MonoBehaviour {

    // *****************
    // 
    //  Variables
    //
    // *****************

    [Tooltip("The speed of moving left and right.\n" +
                "That is, the value going between 0 and the given wave height.")]
    public float zigzagFrequency = 3f;

    [Tooltip("The distance of moving to either left or right followed by the same distance in the opposite direction.\n" +
                "That is, the value of which frequency must reach before moving in the opposite direction")]
    public float zigzagWaveHeight = 5f;

    private MoveSpeedComponent moveSpeedComp;


    // *****************
    // 
    //  Unity Methods
    //
    // *****************

    // Use this for initialization
    void Start () {
        moveSpeedComp = GetComponent<MoveSpeedComponent>();

        zigzagWaveHeight = Mathf.Abs(zigzagWaveHeight);
	}

    private void FixedUpdate()
    {
        Transform transform = GetComponent<Transform>();
        
        // Determine the current oscillating value between -1*(zigzagHeight/2) and zigzagHeight/2
        float pingpong = Mathf.PingPong(Time.time * zigzagFrequency, 2*zigzagWaveHeight) - zigzagWaveHeight;

        // Move the object via its Right direction axis.
        transform.position += transform.right * pingpong * Time.deltaTime;

        // Move the object via its Forward direction axis.
        transform.position += transform.forward * moveSpeedComp.speed * Time.deltaTime;

        //Rigidbody rigidbody = GetComponent<Rigidbody>();
        //rigidbody.MovePosition(transform.position + transform.right * pingpong * Time.deltaTime);
        //rigidbody.MovePosition(transform.position + transform.forward * gameUnit.speed * Time.deltaTime);

    }


    /*  // Couldn't solve this leaving it for maybe later. It is suppose to zig zig by exact distance.
    private void ZigZagInDistance()
    {
        Transform transform = GetComponent<Transform>();

        // Determine the current oscillating value between maxLeftDistance and maxRightDistance
        //float pingpong = Mathf.PingPong(Time.time, zigzagDistance) - maxLeftMovement;
        float pingpong = zigzagSpeed * Time.deltaTime;

        // Determine how much X and Z axis direction the pingpong value translates to in World Space.
        float temp1 = Vector3.Angle(transform.right, Vector3.up);
        float temp2 = Vector3.Angle(transform.right, Vector3.down);
        float degrees = (temp1 <= 90f) ? temp1 : temp2;
        float newPosX = Mathf.Abs(Mathf.Cos(degrees) * pingpong);      //A = Cos(degrees) * H
        float newPosZ = Mathf.Abs(Mathf.Sin(degrees) * pingpong);      //O = Sin(degrees) * H

        // Fix new X and Z position values to proper positives and negatives.
        

        Debug.Log("New X: " + newPosX + "New Z: " + newPosZ);
        // Set this object to new position
        Vector3 newPosition = new Vector3(newPosX, 0.0f, newPosZ);
        transform.position += newPosition;


        Debug.Log("Translate:right = " + transform.right);
        Debug.Log("Right Angle: " + degrees);
        
        //transform.position += transform.right * pingpong * Time.deltaTime;
        
    }
    */

}
