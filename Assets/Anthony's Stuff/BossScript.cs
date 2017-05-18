using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : GameUnit {
	public GameObject projectile;
	Transform Player;
	float MoveSpeed= 4;
	float MaxDist= 10;
	float MinDist= 5;
	// Use this for initialization
	void Start () {
		Player = GameObject.FindWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		//creating a bullet
		transform.LookAt(Player);
		//moving to players location
		if(Vector3.Distance(transform.position,Player.position) >= MinDist){

			transform.position += transform.forward*MoveSpeed*Time.deltaTime;


			//shoot at player as it chases him
			if(Vector3.Distance(transform.position,Player.position) <= MaxDist)
			{
				GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
				bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * 6;
				//destroying bullet after 5 seconds
				Destroy (bullet, 5.0f);
			} 
		
	}
}
}


	


