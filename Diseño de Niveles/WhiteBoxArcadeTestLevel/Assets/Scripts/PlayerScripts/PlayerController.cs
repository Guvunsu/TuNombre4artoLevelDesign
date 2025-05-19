using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour {
    #region Variables
    PlayerInputManager input;
    [SerializeField] CharacterController characterController;
    [SerializeField] Transform cameraFollowTarget;
    [SerializeField] GameObject playerVcam;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    bool isGrounded;
    bool jumpRequest;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpForce = 3f;
    Vector3 velocity;

    float xRotation;
    float yRotation;
    [SerializeField] private float moveSpeed;

    #endregion Variables

    #region PublicMethods
    void Start() {
        input = GetComponent<PlayerInputManager>();
        characterController = GetComponent<CharacterController>();
    }
    void Update() {
        MovePlayer();
        JumpAndGravity();
        CamaraRotation();
    }

    #endregion PublicMethods

    #region Movement
    void MovePlayer() {
        float speed = 0;
        Vector3 moveInput = new Vector3(input.move.x, 0, input.move.y);

        if (moveInput.magnitude >= 0.1f) {
            speed = moveSpeed;

            // Obtén dirección relativa a la cámara
            Vector3 camForward = cameraFollowTarget.forward;
            Vector3 camRight = cameraFollowTarget.right;
            camForward.y = 0;
            camRight.y = 0;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDirection = camRight * moveInput.x + camForward * moveInput.z;

            // Rotación hacia la dirección de movimiento
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);

            characterController.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
    }

    #endregion Movement

    #region Jump
    void JumpAndGravity() {
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        if (characterController.isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
        if (jumpRequest && characterController.isGrounded) {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            jumpRequest = false;
        }
    }
    void OnJump() {
        if (input.jumpValue && characterController.isGrounded) {
            jumpRequest = true;
        }
    }

    #endregion Jump

    #region Camara
    private void CamaraRotation() {
        xRotation += input.look.y;
        yRotation += input.look.x;

        xRotation = Mathf.Clamp(xRotation, -30, 70);
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
        cameraFollowTarget.rotation = rotation;
    }

    #endregion Camara
}