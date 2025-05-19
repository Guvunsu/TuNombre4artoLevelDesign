using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowlingBallForce : MonoBehaviour {
    #region Variables
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float interactDistance = 2.5f;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform bowlingPines;
    [SerializeField] Transform handTransform;

    Rigidbody bowlingBallRB;
    Collider ballCollider;
    bool isHeld = false;
    bool hasBeenThrown = false;
    Vector3 direction;
    #endregion Variables

    #region UnityMethods
    void Start() {
        bowlingBallRB = GetComponent<Rigidbody>();
        ballCollider = GetComponent<Collider>();
    }

    void Update() {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (!isHeld && !hasBeenThrown && distanceToPlayer <= interactDistance && Input.GetKeyDown(KeyCode.E)) {

            // Previene tomar múltiples bolas si ya hay una en mano (ChatGPT)
            bowlingBallForce existingBall = FindObjectOfType<bowlingBallForce>();
            if (existingBall != null && existingBall.isHeld)
                return;

            PickUpBall();
        }
        if (isHeld && Input.GetKeyDown(KeyCode.Q)) {
            ThrowBall();
        }
        if (isHeld && !hasBeenThrown) {
            transform.position = handTransform.position;
        }
    }
    #endregion UnityMethods

    #region BallLogic
    public void PickUpBall() {
        isHeld = true;
        bowlingBallRB.isKinematic = true;
        bowlingBallRB.useGravity = false;

        transform.SetParent(handTransform);
        ballCollider.enabled = false; 

        Debug.Log("Bola recogida.");
    }
    public void ThrowBall() {
        isHeld = false;
        hasBeenThrown = true;

        transform.SetParent(null); 
        transform.position = handTransform.position;
        bowlingBallRB.isKinematic = false;
        bowlingBallRB.useGravity = true;
        ballCollider.enabled = true; 

        direction = (bowlingPines.position - transform.position).normalized;
        bowlingBallRB.AddForce(direction * moveSpeed, ForceMode.Impulse);

        Debug.Log("¡Bola lanzada!");
    }
    #endregion BallLogic

    #region CollisionDestroy
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("BowlPine")) {
            StartCoroutine(DestroyAfterSeconds(6f));
        }
    }
    IEnumerator DestroyAfterSeconds(float seconds) {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
    #endregion CollisionDestroy
}
