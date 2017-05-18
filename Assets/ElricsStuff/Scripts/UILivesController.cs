using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILivesController : UIControl {

    public int lastLives = 1;

	// Use this for initialization
	protected override void Start () {
        base.Start();
        if (Get() != null)
        {
            lastLives = Get().lives;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Get() != null && lastLives != Get().lives)
        {
            anim.Play("Grow", -1, 0);
            lastLives = Get().lives;
        }

        if (Get() != null && uitext != null)
        {
            uitext.text = "Lives: " + Get().lives;
        }
    }
}
