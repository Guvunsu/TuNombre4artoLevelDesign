using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
// acomodar el codigo de paolade clase en funciones y como yo programaria
//agregar un esatdo finito deñ juego para bloqeuar movimiento cuando pierdes y si estas vivo y no haz perdido sigues jugando
// implementar el salto 
// arreglar el movimiento y experimentar

public class PlayerController : MonoBehaviour {
    #region ENUM
    public enum Player_FSM {
        STOP,
        WALK,
        JUMP,
        CAMARA
    }
    public enum GameSate_FSM {
        START,
        PLAYING,
        LOSE,
        WIN //HACER UNA FUNCION EN EL CUAL EN EL PICKUPCONTROLLER UN FSM VINCULADAA A UNA FUNCION QUE SI TENGO LA LLAVE Y EL CANDADO GANE WWWW
    }
    #endregion ENUM

    #region Variables
    [Header("FINITE STATE MECHANICS")]
    Player_FSM fsm_Player;
    GameSate_FSM fsm_GameState;

    [SerializeField] PlayerInputManager script_PlayerInputManager;
    [SerializeField] CharacterController characterController;

    Vector3 inputDirection;
    Vector3 targetDirection;

    [Header("Movimiento del Jugador")]
    [SerializeField] float moveSpeed;
    [SerializeField] private float rotationSmoothTime = 0.1f;
    float rotationVelocity;

    [Header("Referencias de Camara")]
    [SerializeField] Transform cameraFollowTarget;
    [SerializeField] GameObject playerVcam;

    [Header("Rotación de Camara")]
    float xRotation;
    float yRotation;

    #endregion Variables

    #region UnityMethods
    void Start() {
        script_PlayerInputManager = GetComponent<PlayerInputManager>();
        characterController = GetComponent<CharacterController>();
        fsm_GameState = GameSate_FSM.START;
        fsm_Player = Player_FSM.STOP;
    }
    void Update() {
        switch (fsm_GameState) {
            case GameSate_FSM.START:

                break;
            case GameSate_FSM.LOSE:

                break;
            case GameSate_FSM.PLAYING:

                break;
        }
    }
    void FixedUpdate() {
        switch (fsm_Player) {
            case Player_FSM.WALK:

                break;
            case Player_FSM.CAMARA:
                CamaraRotation();
                break;
            case Player_FSM.JUMP:

                break;
            case Player_FSM.STOP:

                break;
        }
    }

    #endregion UnityMethods

    #region ProtectedMethdos

    protected void OnMoveOnLookCamaraAndDirectionTogetherAreEachOne() {
        //jugador input & mov
        float speed = 0;
        Vector3 inputDirection = new Vector3(script_PlayerInputManager.move.x, 0f, script_PlayerInputManager.move.y).normalized;
        inputDirection = Quaternion.Euler(0f, playerVcam.transform.eulerAngles.y, 0f) * inputDirection;
        inputDirection.Normalize();
        float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, rotationSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        characterController.Move(inputDirection * speed * Time.fixedDeltaTime);

        this.targetDirection = new Vector3(script_PlayerInputManager.move.x, 0f, script_PlayerInputManager.move.y);
        float targetRotation = 0f;

        //camara
        if (script_PlayerInputManager.move != Vector2.zero) {
            speed = moveSpeed;
            targetRotation = Quaternion.LookRotation(inputDirection).eulerAngles.y + playerVcam.transform.rotation.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetRotation, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 20 * Time.fixedDeltaTime);
            characterController.Move(script_PlayerInputManager.move * speed * Time.fixedDeltaTime);
        }//movimiento
        Vector3 targetDirection = Quaternion.Euler(0, targetRotation, 0) * Vector3.forward;
        characterController.Move(targetDirection * speed * Time.fixedDeltaTime);
        //aqui hay un bug de doble move, hayq ue comentar una para ver cual es el que hace el ruido
    }

    #endregion ProtectedMethdos

    #region PrivateMethods
    private void CamaraRotation() {
        // xRotation += script_PlayerInputManager.look.y; //deberia ser - para subir y bajar
        xRotation += script_PlayerInputManager.look.x; //der e izq

        xRotation = Mathf.Clamp(xRotation, -30, 70f);
        //Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
        cameraFollowTarget.rotation = Quaternion.Euler(xRotation,/*yRotation*/0, 0f); //tenia antes = rotation;
    }

    #endregion PrivateMethods
}
