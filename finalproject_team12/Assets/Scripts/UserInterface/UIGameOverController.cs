using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOverController : UIControl {
    
    private void Update()
    {
        if (Get() != null && Get().isGameOver)
        {
            anim.Play("GameOverAnim");
        }
    }

}
