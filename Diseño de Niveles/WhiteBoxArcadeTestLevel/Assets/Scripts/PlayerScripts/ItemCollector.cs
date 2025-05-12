using UnityEngine;

public class ItemCollector : MonoBehaviour {
    [SerializeField] private Transform container;

    private void OnCollisionEnter(Collision collision) {
        GameObject item = collision.gameObject;

        if (item.CompareTag("BowlBall") || item.CompareTag("KeyVitrina")) {
            Debug.Log($"Picked up {item.name}");
            item.transform.SetParent(container);
            item.transform.localPosition = Vector3.zero; // opcional: centra en el contenedor
            item.GetComponent<Rigidbody>().isKinematic = true; // para que no se caiga
            item.GetComponent<Collider>().enabled = false; // para evitar colisiones
        }
    }
}
