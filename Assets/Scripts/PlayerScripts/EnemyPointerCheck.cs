using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyPointerCheck : MonoBehaviour
{
    public UnityEvent<bool> onPointingAtEnemyChanged;
    [SerializeField] private Transform origin;
    private bool isPointingAtEnemy;
    public static EnemyPointerCheck instance;
    public static EnemyPointerCheck Instance { get; private set; }

    private RaycastHit _hit;
    private bool _isHit;
    [SerializeField] private int _layer;
    
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
    
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _isHit = Physics.Raycast(origin.position, origin.forward, out _hit,Mathf.Infinity);

        if (_isHit && _hit.transform.gameObject.layer == _layer && !isPointingAtEnemy)
        {
            isPointingAtEnemy = true;
            onPointingAtEnemyChanged.Invoke(isPointingAtEnemy);
        }
        else if (!_isHit || _hit.transform.gameObject.layer != _layer && isPointingAtEnemy)
        {
            isPointingAtEnemy = false;
            onPointingAtEnemyChanged.Invoke(isPointingAtEnemy);
        }
    }
}
