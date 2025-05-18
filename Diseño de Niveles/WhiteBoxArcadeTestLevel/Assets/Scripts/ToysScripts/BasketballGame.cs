using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballGame : MonoBehaviour {
    #region Variables
    [SerializeField] float throwBallForce;

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
        if (!hasBeenThrown && Input.GetKeyDown(KeyCode.E))
            ThrownBallBasketball();
    }
    #endregion PublicMethods

    #region ThrownBall
    public void ThrownBallBasketball() {
        direction = (basketBasketballTransform.position - transform.position).normalized + Vector3.up * 0.666f;
        ballBasketballRB.AddForce(direction * throwBallForce * Time.deltaTime, ForceMode.Impulse);
        hasBeenThrown = true;
        Debug.Log("Lance la pelota");
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
    #endregion  CollisionDestroy
}
