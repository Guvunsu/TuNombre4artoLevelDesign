using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowlingBallForce : MonoBehaviour {
    [SerializeField] float moveSpeed;
    bool iTouchedPines = false;

    Vector3 direction;

    [SerializeField] Transform bowlingPines;
    GameObject bowlingBallGO;
    Rigidbody bowlingBallRB;

    void Update() {
        MovBowlingBallForwardInteract();
    }
    public void MovBowlingBallForwardInteract() {
        if (Input.GetKeyDown(KeyCode.E)) {
            bowlingBallRB = GetComponent<Rigidbody>();
            Debug.Log("ejecuto E para EL BOLICHE");
            if (bowlingBallRB != null) {
                direction = (bowlingPines.position - transform.position).normalized;
                Debug.Log("avance a la direccion correcta? para EL BOLICHE");
                bowlingBallRB.AddForce(direction * moveSpeed * Time.deltaTime, ForceMode.Impulse);
                Debug.Log("le agrego la fuerza y velocidad, para EL BOLICHE");
            }
        }
    }
    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("BowlingBall")) {
            iTouchedPines = true;
            MovBowlingBallForwardInteract();
            Destroy(collision.gameObject, 5f);
        }
    }
}
