using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _velocity;
    private float _destroyDeleay = 3f;
    private Coroutine _destroyCoroutine;
    public GameObject explosionParticles;
    public List<int> collisionLayers;
    public int enemyLayer;
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if(_destroyCoroutine != null) StopCoroutine(_destroyCoroutine);
        _destroyCoroutine = StartCoroutine(DelayedTurnOff());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * (_velocity * Time.deltaTime)); 
    }

    private IEnumerator DelayedTurnOff()
    {
        yield return new WaitForSeconds(_destroyDeleay);
        gameObject.SetActive(false);
    }

    protected void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
