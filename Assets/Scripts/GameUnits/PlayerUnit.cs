using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

/**
 * Jimmy He
 * CSC631
 * Team12
 * Final Project
 */

[RequireComponent(typeof(Rigidbody))]
public class PlayerUnit : GameUnit
{

    // **** Variables ****
    private GameController gc;
    private Square mapBoundaries;

    // *****************
    // 
    //  Unity Methods
    //
    // *****************

    private void Start()
    {
        GameObject gobj = GameObject.FindGameObjectWithTag(Tags.GameController);

        if (gobj == null)
            throw new Exception("MISSING GAMECONTROLLER IN SCENE!");
        else
            gc = gobj.GetComponentInChildren<GameController>();

        mapBoundaries = GetMapBoundary();
        //Debug.Log("LEFT:" + mapBoundaries.left);
        //Debug.Log("RIGHT:" + mapBoundaries.right);
        //Debug.Log("TOP:" + mapBoundaries.top);
        //Debug.Log("BOTTOM:" + mapBoundaries.bottom);
    }

    // **********************************
    // 
    //      Public/Protected Methods
    //
    // **********************************
    
    public void MoveToward(Vector3 newPosition)
    {
        transform.position = Vector3.Lerp(transform.position, newPosition, moveSpeedComponent.speed * Time.deltaTime);
    }
    

    // *****************
    // 
    //  Private Methods
    //
    // *****************
    
    /// <summary>
    /// Get the appropriate screen boundaries  to constrain the Player to screen/ level.
    /// </summary>
    /// <returns><see cref="Square"/></returns>
    private Square GetMapBoundary()
    {
        Square appropriateSize = new Square();
        Square getScreenSizeSquare = GetScreenSize();
        Square getMapBoundarySquare = GetMapColliderBounds();
        
        if (getScreenSizeSquare == null) return getMapBoundarySquare;
        if (getMapBoundarySquare == null) return getScreenSizeSquare;

        // Determine which values (x,y,z) are smaller and to clamp on those values.
        if (getScreenSizeSquare.left < getMapBoundarySquare.left)
            appropriateSize.left = getMapBoundarySquare.left;
        else
            appropriateSize.left = getScreenSizeSquare.left;

        if (getScreenSizeSquare.right > getMapBoundarySquare.right)
            appropriateSize.right = getMapBoundarySquare.right;
        else
            appropriateSize.right = getScreenSizeSquare.right;

        if (getScreenSizeSquare.top < getMapBoundarySquare.top)
            appropriateSize.top = getScreenSizeSquare.top;
        else
            appropriateSize.top = getMapBoundarySquare.top;

        if (getScreenSizeSquare.bottom > getMapBoundarySquare.bottom)
            appropriateSize.bottom = getScreenSizeSquare.bottom;
        else
            appropriateSize.bottom = getMapBoundarySquare.bottom;

        return appropriateSize;
    }

    private Square GetScreenSize()
    {

        Square screenBoundaries = new Square();

        var dist = (transform.position.y - Camera.main.transform.position.y);

        var rightLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var leftLimitation = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var upLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).z;
        var downLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).z;

        screenBoundaries.left = leftLimitation;
        screenBoundaries.right = rightLimitation;
        screenBoundaries.top = upLimitation;
        screenBoundaries.bottom = downLimitation;
        
        return screenBoundaries;
    }

    private Square GetMapColliderBounds()
    {
        GameObject gameObj = GameObject.FindGameObjectWithTag(Tags.Boundary);
        BoxCollider boxBoundary = null;
        Square mapBoundary = new Square();

        if (gameObj != null)
            boxBoundary = gameObj.GetComponent<BoxCollider>();

        if (boxBoundary != null)
        {
            Bounds bounds = boxBoundary.bounds;
            Vector3 center = bounds.center;

            mapBoundary.left = center.x - bounds.extents.x;
            mapBoundary.right = center.x + bounds.extents.x;
            mapBoundary.top = center.z + bounds.extents.z;
            mapBoundary.bottom = center.z - bounds.extents.z;
            
        }

        return mapBoundary;
    }
    

    private Vector3 ConstrainToBoundaries(Vector3 position, Square boundaries)
    {
        Vector3 currentPosition = new Vector3(0.0f, 0.0f, 0.0f);

        currentPosition.x = Mathf.Clamp(position.x, boundaries.left, boundaries.right);
        currentPosition.z = Mathf.Clamp(position.z, boundaries.bottom, boundaries.top);

        return currentPosition;
    }

    // *****************
    // 
    //  Unity Methods
    //
    // *****************

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.transform.position.y;  //Camera is at an Y offset from 0.
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            
            MoveToward(mousePos);
            transform.position = ConstrainToBoundaries(transform.position, mapBoundaries);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnContactEnter(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnContactEnter(other.gameObject);
    }

    /// <summary>Handler method for dealing with trigger/collisions between Player and Enemy.</summary>
    /// <param name="other">Object struck on impact.</param>
    private void OnContactEnter(GameObject other)
    {
        if (other.CompareTag(Tags.Enemy))
        {
            // EnemyUnit enemyUnit = other.transform.root.GetComponentInChildren<EnemyUnit>();
            /*
             * EnemyUnit will deal with both Player and Enemy health reduction
             * upon collision. 
             */
        }
        
    }

    

    //private void OnMouseDrag ()
    //{
    //    Vector3 mPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z - transform.position.z));
    //    mPos.x = Mathf.Clamp(mPos.x, 0, Screen.width);
    //    mPos.y = Mathf.Clamp(mPos.y, 0, Screen.height);
    //    Vector3 point = Camera.main.ScreenToWorldPoint(mPos);
    //    point.z = transform.position.z;
    //    transform.position = point;

    //}

    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("entering trigger");

        //explosion for asteroid
        //Instantiate(explosion, transform.position, transform.rotation);

        //Destroy other object on collision along with the asteroid. Useful for colliding with player or projectiles
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
    */
}

[Serializable]
public class Square
{
    public float left;
    public float right;
    public float top;
    public float bottom;
}

