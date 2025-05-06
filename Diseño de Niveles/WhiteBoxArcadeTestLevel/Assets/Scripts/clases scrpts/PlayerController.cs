using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using static PlayerController;
#region NOTAS Y PROPOSITOS
// acomodar el codigo de Paola de clase en funciones y como yo programaria
//agregar un esatdo finito deñ juego para bloqeuar movimiento cuando pierdes y si estas vivo y no haz perdido sigues jugando
// implementar el salto 
// arreglar el movimiento y experimentar
#endregion NOTAS Y PROPOSITOS

public class PlayerController : MonoBehaviour {
    PlayerInputManager input;
    CharacterController characterController;
    [SerializeField] Transform cameraFollowTarget;
    [SerializeField] GameObject playerVcam;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    bool isGrounded;
    [SerializeField] float gravity = -10f;
    [SerializeField] float jumpHeight = 2f;
    Vector3 velocity;

    float xRotation;
    float yRotation;
    [SerializeField] private float moveSpeed;

    void Start() {
        input = GetComponent<PlayerInputManager>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        MovePlayer();
    }
    void MovePlayer() {
        float speed = 0;
        Vector3 moveInput = new Vector3(input.move.x, 0, input.move.y);
        Vector3 moveDirection = new Vector3(input.move.x, 0, input.move.y);
        float targetRotation = 0f;
        //Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        if (input.move != Vector2.zero) {
            speed = moveSpeed;
            targetRotation = Quaternion.LookRotation(moveInput).eulerAngles.y + playerVcam.transform.rotation.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetRotation, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 20 * Time.deltaTime);
            //characterController.Move(input.move * speed * Time.deltaTime);
        }
        Vector3 targetDurectioin = Quaternion.Euler(0, targetRotation, 0) * Vector3.forward;
        characterController.Move(targetDurectioin * speed * Time.deltaTime);
    }
    void JumpAndGravity() {
        isGrounded = Physics.CheckSphere(groundCheck.position, .2f, groundLayer);
        if (isGrounded) {
            if (input.jump) {
                velocity.y = Mathf.Sqrt(jumpHeight * 2 * -gravity);
                input.jump = false;
            } else {
                velocity.y += gravity * Time.deltaTime;
            }
        }
        characterController.Move(velocity * Time.deltaTime);
    }
    private void CamaraRotation() {
        xRotation += input.look.y;
        yRotation += input.look.x;

        xRotation = Mathf.Clamp(xRotation, -30, 70);
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
        cameraFollowTarget.rotation = Quaternion.identity;
    }
}

