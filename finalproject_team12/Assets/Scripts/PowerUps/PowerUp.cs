﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveSpeedComponent))]
[RequireComponent(typeof(SphereCollider))]
public abstract class PowerUp : MonoBehaviour {

    [Tooltip("Optional effect to instantiate upon pickup")]
    public GameObject onPickupEffect;

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            PlayerUnit playerUnitComp = other.transform.root.GetComponentInChildren<PlayerUnit>();

            if (playerUnitComp != null)
                HandlePickUp(playerUnitComp);

            if (onPickupEffect != null)
                Instantiate(onPickupEffect, transform.position, transform.rotation);

            DestroyPowerUp(gameObject);
        }
    }

    protected virtual void DestroyPowerUp(Object obj)
    {
        Destroy(obj);
    }

    /// <summary>Handle the improvements/effects onto the <see cref="PlayerUnit"/>.</summary>
    /// <param name="playerUnitComp">PlayerUnit component of collided object.</param>
    public abstract void HandlePickUp(PlayerUnit playerUnitComp);

}
