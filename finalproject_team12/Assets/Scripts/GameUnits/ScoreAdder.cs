using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAdder : MonoBehaviour {
	public GameController gc;
	public int scoreValue;
	// Use this for initialization
	void Start () {
		GameObject gcObj = GameObject.FindWithTag ("GameController");
		gc = gcObj.GetComponent<GameController> ();
		scoreValue = 10;

	}
	
	// Update is called once per frame
	void Update () {
		if (this.gameObject == null) {
			gc.AddScore (scoreValue);
		}
	}
}
