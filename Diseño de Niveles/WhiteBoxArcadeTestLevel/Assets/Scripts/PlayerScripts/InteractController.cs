using UnityEngine;

public class InteractController : MonoBehaviour {
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float interactRange = 2f;
    [SerializeField] private SO_InteractObjects interactObject;
    [SerializeField] private GameObject targetToDisableCollider; // objeto como "Vitrina", "Puerta", etc.

    private bool hasInteracted = false;

    void Update() {
        if (hasInteracted) return;

        Vector3 distance = playerTransform.position - transform.position;

        if (distance.magnitude <= interactRange) {
            GetComponent<Renderer>().material.color = Color.green;

            if (Input.GetKeyDown(KeyCode.E)) {
                interactObject.Interact(this.gameObject, targetToDisableCollider);
                hasInteracted = true;
            }
        } else {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
