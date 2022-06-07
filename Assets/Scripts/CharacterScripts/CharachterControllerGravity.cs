using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharachterControllerGravity : MonoBehaviour
{
    public float GroundedOffset = 0.6f;
    public float GroundedRadius = 0.5f;
    public LayerMask GroundLayers;
    public float speedChangeRate = 10.0f;
    public float moveSpeed = 4.0f;
    
    protected float _speed;
    protected float _playerVelocity;
    protected float _playerVerticalVelocity;
    protected Vector3 _impactVector;
    protected Vector2 _movement;
    
    private bool _isGrounded = true;
    private float _gravity = 17.0f;
    private CharacterController _characterController;

    


    private void Awake()
    {
        InitializeComponents();
    }
    
    private void InitializeComponents()
    {
        _characterController = GetComponent<CharacterController>();
    }
    
    protected void Update()
    {
        GroundedCheck();
        Gravity();
        Move();
    }
    private void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        _isGrounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }
    
    private void Gravity()
    {
        if (!_isGrounded) _playerVerticalVelocity += _gravity * Time.deltaTime;
        else _playerVerticalVelocity = 0;
    }
    
    private void Move()
    {
        if (_movement == Vector2.zero) _playerVelocity = 0.0f;
        
        float currentHorizontalSpeed = new Vector3(_characterController.velocity.x, 0.0f, _characterController.velocity.z).magnitude;

        float speedOffset = 0.1f;
        
        if (currentHorizontalSpeed < moveSpeed - speedOffset || currentHorizontalSpeed > moveSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, moveSpeed, Time.deltaTime * speedChangeRate);
        }
        else
        {
            _speed = moveSpeed;
        }
        
        Vector3 inputDirection = new Vector3(_movement.x, 0.0f, _movement.y).normalized;
        
        if (_movement != Vector2.zero)
        {
            inputDirection = transform.right * _movement.x + transform.forward * _movement.y;
        }
        _characterController.Move(inputDirection.normalized * (_speed * Time.deltaTime) + Vector3.down * (_playerVerticalVelocity * Time.deltaTime) + _impactVector * Time.deltaTime);

    }
    
    private void OnDrawGizmosSelected()
    {
        Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.25f);
        Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.25f);

        if (_isGrounded) Gizmos.color = transparentGreen;
        else Gizmos.color = transparentRed;
        
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
    }
}
