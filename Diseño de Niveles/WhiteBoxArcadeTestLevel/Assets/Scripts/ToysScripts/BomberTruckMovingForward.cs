using UnityEngine;

public class BomberTruckMovingForward : MonoBehaviour {
    [SerializeField] private float moveSpeed = 20f;

    private bool shouldMove = false;
    private Vector3 moveDirection = Vector3.forward; // Puedes cambiar la dirección aquí

    void Update() {
        if (shouldMove) {
            transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;
        }
    }

    // Llamar a esta función desde otro script cuando se suelte el objeto
    public void StartMoving() {
        shouldMove = true;
        Debug.Log("El BomberTruck ha comenzado a moverse.");
    }
}
