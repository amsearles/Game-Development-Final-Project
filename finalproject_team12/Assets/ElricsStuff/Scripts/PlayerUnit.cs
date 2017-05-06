using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : GameUnit
{

    public int maxTrailSize = 10;

    void Start ()
    {
		
	}

    void OnMouseDrag ()
    {
        Vector3 mPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(Camera.main.transform.position.z - transform.position.z));
        mPos.x = Mathf.Clamp(mPos.x, 0, Screen.width);
        mPos.y = Mathf.Clamp(mPos.y, 0, Screen.height);
        Vector3 point = Camera.main.ScreenToWorldPoint(mPos);
        point.z = transform.position.z;
        transform.position = point;
    }

	void Update ()
    {

	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		Debug.Log ("entering trigger");

		//explosion for asteroid
		//Instantiate(explosion, transform.position, transform.rotation);

		//Destroy other object on collision along with the asteroid. Useful for colliding with player or projectiles
		if (other.CompareTag ("Enemy")) {
			Destroy (gameObject);
			}
	}
}
