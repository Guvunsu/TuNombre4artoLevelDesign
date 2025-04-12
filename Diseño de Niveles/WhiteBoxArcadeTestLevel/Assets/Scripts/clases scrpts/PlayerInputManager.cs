using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputManager : MonoBehaviour
{
    public Vector2 move;
    public Vector2 look;
    public bool pickedUp;

    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }
    private void OnLook(InputValue value)
    {
        look = value.Get<Vector2>();
    }
    void OnPickUp(InputValue value)
    {
        pickedUp = value.Get<bool>();
    }
}
