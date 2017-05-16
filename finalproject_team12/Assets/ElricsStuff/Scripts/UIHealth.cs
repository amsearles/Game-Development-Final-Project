using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealth : MonoBehaviour
{

    PlayerUnit playerunit;
    UnityEngine.UI.Text uitext;

    void Start ()
    {
        playerunit = GameObject.FindObjectOfType<PlayerUnit>();
        uitext = gameObject.GetComponentInChildren<UnityEngine.UI.Text>();
    }
	
	void Update ()
    {
        if (playerunit != null && uitext != null)
        {
            uitext.text = "HP: " + playerunit.GetComponent<HealthComponent>().currentHealth;
        }
        else
        {
            uitext.text = "HP: 0";
        }
	}

}
