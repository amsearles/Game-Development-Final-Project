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

    HealthComponent GetHealth()
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
        if (GetHealth() != null)
        {
            lastCurrentHealth = GetHealth().currentHealth;
            lastMaxHealth = GetHealth().maxHealth;
        }
        else
        {
            lastCurrentHealth = 0;
            lastMaxHealth = 0;
        }

    }
	
	void Update ()
    {
        if (playerunit == null)
            playerunit = GameController.currentPlayerUnit;

        if (GetHealth() != null && (lastCurrentHealth != GetHealth().currentHealth || lastMaxHealth != GetHealth().maxHealth))
        {
            Play("Grow", -1, 0);
            lastCurrentHealth = GetHealth().currentHealth;
            lastMaxHealth = GetHealth().maxHealth;
        }
        else if (GetHealth() == null && lastCurrentHealth == 0)
        {
            Play("Grow", -1, 0);
            lastCurrentHealth = -1;
        }

        if (playerunit != null && uitext != null)
        {
            uitext.text = "HP: " + GetHealth().currentHealth + " / " + GetHealth().maxHealth;
            if ((float) GetHealth().currentHealth / (float) GetHealth().maxHealth <= .3f)
                uitext.color = Color.red;
            else if ((float) GetHealth().currentHealth / (float) GetHealth().maxHealth <= .6f)
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
