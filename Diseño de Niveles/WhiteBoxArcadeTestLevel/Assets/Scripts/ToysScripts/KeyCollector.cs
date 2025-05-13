using UnityEngine;

public class KeyCollector : MonoBehaviour {
    [SerializeField] private GameObject targetToUnlock;

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("Obtuve la llave y ahorra el box collider de la vitrina del centro se desactivo");
            foreach (BoxCollider bc in targetToUnlock.GetComponentsInChildren<BoxCollider>()) {
                bc.enabled = false;
            }
            gameObject.SetActive(false);
        }
    }
}

