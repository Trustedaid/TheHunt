using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public Transform playerTransform;
    public Rigidbody rb;

    public float moveSpeed;
    
    private Vector3 _moveDirection;
    private float _horizontalInput;
    private float _verticalInput;

    
    
    // Start is called before the first frame update
    void Start()
    {
        FreezeRigidbody();
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInputs();
    }

    private void FreezeRigidbody()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }

    private void PlayerInputs()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        
        _moveDirection = playerTransform.forward * _verticalInput + playerTransform.right * _horizontalInput;
        
        rb.AddForce(_moveDirection.normalized * (moveSpeed * 10f ), ForceMode.Force);
    }
}
