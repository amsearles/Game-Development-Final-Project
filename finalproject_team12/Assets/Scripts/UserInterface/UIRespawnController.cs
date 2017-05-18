using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRespawnController : UIControl {

    public static int DISPLAYTIME = -1;

    protected override void Start()
    {
        DISPLAYTIME = -1;
        base.Start();
    }

    private void Update()
    {
        if (Get() != null && uitext != null && DISPLAYTIME != -1)
        {
            uitext.text = "RESPAWN: " + DISPLAYTIME;
        }
        else
        {
            uitext.text = "";
        }
    }

}
