using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBullet : GameUnit {
    

    private void FixedUpdate()
    {
        MoveFoward();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
    }

    public void MoveFoward()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
    
}
