using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 3f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _gravity = 80f;

    private Vector3 _moveDir;

    private bool isGrounded;

    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = _characterController.isGrounded;

        HandleMovement();

        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Press Space");
            Jump();
        }

        ApplyMovement();
    }

    private void HandleMovement()
    {
        _moveDir = new Vector3(Input.GetAxis("Horizontal") * _movementSpeed, 0f, Input.GetAxis("Vertical") * _movementSpeed);
    }
    private void Jump()
    {
        if (isGrounded)
        {
            Debug.Log("Jumped");
            _moveDir.y = _jumpForce * Time.deltaTime;
        }
    }

    private void ApplyMovement()
    {
        if (!isGrounded)
        {
            _moveDir.y -= _gravity * Time.deltaTime;
            Debug.Log("Gravity");
        }
        _characterController.Move(_moveDir * Time.deltaTime);
    }
}
