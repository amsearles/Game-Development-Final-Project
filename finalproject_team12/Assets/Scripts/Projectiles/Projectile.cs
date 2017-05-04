using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    
    public float damage = 10f;
    public float moveSpeed = 1.0f;
    public GameObject collisionEffect;
    public GameObjectIdentifier enemyIdentifiers;
    
    // Use this for initialization
    private void Start()
    {
        if (damage < 0f) damage = 0f;
    }

    private void FixedUpdate()
    {
        MoveFoward();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (isEnemy(collision.gameObject))
        {
            //TODO: Handle health decrease upon contact.
            Debug.Log("Projectile class requires handling health decrease upon OnCollisionEnter()");

            DieAndEffect();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isEnemy(other.gameObject))
        {
            //TODO: Handle health decrease upon contact.
            Debug.Log("Projectile class requires handling health decrease upon OnTriggerEnter()");

            DieAndEffect();
        }
    }
    
    protected bool isEnemy(GameObject gameObj)
    {
        //TODO: Handle component matching too?
        return (enemyIdentifiers.HasTagName(gameObj.tag));
    }

    public void MoveFoward()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    public void DieAndEffect()
    {
        
        if (collisionEffect != null)
            Instantiate(collisionEffect, transform.position, transform.rotation);

        Destroy(gameObject);
    }

}
