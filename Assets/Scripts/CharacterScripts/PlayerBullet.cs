using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{

    private void OnTriggerEnter(Collider other)
    {
        if (!collisionLayers.Contains(other.gameObject.layer)) return;
        Instantiate(explosionParticles, transform.position, Quaternion.identity);
        if (other.gameObject.layer == 6) Destroy(other.gameObject);
        base.OnTriggerEnter(other);
    }
}
