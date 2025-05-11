using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField, HideInInspector] protected Rigidbody _rigidBody;
    [SerializeField] float _movementSpeed;
    float _rotationSpeed = 100f;
    private float _rotationX;
    [SerializeField, HideInInspector] protected Transform _cameraTransform;

    private bool _canJump = true;
    [SerializeField] float _jumpForce;


    private bool _isGrounded;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerRotation();
        CameraRotation();
        Jump();
    }
    void PlayerMovement()
    {
        float _horizontalInput = Input.GetAxis("Horizontal");
        float _verticalInput = Input.GetAxis("Vertical");
        Vector3 direccion = transform.forward * _verticalInput + transform.right * _horizontalInput;
        Vector3 movimiento = direccion.normalized * _movementSpeed;

        _rigidBody.velocity = new Vector3(movimiento.x, _rigidBody.velocity.y, movimiento.z);
    }

    void PlayerRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * _rotationSpeed * Time.fixedDeltaTime;

        Quaternion rotacion = Quaternion.Euler(0f, mouseX, 0f);
        _rigidBody.MoveRotation(_rigidBody.rotation * rotacion);
    }
    void CameraRotation()
    {
        float mouseY = Input.GetAxis("Mouse Y") * _rotationSpeed * Time.deltaTime;

        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);

        _cameraTransform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && _canJump)
        {
            _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _canJump = false; 
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Jumpable"))
        {
            _canJump = true;
        }
    }
}
