using UnityEngine;

[CreateAssetMenu(fileName = "NewInteractObject", menuName = "Interact/InteractObject")]
public class SO_InteractObjects : ScriptableObject {
    public void Interact() {
        Debug.Log("Interacted with ScriptableObject!");
        // Aquí va la lógica de interacción
    }
}

