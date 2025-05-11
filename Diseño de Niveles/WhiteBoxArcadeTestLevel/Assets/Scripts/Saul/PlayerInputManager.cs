using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public Vector2 directionMove;
    public Vector2 directionLook;
    //public bool pickedUp;
    public bool jumpValue;
    void OnMove(InputValue value)
    {
        directionMove  = value.Get<Vector2>();
    }
    void OnLook(InputValue value)
    {
        directionLook = value.Get<Vector2>();
    }
    void OnPickUp(InputValue value)
    {
        //pickedUp = value.Get<bool>();
    }

    void OnJump(InputValue value)
    {
        jumpValue = value.isPressed; 
    }
}
