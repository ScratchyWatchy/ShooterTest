using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaEntered : MonoBehaviour
{
    public UnityEvent<bool> onLevelRestart;
    public bool isDeath;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != 9) return;
        onLevelRestart.Invoke(isDeath);
    }
}
