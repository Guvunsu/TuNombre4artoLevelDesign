using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour {
    [SerializeField] private Rigidbody itemRB;
    [SerializeField] private Collider itemCollider;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform container;
    [SerializeField] private float pickUpRange = 3f;
    [SerializeField] private float throwForce = 5f;

    public static bool slotFull;
    private bool equipped;

    void Update() {
        Vector3 distanceToPlayer = playerTransform.position - transform.position;

        if (!equipped && distanceToPlayer.magnitude <= pickUpRange) {
            GetComponent<Renderer>().material.color = Color.magenta;

            if (Input.GetKeyDown(KeyCode.E) && !slotFull) {
                PickUp();
            }
        } else if (!equipped) {
            GetComponent<Renderer>().material.color = Color.red;
        }

        if (equipped && Input.GetKeyDown(KeyCode.Q)) {
            Drop();
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
            Vector3 throwDirection = playerTransform.forward + Vector3.up * 0.5f;
            itemRB.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }

        if (CompareTag("BomberTruck")) {
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
        if (CompareTag("BomberTruck") && collision.gameObject.CompareTag("WallBlock")) {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (CompareTag("BowlBall") && collision.gameObject.CompareTag("BowlPine")) {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
