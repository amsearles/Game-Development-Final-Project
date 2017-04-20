using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
	public float speed;
	public GameObject explosion;
	// Use this for initialization
	void Start () {
		 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void OnTriggerEnter(Collider other) 
	{

		//explosion for asteroid
		Instantiate(explosion, transform.position, transform.rotation);

		//Destroy other object on collision along with the asteroid.
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
