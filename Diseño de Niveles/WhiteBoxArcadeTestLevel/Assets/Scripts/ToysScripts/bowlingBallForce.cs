using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowlingBallForce : MonoBehaviour {
    #region Variables
    [SerializeField] float moveSpeed = 10f;
    bool iTouchedPines = false;

    Vector3 direction;

    [SerializeField] Transform bowlingPines;
    GameObject bowlingBallGO;
    Rigidbody bowlingBallRB;

    #endregion Variables

    #region PublicMethods
    void Start() {
        bowlingBallRB = GetComponent<Rigidbody>();
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.E))
            MovBowlingBallForwardInteract();
    }

    #endregion PublicMethods

    #region MoveBall
    public void MovBowlingBallForwardInteract() {
        direction = (bowlingPines.position - transform.position).normalized;
        Debug.Log("avance a la direccion correcta? para EL BOLICHE");
        bowlingBallRB.AddForce(direction * moveSpeed * Time.deltaTime, ForceMode.Impulse);
        Debug.Log("le agrego la fuerza y velocidad, para EL BOLICHE");
    }

    #endregion MoveBall

    #region CollsionDestroy
    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("BowlPine")) {
            StartCoroutine(DestroyAfterSeconds(6f));
        } 
    }
    IEnumerator DestroyAfterSeconds(float seconds) {
        yield return new WaitForSeconds(seconds);
        //Destroy(gameObject);
    }
    #endregion CollsionDestroy
}
