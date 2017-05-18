using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {
	public float speed;
	public GameObject explosion;
	public GameController gc;
	// Use this for initialization
	void Start () {
		GameObject gcObj = GameObject.FindWithTag ("GameController");
		gc = gcObj.GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void OnTriggerEnter2D(Collider2D other) 
	{
		Debug.Log ("entering trigger");

		//explosion for asteroid
		Instantiate(explosion, transform.position, transform.rotation);

		//Destroy other object on collision along with the asteroid. Useful for colliding with player or projectiles
		if (!(other.CompareTag ("Enemy"))) {
			Destroy (gameObject);
		}
		gc.AddScore (10);
	}
}
