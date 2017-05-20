/**
 * Elric Dang
 */ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyGameController : MonoBehaviour
{

	void Start ()
    {
        GameController find = GameObject.FindObjectOfType<GameController>();
        find.setBoss(gameObject);
    }

    void Update () {
		
	}
}
