using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Parametrs")]
    [SerializeField] private float _movementSpeed = 3.0f;
    //Напишем сами
    [SerializeField] private float _sprintSpeed = 9.0f;

    [Header("Jump Parametrs")]
    [SerializeField] private float _jumpForce = 8.0f;
    [SerializeField] private float _gravity = 30.0f;

    [Header("Camera Parametrs")]
    [SerializeField, Range(1, 10)] private float _cameraSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float _cameraSpeedY = 2.0f;
    [SerializeField, Range(1, 100)] private float _upCameraLimit = 80.0f;
    [SerializeField, Range(1, 100)] private float _downCameraLimit = 80.0f;

    private Camera _playerCamera;
    private CharacterController _characterController;

    private Vector3 _moveDirection;
    private Vector2 _currentInput;

    private float _rotationX = 0;
    void Awake()
    {
        _playerCamera = GetComponentInChildren<Camera>();
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovementInput();
        HandleMouseLook();


        //Напишем сами
        if (_characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            HandleJump();
        }

        ApplyFinalMovements();
    }

    private void HandleMovementInput()
    {
        //Напишем сами(логика бега)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentInput = new Vector2(_sprintSpeed * Input.GetAxis("Vertical"), _sprintSpeed * Input.GetAxis("Horizontal"));
        }
        else
        {
            _currentInput = new Vector2(_movementSpeed * Input.GetAxis("Vertical"), _movementSpeed * Input.GetAxis("Horizontal"));
        }
        float moveDirectionY = _moveDirection.y;
        _moveDirection = (transform.TransformDirection(Vector3.forward) * _currentInput.x)
            + (transform.TransformDirection(Vector3.right) * _currentInput.y);
        _moveDirection.y = moveDirectionY;
    }

    private void HandleMouseLook()
    {
        _rotationX -= Input.GetAxis("Mouse Y") * _cameraSpeedY;
        _rotationX = Mathf.Clamp(_rotationX, -_upCameraLimit, _downCameraLimit);
        _playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _cameraSpeedX, 0);
    }

    private void ApplyFinalMovements()
    {
        if (!_characterController.isGrounded)
        {
            _moveDirection.y -= _gravity * Time.deltaTime;
        }

        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (_characterController.isGrounded)
        {
            _moveDirection.y = _jumpForce;
        }
    }

    //POWER UPS
    public void SpeedUP()
    {
        _movementSpeed += 2;
        _sprintSpeed += 3;
    }
    public void JumpUP()
    {
        _jumpForce += 3;
    }
    public void SizeUP()
    {
        gameObject.transform.localScale = Vector3.one * 2;
    }
}
