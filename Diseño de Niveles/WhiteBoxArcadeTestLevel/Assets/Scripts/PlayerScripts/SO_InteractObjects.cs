using UnityEngine;

[CreateAssetMenu(fileName = "NewInteractObject", menuName = "Interact/InteractObject")]
public class SO_InteractObjects : ScriptableObject {
    public void Interact() {
        Debug.Log("Interacted with ScriptableObject!");
        // Aqu� va la l�gica de interacci�n
    }
}

