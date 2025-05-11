using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerInputManager _inputManager;
    CharacterController _characterController;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform _cameraFollowTarget;
    [SerializeField] GameObject playerVcam;

    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpForce = 5f;

    #region RunTimeVariables
    private Vector3 targetDirection;
    private float targetRotation;
    private Quaternion rotation;

    private Quaternion rotationCameraDregrees;
    private Vector3 inputDirection;

    private float xRotation;
    private float yRotation;
    private float speed;

    private Vector3 velocity;
    private bool jumpRequest;
    #endregion

    void Start()
    {
        _inputManager = GetComponent<PlayerInputManager>();
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Movement();
        HandleJump();
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    void Movement()
    {
        speed = 0;
        inputDirection = new Vector3(_inputManager.directionMove.x, 0, _inputManager.directionMove.y);

        if (_inputManager.directionMove != Vector2.zero)
        {
            speed = moveSpeed;

            Vector3 toCamera = playerVcam.transform.position - transform.position;
            toCamera.y = 0;
            toCamera.Normalize();

            Vector3 cameraForward = -toCamera;
            Vector3 cameraRight = Vector3.Cross(Vector3.up, cameraForward);
            Vector3 moveDirection = (cameraForward * inputDirection.z + cameraRight * inputDirection.x).normalized;

            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20 * Time.deltaTime);
            }

            // Movemos en horizontal
            _characterController.Move(moveDirection * speed * Time.deltaTime);
        }
    }

    void HandleJump()
    {
        if (_characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (jumpRequest && _characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity); 
            jumpRequest = false; 
        }

        velocity.y += gravity * Time.deltaTime;

        _characterController.Move(velocity * Time.deltaTime);
    }

    void CameraRotation()
    {
        xRotation += _inputManager.directionLook.y;
        yRotation += _inputManager.directionLook.x;
        xRotation = Mathf.Clamp(xRotation, -30, 70);
        rotationCameraDregrees = Quaternion.Euler(xRotation, yRotation, 0);
        _cameraFollowTarget.rotation = rotationCameraDregrees;
    }

    void OnJump()
    {
        if (_inputManager.jumpValue && _characterController.isGrounded) 
        {
            jumpRequest = true; 
        }
    }
}