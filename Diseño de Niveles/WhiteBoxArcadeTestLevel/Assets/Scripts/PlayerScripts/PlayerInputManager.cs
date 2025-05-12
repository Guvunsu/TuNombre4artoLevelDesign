using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour {
    #region Variables
    [Header("Inputs")]
    public Vector2 move;
    public Vector2 look;
    public bool pickedUp;
    public bool jumpValue;
    #endregion Variables

    #region PublicMethods
    //Es como la Sliver Queen pero necesita uno de sus combos para ganar con ella, solo que esto ara que se mueva. P.D. recordatorio para alguien muy pacheco que te quiere <3
    //PlayerInput mediante "Send Messages"
    void OnMove(InputValue value) {
        move = value.Get<Vector2>();
    }
    void OnLook(InputValue value) {
        look = value.Get<Vector2>();
    }
    void OnPickUp(InputValue value) {
        pickedUp = value.isPressed; //value.Get<bool>();
    }

    void OnJump(InputValue value) {
        jumpValue = value.isPressed;
    }
    #endregion PublicMethods
}
