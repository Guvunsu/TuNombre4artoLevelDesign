using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour {

    // HACER UNA VARIABLE PARA LIMITAR EL NUMERO DE SALTOS 
    #region ENUM
    public enum Player_FSM {
        IDLE,
        MOVING,
        JUMPING,
        TAKE_IT,
        STOP
    }

    public enum GameState {
        PLAYING,
        WIN,
        LOSE
    }

    #endregion ENUM

    #region VARIABLES
    [SerializeField] protected Player_FSM fsm;
    [SerializeField] protected GameState gameFSM;

    [SerializeField] GameObject panelWin;
    [SerializeField] GameObject panelLose;
    [SerializeField] Rigidbody rb;

    protected Vector3 direction;

    [SerializeField] float jumpForce;
    [SerializeField] float moveSpeed;
    [SerializeField] float accelerationSpeed = 1f;
    [SerializeField] float timePassed = 0.5f;

    #endregion VARIABLES

    #region PublicUnityMethods

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }
    void Start() {
        fsm = Player_FSM.IDLE;
    }

    void FixedUpdate() {
        switch (fsm) {
            case Player_FSM.MOVING:
                moveSpeed += Time.fixedDeltaTime * accelerationSpeed;
                if (moveSpeed >= 0.35f) {
                    moveSpeed -= 0.05f;
                }
                transform.Translate(direction * moveSpeed * Time.fixedDeltaTime);
                break;
            case Player_FSM.JUMPING:
                if (rb.velocity.y == 0) {
                    fsm = Player_FSM.IDLE;
                }
                break;
            case Player_FSM.TAKE_IT:
                // nothing 
                break;
            case Player_FSM.STOP:
                if (moveSpeed > 0f) {
                    moveSpeed -= Time.fixedDeltaTime * accelerationSpeed;
                } else {
                    moveSpeed = 0f;
                }
                break;
        }
    }

    #endregion PublicUnityMethods

    #region PublicMethods
    public void MovePlayerDoll(InputAction.CallbackContext value) {
        if (value.performed && gameFSM == GameState.PLAYING) {
            direction = value.ReadValue<Vector3>();
            fsm = Player_FSM.MOVING;
        } else if (value.canceled && gameFSM == GameState.PLAYING) {
            fsm = Player_FSM.STOP;
            if (fsm == Player_FSM.STOP) {
                timePassed = Time.fixedDeltaTime;
                fsm = Player_FSM.IDLE;
            }
        }
    }

    public void JumpPlayerDoll(InputAction.CallbackContext value) {
        if (value.performed && fsm != Player_FSM.JUMPING) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            fsm = Player_FSM.JUMPING;
        }
    }

    public void InteractionPlayerDoll(InputAction.CallbackContext value) {
        if (value.performed) {
            fsm = Player_FSM.TAKE_IT;
        }
    }

    #endregion PublicMethods

    #region Collisions
    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Candado") && other.gameObject.CompareTag("KeyVitrina")) {
            fsm = Player_FSM.TAKE_IT;
            gameFSM = GameState.WIN;
        }
    }

    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("BowlBall") ||
          collision.gameObject.CompareTag("BomberTruck") ||
          collision.gameObject.CompareTag("BowlPine") ||
          collision.gameObject.CompareTag("BasketBall") ||
          collision.gameObject.CompareTag("CubeWood") ||
          collision.gameObject.CompareTag("KeyVitrina")) {
            fsm = Player_FSM.TAKE_IT;
        }
        if (collision.gameObject.CompareTag("BomberTruck") && collision.gameObject.CompareTag("WallBlock")) {
            Destroy(collision.gameObject, 1.1f);
        }
        if (collision.gameObject.CompareTag("BaseCubeWood") && collision.gameObject.CompareTag("CubeWood")) {

        }
    }

    #endregion Collisions

    #region Victory&Lose Paneles
    public void VictoryPanel() {
        if (gameFSM != GameState.PLAYING) {
            return;
        }
        gameFSM = GameState.WIN;
        panelWin.SetActive(true);
        fsm = Player_FSM.STOP;
        panelLose.SetActive(false);
    }
    public void LosePanel() {
        if (gameFSM != GameState.PLAYING) {
            return;
        }
        gameFSM = GameState.LOSE;
        panelLose.SetActive(true);
        fsm = Player_FSM.STOP;
        panelWin.SetActive(false);
    }

    #endregion Victory%Lose Paneles
}