using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//implementar el new sistem script_PlayerInputManager en las mecanicas de este script
// acomodar el codigo como yo programo
// implementar mis Collsisions y triggers de mi nivel para interactuar con los objetos 

// arreglar el bug de tomar algo, teletransportarse y perder creo yo por un momento los colliders (corregido)

public class PickupController : MonoBehaviour {
    [SerializeField] private Rigidbody itemtRB;
    [SerializeField] private Collider itemCollider;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform container;
    [SerializeField] private float pickUpRange;
    [SerializeField] private float dropForwardForce;
    [SerializeField] private float dropUpwardForce;
    [SerializeField] private float throwForce;
    public static bool slotFull;
    public bool equipaded;
    // Start is called before the first frame update
    void Start() {
        if (!equipaded) {
            itemtRB.isKinematic = true;
            itemCollider.isTrigger = false;

        } else {
            itemtRB.isKinematic = true;
            itemCollider.isTrigger = true;
            slotFull = true;
        }

    }

    // Update is called once per frame
    void Update() {
        Vector3 distamceToPlayer = transform.position - transform.position;
        if (!equipaded && distamceToPlayer.magnitude <= pickUpRange) {
            gameObject.GetComponent<Renderer>().material.color = Color.magenta;
            if (Input.GetKeyDown(KeyCode.E) && !slotFull) {
                PickUp();
            }
        } else {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        if (equipaded && Input.GetKeyDown(KeyCode.Q)) {
            Drop();
        }

    }

    #region Take&DropItems
    private void PickUp() {
        equipaded = true;
        slotFull = true;
        gameObject.GetComponent<Renderer>().material.color = Color.white;

        //make weapon a child of the player
        transform.SetParent(container);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        // make rb kinematic and boxcollider a trigger
        itemtRB.isKinematic = true;
        itemCollider.isTrigger = true;
    }

    private void Drop() {
        equipaded = false;
        slotFull = false;

        //set parent to null
        transform.SetParent(null);

        // make rb not kinematic and boxcollider normal
        itemtRB.isKinematic = false;
        itemCollider.isTrigger = false;

        //add random rotation
        float random = Random.Range(-4f, 1f);
        itemtRB.AddTorque(new Vector3(random, random, random) * 10);

        if (gameObject.CompareTag("BowlBall")) {
            Vector3 throwDirection = playerTransform.forward + Vector3.up * 0.5f;
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }
        if (gameObject.CompareTag("BomberTruck")) {
            StartCoroutine(MoveForwardUntilCollision());
        }
    }

    IEnumerator MoveForwardUntilCollision() {
        float moveSpeed = 20f;
        while (true) {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            yield return null;
        }
    }
    #endregion Take&DropItems

    void OnCollisionEnter(Collision collision) {
        if (gameObject.CompareTag("BomberTruck") && collision.gameObject.CompareTag("WallBlock")) {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        if (gameObject.CompareTag("BowlBall") && collision.gameObject.CompareTag("BowlPine")) {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}

