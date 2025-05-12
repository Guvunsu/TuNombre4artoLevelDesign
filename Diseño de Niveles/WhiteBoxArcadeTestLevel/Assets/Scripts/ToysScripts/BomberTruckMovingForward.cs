using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberTruckMovingForward : MonoBehaviour {
    PlayerInputManager scrip_PlayerInputManager;

    [SerializeField] float moveSpeed;
    bool isNearMyBomberTruckOrMyDoggie = false;

    Vector3 direction;

    [SerializeField] Transform BlockWallTransform;
    GameObject bomberTruckGO;
    Rigidbody bomberTruckRB;
    void Update() {
        if (isNearMyBomberTruckOrMyDoggie && Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("ejecuto E para LOS VEHICULOS");
            if (bomberTruckRB != null) {
                direction = (BlockWallTransform.position - bomberTruckRB.transform.position).normalized;
                Debug.Log("avance a la direccion correcta?para LOS VEHICULOS");
                bomberTruckRB.AddForce(direction * moveSpeed * Time.deltaTime, ForceMode.Impulse);
                Debug.Log("le agrego la fuerza y velocidad,para LOS VEHICULOS");
            }
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("BomberTruck")) {
            isNearMyBomberTruckOrMyDoggie = true;
            bomberTruckGO = collision.gameObject;
            bomberTruckRB = bomberTruckGO.GetComponent<Rigidbody>();
            Debug.Log("estoy cerca del BomberTruck y listo para moverlo.");
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("BomberTruck")) {
            isNearMyBomberTruckOrMyDoggie = false;
            bomberTruckGO = null;
            bomberTruckRB = null;
            Debug.Log("me alejé del BomberTruck.");
        }
    }
}
