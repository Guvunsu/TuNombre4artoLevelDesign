//using UnityEngine;
//using System.Collections;

//public class PickupController : MonoBehaviour {
//    bowlingBallForce scrip_bowlingBallForce;
//    BomberTruckMovingForward script_BomberTruckMovingForward;
//    BasketballGame script_BasketballGame;
//    KeyOpenShowCase script_KeyOpenShowCase;

//    [SerializeField] private Rigidbody itemRB;
//    [SerializeField] private Collider itemCollider;
//    [SerializeField] private Transform playerTransform;
//    [SerializeField] private Transform container;

//    [SerializeField] private Transform targetToUnlock;
//    [SerializeField] private GameObject targetToDisableCollider;
//    bool hasInteracted = false;

//    [SerializeField] private float pickUpRange = 3f;
//    [SerializeField] private float throwForce = 5f;

//    public static bool slotFull;
//    private bool equipped;
//    Vector3 originalScale;

//    void Update() {
//        PickUp();
//        Drop();
//        //Vector3 distanceToPlayer = playerTransform.position - transform.position;
//        //Debug.LogWarning("Update - Distance to player " + distanceToPlayer);
//        //if (!equipped && distanceToPlayer.magnitude <= pickUpRange) {
//        //    GetComponent<Renderer>().material.color = Color.magenta;
//        //    Debug.LogWarning("Not equipped and distance in pick up range");
//        //    if (Input.GetKeyDown(KeyCode.E) && !slotFull) {
//        //        Debug.LogWarning("Pickup validated!!!!!");
//        //        Interact(this.gameObject, targetToDisableCollider);
//        //        hasInteracted = true;
//        //        PickUp();
//        //    }
//        //} else if (!equipped) {
//        //    GetComponent<Renderer>().material.color = Color.red;
//        //}

//        //if (equipped && Input.GetKeyDown(KeyCode.Q)) {
//        //    Drop();
//        //}
//    }

//    public virtual void Interact(GameObject sourceObject, GameObject targetToDisableCollider) {
//        if (sourceObject.CompareTag("KeyVitrina") && targetToDisableCollider != null) {
//            Collider targetCollider = targetToDisableCollider.GetComponent<Collider>();
//            if (targetCollider != null) {
//                targetCollider.enabled = false;
//                Debug.Log("¡Puerta/Vitrina desbloqueada!");
//            }
//        }
//    }
//    private void PickUp() {
//        equipped = true;
//        slotFull = true;
//        GetComponent<Renderer>().material.color = Color.white;

//        //itemRB.isKinematic = true;
//        //itemCollider.isTrigger = true;

//        transform.SetParent(container);
//        transform.localPosition = Vector3.zero;
//        transform.localRotation = Quaternion.identity;
//        transform.localScale = originalScale;
//    }
//    private void Drop() {
//        equipped = false;
//        slotFull = false;

//        transform.SetParent(null);
//        itemRB.isKinematic = false;
//        itemCollider.isTrigger = false;

//        if (CompareTag("BowlBall")) {
//            scrip_bowlingBallForce.MovBowlingBallForwardInteract();
//        }
//        if (CompareTag("BomberTruck")) {
//            script_BomberTruckMovingForward.MoveCarForwarWithInteract();
//        }
//        if (CompareTag("Basketball")) {
//            script_BasketballGame.ThrownBallBasketball();
//        }
//    }
//    private void OnCollisionEnter(Collision collision) {
//        if (collision.gameObject.CompareTag("KeyVitrina")) {
//            Debug.Log("Obtuve la llave y ahorra el box collider de la vitrina del centro se desactivo");
//            foreach (BoxCollider bc in targetToUnlock.GetComponentsInChildren<BoxCollider>()) {
//                bc.enabled = false;
//            }
//            gameObject.SetActive(false);
//        }
//    }
//}
using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour {
    bowlingBallForce scrip_bowlingBallForce;
    BomberTruckMovingForward script_BomberTruckMovingForward;
    BasketballGame script_BasketballGame;
    KeyOpenShowCase script_KeyOpenShowCase;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform container;

    [SerializeField] private Transform targetToUnlock;
    [SerializeField] private GameObject targetToDisableCollider;
    bool hasInteracted = false;

    [SerializeField] private float pickUpRange = 3f;
    [SerializeField] private float throwForce = 5f;

    public static bool slotFull;
    private bool equipped;
    Vector3 originalScale;

    void Start() {
        originalScale = transform.localScale;

        // Asignaciones de scripts (si están en el mismo GameObject o ajustar si están en otros)
        scrip_bowlingBallForce = GetComponent<bowlingBallForce>();
        script_BomberTruckMovingForward = GetComponent<BomberTruckMovingForward>();
        script_BasketballGame = GetComponent<BasketballGame>();
        script_KeyOpenShowCase = GetComponent<KeyOpenShowCase>();
    }
    void Update() {
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (!equipped && distance <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) {
            Interact(this.gameObject, targetToDisableCollider);
            hasInteracted = true;
            PickUp();
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

        if (!CompareTag("BowlBall") && !CompareTag("BomberTruck") && !CompareTag("BasketBall") && !CompareTag("KeyVitrina"))
            return;

        Vector3 worldScale = transform.lossyScale;

        transform.SetParent(container);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        // Reaplicar escala global
        transform.localScale = Vector3.one;
        transform.localScale = new Vector3(
            worldScale.x / container.lossyScale.x,
            worldScale.y / container.lossyScale.y,
            worldScale.z / container.lossyScale.z
        );
    }

    private void Drop() {
        equipped = false;
        slotFull = false;

        transform.SetParent(null);

        if (CompareTag("BowlBall") && scrip_bowlingBallForce != null) {
            scrip_bowlingBallForce.ThrowBall();
        }
        if (CompareTag("BomberTruck") && script_BomberTruckMovingForward != null) {
            script_BomberTruckMovingForward.StartMoving();
        }
        if (CompareTag("BasketBall") && script_BasketballGame != null) {
            script_BasketballGame.ThrownBallBasketball();
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("KeyVitrina")) {
            Debug.Log("Obtuve la llave y ahorra el box collider de la vitrina del centro se desactivo");
            foreach (BoxCollider bc in targetToUnlock.GetComponentsInChildren<BoxCollider>()) {
                bc.enabled = false;
            }
            gameObject.SetActive(false);
        }
    }
}