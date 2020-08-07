using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour

{
    [SerializeField] private Transform target;

    public Vector3 offset;
    public float smoothTime = 5f;
    
    public float mouseSensivity;
    public Vector2 angleRange;

    private float xRot, yRot;
    private Vector3 currentVelocity,currentRotation;
    
    private void Start()
    {
        PerformOffset();
    }

    private void Update()
    {
        // HandleInput();
    }

    private void LateUpdate()
    {
        // PerformRotation();
        PerformOffset();
    }

    private void PerformOffset()
    {
        transform.position = target.position - transform.forward * offset.z - transform.right * offset.x -
                             transform.up * offset.y;
    }

    private void PerformRotation()
    {
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(xRot, yRot, 0f), ref currentVelocity,
            Time.deltaTime * smoothTime);

        transform.eulerAngles = currentRotation;
    }

    private void HandleInput()
    {
        xRot -= Input.GetAxisRaw("Mouse Y")*mouseSensivity;
        yRot += Input.GetAxisRaw("Mouse X")*mouseSensivity;

        xRot = Mathf.Clamp(xRot, angleRange.x, angleRange.y);
    }
}
