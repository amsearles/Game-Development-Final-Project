using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBullet : GameUnit {
    
    private void FixedUpdate()
    {
        MoveFoward();
    }
    
    public void MoveFoward()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (rigidbody != null)
            rigidbody.velocity = transform.forward * moveSpeed * Time.deltaTime;
        else
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
    
}
