using System;
using System.Collections;
using System.Collections.Generic;
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
	public AudioClip shoot;
	private AudioSource source;
	private float vol = 0.5F;

    // **** Variables ****
    public List<Renderer> playerUnitBodyMeshes;
    public List<Collider> playerUnitBodyColliders;

    [SerializeField]
    private int _lives = 3;

    // **** Property ****
    public int lives
    {
        get { return _lives; }
        set { _lives = (value < 0) ? 0 : value; }
    }


    // *****************
    // 
    //  Public Methods
    //
    // *****************

	void Awake(){
		source = GetComponent<AudioSource> ();
	}

    public void MoveToward(Vector3 newPosition)
    {
        transform.position = Vector3.Lerp(transform.position, newPosition, moveSpeedComponent.speed * Time.deltaTime);
    }
    
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        
        // TODO: Maybe handle health slider / health text here.
        
    }

    public void Respawn()
    {
        // TODO: use lives to respawn PlayerUnit.
        /*
         * Dilemma:
         * PlayerUnit is the actual unit and gets Destroy() on death.
         * Static int is possible but it will deny access via Inspector.
         * Easy Fix is to place this into GameController.
         */

    }

    // *****************
    // 
    //  Private Methods
    //
    // *****************

    private void ConstrainPositionToScreen()
    {
        var dist = (transform.position.y - Camera.main.transform.position.y);

        var leftLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightLimitation = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;

        var upLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).z;
        var downLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).z;


        Vector3 currentPosition = transform.position;

        currentPosition.x = Mathf.Clamp(transform.position.x, rightLimitation, leftLimitation);
        currentPosition.z = Mathf.Clamp(transform.position.z, downLimitation, upLimitation);


        transform.position = currentPosition;
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
            ConstrainPositionToScreen();
            if (source != null)
			    source.PlayOneShot (shoot, 20);
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
