using UnityEngine;

public class KeyCollector : MonoBehaviour {
    [SerializeField] private GameObject targetToUnlock; 

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Key collected! Unlocking target...");
            foreach (BoxCollider bc in targetToUnlock.GetComponentsInChildren<BoxCollider>()) {
                bc.enabled = false;
            }
            gameObject.SetActive(false);
        }
    }
}

