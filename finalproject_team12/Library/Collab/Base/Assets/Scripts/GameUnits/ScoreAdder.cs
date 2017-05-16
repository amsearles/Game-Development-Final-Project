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
		scoreValue = 100;	
	}
	void Update(){
	}
	// Update is called once per frame
	void OnDestroy () {
		
			gc.AddScore (10);
	}
}
