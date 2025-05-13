using System.Collections;
using UnityEngine;

public class KeyOpenShowCase : MonoBehaviour {
    #region Variables
    [SerializeField] private GameObject targetToUnlock;

    #endregion Variables

    #region CollisionDestroyBoxCollider
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("KeyVitrina")) {
            Debug.Log("Llave recogida, desactivando colisionadores de la vitrina...");
            foreach (BoxCollider bc in targetToUnlock.GetComponentsInChildren<BoxCollider>()) {
                bc.enabled = false;
            }
            StartCoroutine(DestroyAfterSeconds(1.666f));
        }
    }
    IEnumerator DestroyAfterSeconds(float seconds) {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
    #endregion CollisionDestroyBoxCollider
}

