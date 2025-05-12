using UnityEngine;

public class KeyCollector : MonoBehaviour {
    [SerializeField] private GameObject targetToUnlock; // Objeto cuyos BoxColliders quieres desactivar

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Key collected! Unlocking target...");
            foreach (BoxCollider bc in targetToUnlock.GetComponentsInChildren<BoxCollider>()) {
                bc.enabled = false;
            }
            // Desactiva la llave
            gameObject.SetActive(false);
        }
    }
}

