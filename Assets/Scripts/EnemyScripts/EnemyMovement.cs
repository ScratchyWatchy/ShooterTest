using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyMovement : CharachterControllerGravity
{

    void Start()
    {
        _movement = transform.right;
    }
    
    void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != 7) return;
        _movement *= -1;
    }
}
