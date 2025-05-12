using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;
using UnityEngine.Windows;

using static PlayerController;
#region NOTAS Y PROPOSITOS
// implementar el salto 
// arreglar el movimiento y experimentar
#endregion NOTAS Y PROPOSITOS

public class PlayerController : MonoBehaviour
{
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

    void Start()
    {
        input = GetComponent<PlayerInputManager>();
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        MovePlayer();
        JumpAndGravity();
        ///*CamaraRotation*/();
    }
    void MovePlayer()
    {
        float speed = 0;
        Vector3 moveInput = new Vector3(input.move.x, 0, input.move.y);
        Vector3 moveDirection = new Vector3(input.move.x, 0, input.move.y);
        float targetRotation = 0f;
        //Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        if (input.move != Vector2.zero)
        {
            speed = moveSpeed;
            targetRotation = Quaternion.LookRotation(moveInput).eulerAngles.y + playerVcam.transform.rotation.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetRotation, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 20 * Time.deltaTime);
            //characterController.Move(input.move * speed * Time.deltaTime);
        }
        Vector3 targetDurectioin = Quaternion.Euler(0, targetRotation, 0) * Vector3.forward;
        characterController.Move(targetDurectioin * speed * Time.deltaTime);
    }
    void JumpAndGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (jumpRequest && characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            jumpRequest = false;
        }
    }
    //void OnJump()
    //{
    //    if (input.jumpValue && characterController.isGrounded)
    //    {
    //        jumpRequest = true;
    //    }
    //}
    private void CamaraRotation()
    {
        xRotation += input.look.y;
        yRotation += input.look.x;

        xRotation = Mathf.Clamp(xRotation, -30, 70);
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
        cameraFollowTarget.rotation = rotation;
    }
}