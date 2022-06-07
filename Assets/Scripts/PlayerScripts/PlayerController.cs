using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : CharachterControllerGravity
{
    

    [SerializeField] 
    private Transform _cameraTransform;

    private Vector2 _look;

    private float _threshold = 0.01f;
    private float _lookAngle;
    
    public float topClamp = 90.0f;
    public float bottomClamp = -90.0f;
    
    public float rotationSpeed = 1.0f;

    public UnityEvent<bool> onRestartLevel;
    private float _imapctDecreaseRate = 0.93f;
    private float _impactTreshold = 0.1f;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        FindObjectsOfType<EnemyAim>().ForEach(x => x.player = transform);
    }
    
    private void OnMove(InputValue movementValue)
    {
        _movement = movementValue.Get<Vector2>();
    }

    [Button]
    public void AddImpact(Vector3 force)
    {
        _impactVector += force;
    }

    private void CullImpact()
    {
        if(_impactVector.magnitude == 0) return;
        _impactVector *= _imapctDecreaseRate;
        if(_impactVector.magnitude < _impactTreshold) _impactVector = Vector3.zero;
    }

    private void OnLook(InputValue lookValue)
    {
        _look = lookValue.Get<Vector2>();
    }
    
    private void CameraRotation()
    {
        if (_look.sqrMagnitude >= _threshold)
        {
            _lookAngle -= _look.y * rotationSpeed;
            _lookAngle = ClampAngle(_lookAngle, bottomClamp, topClamp);
            _cameraTransform.localRotation = Quaternion.Euler(_lookAngle, 0, 0);
            transform.Rotate(Vector3.up * (rotationSpeed * _look.x));
        }
    }

    private void Update()
    {
        base.Update();
        CullImpact();
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
