using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILivesController : UIControl {

    public int lastLives = 1;

	protected override void Start () {
        base.Start();
        if (Get() != null)
        {
            lastLives = Get().lives;
        }
    }
	
	void Update () {

        if (Get() != null && lastLives != Get().lives)
        {
            Play("Grow", -1, 0);
            lastLives = Get().lives;
        }

        if (Get() != null && uitext != null && lastLives < 0)
        {
            uitext.text = "Lives: 0";
        }
        else if (Get() != null && uitext != null)
        {
            uitext.text = "Lives: " + Get().lives;
        }

    }
}
