using UnityEngine;

public class CharacterPhysics : MonoBehaviour
{
    public CharacterController controller;
    public float gravity = -9.81f;

    Vector3 velocity;

    void Update()
    {
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }
    }
}