using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballGame : MonoBehaviour {
    [SerializeField] float throwBallForce;

    [SerializeField] GameObject basketBasketballGO;
    [SerializeField] Transform basketBasketballTransform;
    GameObject ballBasketballGO;
    Rigidbody ballBasketballRB;

    Vector3 direction;
    bool point = false;
    bool hasBeenThrown = false;

    void Start() {
        ballBasketballRB = GetComponent<Rigidbody>();
    }
    void Update() {
        if (!hasBeenThrown && Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("aprete E para BASKETABALL pelota");
            if (ballBasketballRB != null) {
                direction = (basketBasketballTransform.position - transform.position).normalized + Vector3.up * 0.666f;
                ballBasketballRB.AddForce(direction * throwBallForce * Time.deltaTime, ForceMode.Impulse);
                hasBeenThrown = true;
                Debug.Log("Lance la pelota");
            }
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Basket")) {
            point = true;
            Debug.Log("¡Anotaste!");
        } else if (collision.gameObject.CompareTag("Ground")) {
            hasBeenThrown = false;
            Debug.Log("Fallaste, puedes lanzar de nuevo");
        }
    }
}
