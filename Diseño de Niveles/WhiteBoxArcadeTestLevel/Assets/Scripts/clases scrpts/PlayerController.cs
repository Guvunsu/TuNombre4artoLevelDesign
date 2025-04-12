using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInputManager input;
    CharacterController characterController;
    [SerializeField] Transform cameraFollowTarget;
    [SerializeField] GameObject playerVcam;

    float xRotation;
    float yRotation;
    //[SerializeField] private float speed;
    [SerializeField] private float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<PlayerInputManager>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 0;
        Vector3 inputDirection = new Vector3(input.move.x, 0, input.move.y);
        Vector3 targetDirection = new Vector3(input.move.x, 0, input.move.y);
        float targetRotation = 0f;
        //Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        if (input.move != Vector2.zero)
        {
            speed = moveSpeed;
            targetRotation = Quaternion.LookRotation(inputDirection).eulerAngles.y + playerVcam.transform.rotation.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetRotation, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 20 * Time.deltaTime);
            characterController.Move(input.move * speed * Time.deltaTime);
        }
        Vector3 targetDurectioin = Quaternion.Euler(0, targetRotation, 0) * Vector3.forward;
        characterController.Move(targetDurectioin * speed * Time.deltaTime);
    }
    private void CamaraRotation()
    {
        xRotation += input.look.y;
        xRotation += input.look.x;

        xRotation = Mathf.Clamp(xRotation, -30, 70f);
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
        cameraFollowTarget.rotation = rotation;
    }
}
