using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : PowerUp
{

    void OnTriggerEnter (Collider other)
    {
        if (other.GetComponent<PlayerUnit> () != null)
        {
            other.GetComponent<PlayerUnit> ().currentHealth += 10;
            Destroy(gameObject);
        }
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

}
