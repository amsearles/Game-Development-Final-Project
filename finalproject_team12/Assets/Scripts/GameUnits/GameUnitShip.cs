using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUnitShip : GameUnit {

    public Weapon weapon;
    public GameObject[] firingPorts;

    private int projSpawnPtsIndex;

    // Use this for initialization
    void Start () {
        projSpawnPtsIndex = 0;
    }

    // TEMPORARY: This is set to always shoot when available. Move this elsewhere, maybe an AI script or something.
    //private void Update()
    //{
    //    //If this ship has a weapon and have firing ports to spawn the projectiles...
    //    if ((weapon != null) && (weapon.canFire()) && (firingPorts.Length != 0))
    //    {
    //        weapon.Fire(firingPorts[projSpawnPtsIndex].transform);

    //        //Reset values.
    //        projSpawnPtsIndex = (projSpawnPtsIndex + 1) % firingPorts.Length;
    //    }
    //}


}
