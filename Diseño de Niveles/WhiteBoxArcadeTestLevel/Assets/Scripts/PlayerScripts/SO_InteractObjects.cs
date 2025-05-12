using UnityEngine;

[CreateAssetMenu(fileName = "NewInteractObject", menuName = "Interact/InteractObject")]
public class SO_InteractObjects : ScriptableObject {
    public virtual void Interact(GameObject sourceObject, GameObject targetToDisableCollider) {
        if (sourceObject.CompareTag("KeyVitrina") && targetToDisableCollider != null) {
            Collider targetCollider = targetToDisableCollider.GetComponent<Collider>();
            if (targetCollider != null) {
                targetCollider.enabled = false;
                Debug.Log("¡Puerta/Vitrina desbloqueada!");
            }
        }
    }
}
