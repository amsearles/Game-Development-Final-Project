using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgSwitch : MonoBehaviour {

	public GameObject background_one;
	public GameObject background_two;
	public GameObject background_three;
	float timer = 0f;



	// Use this for initialization
	void Start () {

		Instantiate(background_one, new Vector3(0,0,-10), Quaternion.identity);                
	}


	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;    

		if (timer >= 15)
		{
			Instantiate(background_two, new Vector3(0, 0, -10), Quaternion.identity);
		}

		if (timer >= 25)
		{
			Instantiate(background_three, new Vector3(0, 0, -10), Quaternion.identity);
		}
	}

}