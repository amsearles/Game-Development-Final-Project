using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPowerUp : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerUnit>() != null)
        {
            other.GetComponent<TrailRendererWith2DCollider>().widthStart += .2f;
            other.GetComponent<TrailRendererWith2DCollider>().widthEnd += .2f;
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
