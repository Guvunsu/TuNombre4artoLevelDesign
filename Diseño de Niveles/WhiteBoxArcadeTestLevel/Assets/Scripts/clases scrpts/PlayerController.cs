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

#region Codigo Clase 22/04/2025 COMENTADO
//    PlayerInputManager input;
//    CharacterController characterController;
//    [SerializeField] Transform cameraFollowTarget;
//    [SerializeField] GameObject playerVcam;

//    float xRotation;
//    float yRotation;
//    //[SerializeField] private float speed;
//    [SerializeField] private float moveSpeed;
//    // Start is called before the first frame update
//    void Start() {
//        input = GetComponent<PlayerInputManager>();
//        characterController = GetComponent<CharacterController>();
//    }

//    // Update is called once per frame
//    void Update() {
//        float speed = 0;
//        Vector3 moveInput = new Vector3(input.move.x, 0, input.move.y);
//        Vector3 moveDirection = new Vector3(input.move.x, 0, input.move.y);
//        float targetRotation = 0f;
//        //Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
//        if (input.move != Vector2.zero) {
//            speed = moveSpeed;
//            targetRotation = Quaternion.LookRotation(moveInput).eulerAngles.y + playerVcam.transform.rotation.eulerAngles.y;
//            Quaternion rotation = Quaternion.Euler(0, targetRotation, 0);
//            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 20 * Time.deltaTime);
//            characterController.Move(input.move * speed * Time.deltaTime);
//        }
//        Vector3 targetDurectioin = Quaternion.Euler(0, targetRotation, 0) * Vector3.forward;
//        characterController.Move(targetDurectioin * speed * Time.deltaTime);
//    }
//    private void CamaraRotation() {
//        xRotation += input.look.y;
//        xRotation += input.look.x;

//        xRotation = Mathf.Clamp(xRotation, -30, 70f);
//        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
//        cameraFollowTarget.rotation = rotation;
//    }
//}
#endregion Codigo Clase 22/04/2025 COMENTADO
public class PlayerController : MonoBehaviour {

    #region ENUM
    public enum Player_FSM {
        IDLE,
        WALK_CAMARA,
        JUMP
    }
    public enum GameState_FSM {
        START,
        PLAYING,
        LOSE,
        WIN //HACER UNA FUNCION EN EL CUAL EN EL PICKUPCONTROLLER UN FSM VINCULADAA A UNA FUNCION QUE SI TENGO LA LLAVE Y EL CANDADO GANE
    }
    #endregion ENUM

    #region Variables
    [Header("FSM")]
    public Player_FSM fsm_Player;
    public GameState_FSM fsm_GameState;

    [Header("Vectores")]
    public Vector3 moveInput;
    public Vector3 moveDirection;
    public Vector2 lookInput;

    [Header("Scripts")]
    PlayerInputManager script_PlayerInputManager;

    [Header("Componentes")]
    CharacterController characterController;

    //[Header("Gravedad")]
    //[SerializeField] float gravity = -9.81f;
    //[SerializeField] float verticalVelocity = 0f;
    //[SerializeField] float groundCheckDistance = 0.1f;
    //[SerializeField] LayerMask groundLayer;
    //Rigidbody rb;

    [Header("Movimiento del Jugador")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSmoothTime = 0.1f;
    [SerializeField] float rotationVelocity;

    [Header("Salto del Jugador")]
    [SerializeField] float jumpForce = 5f;
    [SerializeField] bool isGrounded;

    [Header("Referencias de Camara")]
    [SerializeField] Transform cameraFollowTarget;
    [SerializeField] GameObject playerVcam;

    [Header("Rotación de Camara")]
    [SerializeField] float xRotation;
    [SerializeField] float yRotation;

    #endregion Variables

    #region UnityMethods
    void Awake() {
        //rb.isKinematic = true;
        characterController = GetComponent<CharacterController>();
        script_PlayerInputManager = GetComponent<PlayerInputManager>();
    }

    void Start() {
        fsm_GameState = GameState_FSM.START;
        fsm_Player = Player_FSM.IDLE;
    }

    void Update() {
        switch (fsm_GameState) {
            case GameState_FSM.START: break;
            case GameState_FSM.LOSE:
                moveSpeed = 0;
                break;
            case GameState_FSM.PLAYING:
                MoveWithCameraDirection();
                break;
        }
    }

    void FixedUpdate() {
        //ApplyGravity(); //ESTA EN PrivateMethods
        moveInput = script_PlayerInputManager.move;
        lookInput = script_PlayerInputManager.look;

        switch (fsm_Player) {
            case Player_FSM.WALK_CAMARA:
                fsm_GameState = GameState_FSM.PLAYING;
                CamaraRotation();
                break;

            case Player_FSM.JUMP:
                fsm_GameState = GameState_FSM.PLAYING;
                if (characterController.isGrounded) {
                    fsm_Player = Player_FSM.IDLE;
                }
                break;

            case Player_FSM.IDLE:
                fsm_GameState = GameState_FSM.PLAYING;
                break;
        }
    }
    #endregion UnityMethods

    #region PrivateMethods
    ////Aplicar Gravedad al usuario por la via de rb.iskinematic. Recordatorio para un futuro muy lejano y marihuano que te quiere  <3
    //void ApplyGravity() {
    //    bool isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

    //    if (isGrounded && verticalVelocity < 0) {
    //        verticalVelocity = -2f;
    //    } else {
    //        verticalVelocity += gravity * Time.deltaTime;
    //    }

    //    Vector3 gravityMove = new Vector3(0, verticalVelocity, 0);
    //    rb.MovePosition(rb.position + gravityMove * Time.deltaTime);
    //}

    #endregion PrivateMethods

    #region PublicMethods

    #region Player
    public void MoveWithCameraDirection() {
        if (fsm_Player == Player_FSM.IDLE || fsm_Player == Player_FSM.WALK_CAMARA) {
            if (moveInput.sqrMagnitude >= 0.01f) {
                Vector3 inputDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
                Vector3 moveDir = Quaternion.Euler(0f, cameraFollowTarget.eulerAngles.y, 0f) * inputDirection;
                characterController.Move(moveDir * moveSpeed * Time.deltaTime);

                float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
                float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, rotationSmoothTime);
                transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

                fsm_Player = Player_FSM.WALK_CAMARA;
            } else {
                fsm_Player = Player_FSM.IDLE;
            }
        }
    }

    public void JumpPlayerDoll() {
        if (characterController.isGrounded && fsm_Player != Player_FSM.JUMP) {
            fsm_Player = Player_FSM.JUMP;
        }
    }
    #endregion Player

    #region Camara
    public void CamaraRotation() {
        lookInput = script_PlayerInputManager.look;

        xRotation += lookInput.y;
        yRotation -= lookInput.x;

        xRotation = Mathf.Clamp(xRotation, -30, 70);
        cameraFollowTarget.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
    #endregion Camara

    #region Calling Scrip_PlayerInputManager
    // Estos métodos serán llamados desde PlayerInputManager mediante "Send Messages"
    public void OnMove(InputValue value) {
        script_PlayerInputManager.OnMove(value);
    }

    public void OnLook(InputValue value) {
        script_PlayerInputManager.OnLook(value);
    }

    public void OnJump(InputValue value) {
        script_PlayerInputManager.OnJump(value);
        if (script_PlayerInputManager.jump) {
            JumpPlayerDoll();
        }
    }

    public void OnPickUp(InputValue value) {
        script_PlayerInputManager.OnPickUp(value);
    }
    #endregion Calling Scrip_PlayerInputManager

    #endregion PublicMethods

}