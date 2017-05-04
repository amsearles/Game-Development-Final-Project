using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameUnitShip))]
public class AIMoveZigZag : MonoBehaviour {

    [Tooltip("Speed of moving left and right.")]
    public float zigzagSpeed = 0.3f;
    [Tooltip("Duration in SECONDS of far left and right the object will go.")]
    public float zigzagWaveHeight = 1.5f;

    private GameUnitShip gameUnit;

	// Use this for initialization
	void Start () {
        gameUnit = GetComponent<GameUnitShip>();

        zigzagWaveHeight = Mathf.Abs(zigzagWaveHeight);
	}

    private void FixedUpdate()
    {
        Transform transform = GetComponent<Transform>();
        
        // Determine the current oscillating value between -1*(zigzagHeight/2) and zigzagHeight/2
        float pingpong = Mathf.PingPong(Time.time * zigzagSpeed, zigzagWaveHeight) - (zigzagWaveHeight / 2.0f);

        // Move the object via its Right direction axis.
        transform.Translate(transform.right * pingpong * Time.deltaTime);

        // Move the object via its Forward direction axis.
        transform.Translate(transform.forward * gameUnit.speed * Time.deltaTime);
        
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
