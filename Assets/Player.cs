using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(Animator))]
public class Player : MonoBehaviour
{

    #region Public Variables

    public float movementSpeed = 10f;
    public float rotationSpeed = 10f;

    #endregion
    
    #region Serialized Variables

    

    #endregion
    
    #region Private Variables

    private Rigidbody _rigidbody;
    private Animator _animator;
    private Transform _camera;

    private Vector2 input;
    private float smoothMagnitude = 0f;
    private bool weaponEnabled = false;

    #endregion


    #region Unity's Methods

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        
        if (Camera.main == null)
        {
            _camera = FindObjectOfType<Camera>().transform;
        }
        else _camera = Camera.main.transform;
        
    }

    private void Update()
    {
        HandleInput();
        HandleAnimation();
    }

    private void FixedUpdate()
    {
        PerformRotation();
        PerformMovement();
    }

    #endregion

    #region Private Methods

    private void HandleInput()
    {
        input = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
    }

    private void HandleAnimation()
    {
        smoothMagnitude = Mathf.Lerp(smoothMagnitude, input.normalized.magnitude, Time.deltaTime * 5f);
        _animator.SetFloat("speedPercentage",smoothMagnitude);
    }
    
    private void PerformRotation()
    {
        Vector2 inputDirection = input.normalized;

        if (inputDirection != Vector2.zero)
        {
            _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, Quaternion.AngleAxis(
                                                                            (Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg +
                                                                             _camera.eulerAngles.y), Vector3.up)*Quaternion.Euler(_rigidbody.transform.eulerAngles.x,0f,_rigidbody.transform.eulerAngles.z), Time.deltaTime * rotationSpeed);
        }
    }

    private void PerformMovement()
    {
        _rigidbody.MovePosition(_rigidbody.position + _rigidbody.transform.forward*movementSpeed*Time.fixedDeltaTime*input.magnitude);
    }

    #endregion
    
    #region Public Methods

    

    #endregion
}
