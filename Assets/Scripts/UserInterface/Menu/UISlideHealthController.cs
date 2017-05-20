/**
 * Elric Dang
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlideHealthController : UIControl {

    protected override void Start()
    {

        base.Start();

    }

    void Update () {
        if (go != null)
        {
            GetComponent<Slider>().value = (float) go.GetComponent<GameUnit> ().healthComponent.currentHealth / (float) go.GetComponent<GameUnit>().healthComponent.maxHealth;
        }	
	}

}
