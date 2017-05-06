using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class AIFiringStraight : MonoBehaviour {
    
    private Weapon weapon;          // Weapon to use in firing and manipulating.


	// Use this for initialization
	private void Start () {
        weapon = GetComponent<Weapon>();
	}
	
	// Update is called once per frame
	void Update () {

        // If this ship has a weapon and have firing ports to spawn the projectiles...
        if (weapon != null)
        {
            weapon.Fire();
        }
    }

}
