using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(Animator))]
public class Player : MonoBehaviour
{

    #region Public Variables

    // public float movementSpeed = 10f;
    // public float rotationSpeed = 10f;

    public float movementDuration = 1f;
    public float rotationDuration = 1f;

    public AnimationCurve animationEaseFunction;

    [Range(10,1000)]
    public float swipeThreshold = 100f;

    #endregion
    
    #region Serialized Variables

    [SerializeField] private Transform spotPositions;

    #endregion
    
    #region Private Variables

    private Rigidbody _rigidbody;
    private Animator _animator;
    private Transform _camera;

    private Vector2 input;
    private Vector3 swipeStartPos;
    private float smoothMagnitude = 0f;
    private bool weaponEnabled = false;

    private bool IsMove = false;

    private List<Transform> spots;
    private int currentSpotIndex = 0;

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

        spots = new List<Transform>();
        for (int i = 0; i < spotPositions.childCount; i++)
        {
            spots.Add(spotPositions.GetChild(i));
        }
        
        _rigidbody.transform.position = new Vector3(spots[0].position.x,_rigidbody.transform.position.y,_rigidbody.transform.position.z);
        
    }

    private void Update()
    {
        HandleInput();
        HandleAnimation();
    }

    private void FixedUpdate()
    {
        // PerformRotation();
        // PerformMovement();
    }

    #endregion

    #region Private Methods

    private void HandleInput()
    {
        //For PC Version
        // input = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));  // 8-directional movement
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
            swipeStartPos = Input.mousePosition;

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            float swipeDistance = Mathf.Abs(swipeStartPos.x - Input.mousePosition.x);

            if (swipeDistance >= swipeThreshold && IsMove==false)
            {
                bool swipeLeft = swipeStartPos.x - Input.mousePosition.x > 0;
                
                if (swipeLeft)
                {
                    if (currentSpotIndex > 0)
                    {
                        StartCoroutine(MoveToDestinationPoint(swipeLeft));
                    }
                }
                else
                {
                    if (currentSpotIndex < spots.Count-1)
                    {
                        StartCoroutine(MoveToDestinationPoint(swipeLeft));
                    }
                }
            }
        }

        //For Android Version
        // if (Input.touchCount > 0 )
        // {
        //     if(Input.GetTouch(0).phase.Equals(TouchPhase.Began))
        //         swipeStartPos = Input.mousePosition;
        //
        //     if (Input.GetTouch(0).phase.Equals(TouchPhase.Ended))
        //     {
        //         float swipeDistance = Mathf.Abs(swipeStartPos.x - Input.mousePosition.x);
        //
        //         if (swipeDistance >= swipeThreshold)
        //         {
        //             bool swipeLeft = swipeStartPos.x - Input.mousePosition.x > 0;
        //             
        //             Debug.Log(swipeLeft);
        //         }
        //     }
        // }
    }

    private void HandleAnimation()
    {
        // smoothMagnitude = Mathf.Lerp(smoothMagnitude, input.normalized.magnitude, Time.deltaTime * 5f);
        _animator.SetFloat("speedPercentage",smoothMagnitude);
    }
    
    private void PerformRotation()
    {
        Vector2 inputDirection = input.normalized;

        if (inputDirection != Vector2.zero)
        {
            // _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, Quaternion.AngleAxis(
            //                                                                 (Mathf.Atan2(inputDirection.x, inputDirection.y) * Mathf.Rad2Deg +
            //                                                                  _camera.eulerAngles.y), Vector3.up)*Quaternion.Euler(_rigidbody.transform.eulerAngles.x,0f,_rigidbody.transform.eulerAngles.z), Time.deltaTime * rotationSpeed);
        }
    }

    private void PerformMovement()
    {
        // _rigidbody.MovePosition(_rigidbody.position + _rigidbody.transform.forward*movementSpeed*Time.fixedDeltaTime*input.magnitude);
    }


    #endregion
    
    #region Public Methods

    

    #endregion

    #region Coroutines

    private IEnumerator MoveToDestinationPoint(bool swipeLeft)
    {
        IsMove = true;
        
        float value = 0f;
        
        // Rotate - Move - Rotate

        Vector3 direction = Vector3.zero;
        Vector3 initialDireciton = _rigidbody.transform.forward;
        
        float startX = _rigidbody.position.x;
        float endX = 0;


        if (swipeLeft)
        {
            if (currentSpotIndex > 0)
            {
                direction = spots[currentSpotIndex - 1].position - spots[currentSpotIndex].position;
                direction.y = 0;
                endX = spots[currentSpotIndex - 1].position.x;
                currentSpotIndex--;
            }
        }
        else
        {
            if (currentSpotIndex < spots.Count-1)
            {
                direction = spots[currentSpotIndex + 1].position - spots[currentSpotIndex].position;
                direction.y = 0;
                endX = spots[currentSpotIndex + 1].position.x;
                currentSpotIndex++;
            }
        }

        Vector3 startForward = _rigidbody.transform.forward;
        while (value < 1f)
        {
            
            value = Mathf.Clamp01(Time.deltaTime / rotationDuration + value);
            _rigidbody.transform.forward = Vector3.Lerp(startForward, direction, value);
            
            yield return null;
        }

        value = 0f;
        while (value < 1f)
        {
            value = Mathf.Clamp01(Time.deltaTime / movementDuration + value);
            smoothMagnitude = animationEaseFunction.Evaluate(value);
            _rigidbody.position = new Vector3(Mathf.Lerp(startX,endX,value),_rigidbody.transform.position.y,_rigidbody.transform.position.z);
            
            yield return null;
        }
        
        startForward = _rigidbody.transform.forward;
        value = 0f;
        while (value < 1f)
        {
            value = Mathf.Clamp01(Time.deltaTime / rotationDuration + value);

            _rigidbody.transform.forward = Vector3.Lerp(startForward, initialDireciton, value);
            
            yield return null;
        }

        IsMove = false;
    }

    #endregion
}
