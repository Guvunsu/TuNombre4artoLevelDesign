using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour {
    bowlingBallForce scrip_bowlingBallForce;
    BomberTruckMovingForward script_BomberTruckMovingForward;
    BasketballGame script_BasketballGame;
    KeyOpenShowCase script_KeyOpenShowCase;

    [SerializeField] private Rigidbody itemRB;
    [SerializeField] private Collider itemCollider;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform container;

    [SerializeField] private Transform targetToUnlock;
    [SerializeField] private GameObject targetToDisableCollider;
    bool hasInteracted = false;

    [SerializeField] private float pickUpRange = 3f;
    [SerializeField] private float throwForce = 5f;

    public static bool slotFull;
    private bool equipped;

    void Update() {
        Vector3 distanceToPlayer = playerTransform.position - transform.position;
        Debug.LogWarning("Update - Distance to player " + distanceToPlayer);
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange) {
            GetComponent<Renderer>().material.color = Color.magenta;
            Debug.LogWarning("Not equipped and distance in pick up range");
            if (Input.GetKeyDown(KeyCode.E) && !slotFull) {
                Debug.LogWarning("Pickup validated!!!!!");
                Interact(this.gameObject, targetToDisableCollider);
                hasInteracted = true;
                PickUp();
            }
        } else if (!equipped) {
            GetComponent<Renderer>().material.color = Color.red;
        }

        if (equipped && Input.GetKeyDown(KeyCode.Q)) {
            Drop();
        }
    }

    public virtual void Interact(GameObject sourceObject, GameObject targetToDisableCollider) {
        if (sourceObject.CompareTag("KeyVitrina") && targetToDisableCollider != null) {
            Collider targetCollider = targetToDisableCollider.GetComponent<Collider>();
            if (targetCollider != null) {
                targetCollider.enabled = false;
                Debug.Log("¡Puerta/Vitrina desbloqueada!");
            }
        }
    }

    private void PickUp() {
        equipped = true;
        slotFull = true;
        GetComponent<Renderer>().material.color = Color.white;

        transform.SetParent(container);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        itemRB.isKinematic = true;
        itemCollider.isTrigger = true;
    }
    private void Drop() {
        equipped = false;
        slotFull = false;

        transform.SetParent(null);
        itemRB.isKinematic = false;
        itemCollider.isTrigger = false;

        if (CompareTag("BowlBall")) {
            scrip_bowlingBallForce.MovBowlingBallForwardInteract();
            Vector3 throwDirection = playerTransform.forward + Vector3.up * 0.5f;
            itemRB.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }
        if (CompareTag("BomberTruck")) {
            script_BomberTruckMovingForward.MoveCarForwarWithInteract();
            StartCoroutine(MoveForwardUntilCollision());
        }
        if (CompareTag("BallBasketball")) {
            script_BasketballGame.ThrownBallBasketball();
            StartCoroutine(MoveForwardUntilCollision());
        }
    }
    IEnumerator MoveForwardUntilCollision() {
        float speed = 20f;
        while (true) {
            transform.position += transform.forward * speed * Time.deltaTime;
            yield return null;
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            Debug.Log("Obtuve la llave y ahorra el box collider de la vitrina del centro se desactivo");
            foreach (BoxCollider bc in targetToUnlock.GetComponentsInChildren<BoxCollider>()) {
                bc.enabled = false;
            }
            gameObject.SetActive(false);
        }
    }
}
