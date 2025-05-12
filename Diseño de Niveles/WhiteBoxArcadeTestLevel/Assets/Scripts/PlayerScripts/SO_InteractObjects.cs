using UnityEngine;

[CreateAssetMenu(fileName = "NewInteractObject", menuName = "Interact/InteractObject")]
public class SO_InteractObjects : ScriptableObject {
    public void Interact(GameObject targetObject) {
        Debug.Log("Interacted with ScriptableObject!");
        // Aquí va la lógica de interacción
        if (targetObject.CompareTag("BowlBall") && targetObject.CompareTag("BowlPine")) {

        }
        if (targetObject.CompareTag("WallBlock") && targetObject.CompareTag("BomberTruck")) {

        }
        if (targetObject.CompareTag("BasketBall") && targetObject.CompareTag("Canasta")) {

        }
        if (targetObject.CompareTag("KeyVitrina")) {

        }

    }
}

