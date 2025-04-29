using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour {
    #region Variables
    [Header("Inputs")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool pickedUp;

    #endregion Variables

    #region PublicMethods
    //Es como la Sliver Queen pero necesita uno de sus combos para ganar con ella, solo que esto ara que se mueva. P.D. recordatorio para alguien muy pacheco que te quiere <3
    //PlayerInput mediante "Send Messages"
    public void OnMove(InputValue value) {
        move = value.Get<Vector2>();
    }
    public void OnJump(InputValue value) {
        jump = value.isPressed;
    }
    public void OnLook(InputValue value) {
        look = value.Get<Vector2>();
    }
    public void OnPickUp(InputValue value) {
        pickedUp = value.Get<bool>();
    }

    #endregion PublicMethods
}