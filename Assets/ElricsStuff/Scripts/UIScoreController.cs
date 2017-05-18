/**
 * Elric Dang
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScoreController : UIControl
{

    int lastScore = 0;
    
    protected override void Start()
    {

        base.Start();
        if (Get() != null)
        {
            lastScore = Get().score;
        }
        else
        {
            lastScore = 0;
        }

    }

    void Update()
    {
        if (Get() != null && lastScore != Get().score)
        {
            anim.Play("Grow", -1, 0);
            lastScore = Get().score;
        }

        if (Get() != null && uitext != null)
        {
            uitext.text = "Score: " + Get().score;
        }

    }

}
