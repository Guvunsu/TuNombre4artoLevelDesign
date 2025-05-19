using UnityEngine;

public class BomberTruckMovingForward : MonoBehaviour {
    [SerializeField] float moveSpeed = 18f;

    bool shouldMove = false;
    Vector3 moveDirection = Vector3.forward;

    void Update() {
        if (shouldMove) {
            transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
        }
    }
    public void StartMoving() {
        shouldMove = true;
        Debug.Log("El BomberTruck ha comenzado a moverse.");
    }
}
