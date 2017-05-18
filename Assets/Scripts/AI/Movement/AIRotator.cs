using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AIRotator simply rotates the object about itself in random fashion.
/// </summary>
[RequireComponent(typeof(RotateSpeedComponent))]
public class AIRotator : MonoBehaviour {

    // *****************
    // 
    //  Variables
    //
    // *****************

    [Tooltip("Random rotation range around the X axis between min and max degrees. "
                + "Setting the range's two values equal will stop the rotation about this axis.")]
    public Range rotateAroundX;
    [Tooltip("Random rotation range around the Y axis between min and max degrees. "
                + "Setting the range to 0.0 to 0.0 will stop this the rotation about this axis.")]
    public Range rotateAroundY;
    [Tooltip("Random rotation range around the Z axis between min and max degrees. "
                + "Setting the range to 0.0 to 0.0 will stop this the rotation about this axis.")]
    public Range rotateAroundZ;

    /**** Private Variables ****/
    private Quaternion targetRotation;
    private RotateSpeedComponent rotateSpeedComponent;


    // *****************
    // 
    //  Unity Methods
    //
    // *****************

    private void Start()
    {
        targetRotation = GetRandomQuaternion();

        //Debug.Log("Target Rotation: " + targetRotation);
        rotateSpeedComponent = GetComponent<RotateSpeedComponent>();
    }

    // Update is called once per frame
    private void Update()
    {
        /*
         *  Quaternion values (x,y,z,w) range from 0 to 1 and is too precise to compare.
         *  Thus it is required to convert to angles for comparisons.
         */

        // Get a random direction if current rotation has reached the targeted rotation.
        if (Quaternion.Angle(transform.rotation, targetRotation) < 10.0f)
            targetRotation = GetRandomQuaternion();
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                                                    rotateSpeedComponent.speed * Time.deltaTime);

    }

    private Quaternion GetRandomQuaternion()
    {

        Vector3 _targetRotation = transform.rotation.eulerAngles;

        if (rotateAroundX.min != rotateAroundX.max)
            _targetRotation.x = Random.Range(rotateAroundX.min, rotateAroundX.max);

        if (rotateAroundY.min != rotateAroundY.max)
            _targetRotation.y = Random.Range(rotateAroundY.min, rotateAroundY.max);

        if (rotateAroundZ.min != rotateAroundZ.max)
            _targetRotation.z = Random.Range(rotateAroundZ.min, rotateAroundZ.max);

        Debug.Log(_targetRotation);

        return Quaternion.Euler(_targetRotation);
    }


    // *****************
    // 
    //  Public Class
    //
    // *****************

    [System.Serializable]
    public class Range
    {
        [SerializeField]
        private float _min = 10.0f;
        [SerializeField]
        public float _max = 20.0f;

        public float min
        {
            get { return _min; }
            set { _min = (value % 360) / 360; }
        }

        public float max
        {
            get { return _max; }
            set { _max = (value % 360) / 360; }
        }

    }

}
