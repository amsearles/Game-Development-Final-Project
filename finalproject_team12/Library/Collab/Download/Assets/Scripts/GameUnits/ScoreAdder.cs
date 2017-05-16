/**
 * Anthony Searles & Elric Dang
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAdder : MonoBehaviour {
	public GameController gc;
	public int scoreValue;
	// Use this for initialization
	void Start () {
		GameObject gcObj = GameObject.Find ("GameController");
		gc = gcObj.GetComponent<GameController> ();
		scoreValue = 100;	
	}
	void Update(){
	}
	// Update is called once per frame
	void OnDestroy () {
        if (gameObject.GetComponent<HealthComponent> () != null && gameObject.GetComponent<HealthComponent>().currentHealth <= 0 && gc != null)
            gc.AddScore (10);
	}
}
