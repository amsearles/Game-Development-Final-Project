/** 
 * Elric Dang
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealthController : UIControl
{

    protected PlayerUnit playerunit;
    int lastCurrentHealth = 0;
    int lastMaxHealth = 0;

    HealthComponent Get()
    {
        if (playerunit != null)
            return playerunit.gameObject.GetComponent<HealthComponent>();
        else
            return null;
    }

    protected override void Start ()
    {

        base.Start();
        playerunit = GameObject.FindObjectOfType<PlayerUnit>();
        if (Get() != null)
        {
            lastCurrentHealth = Get().currentHealth;
            lastMaxHealth = Get().maxHealth;
        }
        else
        {
            lastCurrentHealth = 0;
            lastMaxHealth = 0;
        }

    }
	
	void Update ()
    {

        if (Get() != null && lastCurrentHealth != Get().currentHealth)
        {
            anim.Play("Grow", -1, 0);
            lastCurrentHealth = Get().currentHealth;
            lastMaxHealth = Get().maxHealth;
        }
        else if (Get() == null && lastCurrentHealth == 0)
        {
            anim.Play("Grow", -1, 0);
            lastCurrentHealth = -1;
        }

        if (playerunit != null && uitext != null)
        {
            uitext.text = "HP: " + Get().currentHealth + " / " + Get().maxHealth;
            if ((float) Get().currentHealth / (float) Get().maxHealth <= .3f)
                uitext.color = Color.red;
            else if ((float) Get().currentHealth / (float) Get().maxHealth <= .6f)
                uitext.color = Color.yellow;
            else
                uitext.color = Color.white;
        }
        else if (uitext != null)
        {
            uitext.color = Color.red;
            uitext.text = "HP: 0 / " + lastMaxHealth;
        }

    }

}
