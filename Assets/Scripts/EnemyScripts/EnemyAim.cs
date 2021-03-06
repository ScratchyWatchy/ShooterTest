using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    public Transform player;
    public GameObject bullet;
    public float breakBetweenShots;
    
    [SerializeField] private Transform _bulletOrigin;
    void Start()
    {
        InvokeRepeating(nameof(Shoot),0, breakBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
    }

    private void Shoot()
    {
        Instantiate(bullet, _bulletOrigin.position, transform.rotation);
    }
}
