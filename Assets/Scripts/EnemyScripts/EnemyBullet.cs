using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : Bullet
{
    public float pushForce;

    private void OnTriggerEnter(Collider other)
    {
        if(!collisionLayers.Contains(other.gameObject.layer)) return;
        if (other.gameObject.layer == 9)
        {
            other.GetComponent<PlayerController>().AddImpact((transform.forward + Vector3.up) * pushForce);
        }
        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        base.OnTriggerEnter(other);
    }
}
