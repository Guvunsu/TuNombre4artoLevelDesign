using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballGame : MonoBehaviour {
    #region Variables
    [SerializeField] float throwBallForce = 18f;
    [SerializeField] float interactDistance = 2.5f; // Distancia mínima para lanzar
    [SerializeField] Transform playerTransform;     // Asigna el jugador en el Inspector
    [SerializeField] Transform basketBasketballTransform;

    Rigidbody ballBasketballRB;
    bool hasBeenThrown = false;

    [SerializeField] GameObject basketBasketballGO;
    Vector3 direction;
    #endregion Variables

    #region PublicMethods
    void Start() {
        ballBasketballRB = GetComponent<Rigidbody>();
    }

    void Update() {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (!hasBeenThrown && distanceToPlayer <= interactDistance && Input.GetKeyDown(KeyCode.E)) {
            ThrownBallBasketball();
        }
    }
    #endregion PublicMethods

    #region ThrownBall
    public void ThrownBallBasketball() {
        Vector3 targetOffset = new Vector3(0f, 1.5f, 0f);
        Vector3 finalTarget = basketBasketballTransform.position + targetOffset;

        direction = (finalTarget - transform.position).normalized + Vector3.up * 0.5f;

        ballBasketballRB.AddForce(direction * throwBallForce, ForceMode.Impulse);

        hasBeenThrown = true;
        Debug.Log("Lancé la pelota con parábola hacia el aro.");
    }
    #endregion ThrownBall

    #region CollisionDestroy
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Canasta")) {
            Debug.Log("¡Anotaste!");
            StartCoroutine(DestroyAfterSeconds(2f));
        } else if (collision.gameObject.CompareTag("Vitrina")) {
            hasBeenThrown = false;
            Debug.Log("Fallaste, puedes lanzar de nuevo");
        }
    }

    IEnumerator DestroyAfterSeconds(float seconds) {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
    #endregion CollisionDestroy
}
