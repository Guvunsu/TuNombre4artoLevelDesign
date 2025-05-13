using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberTruckMovingForward : MonoBehaviour {
    #region Variables
    [SerializeField] float moveSpeed;
    bool isNearMyBomberTruckOrMyDoggie = false;

    Vector3 direction;

    [SerializeField] Transform BlockWallTransform;
    GameObject bomberTruckGO;
    Rigidbody bomberTruckRB;

    #endregion Variables

    #region PublicMethods
    void Start() {
        bomberTruckRB = GetComponent<Rigidbody>();
    }
    void Update() {
        if (isNearMyBomberTruckOrMyDoggie == true && Input.GetKeyDown(KeyCode.E)) {
            MoveCarForwarWithInteract();
        }
    }
    #endregion PublciMethods

    #region MoveCars
    public void MoveCarForwarWithInteract() {
        direction = (BlockWallTransform.position - bomberTruckRB.transform.position).normalized;
        Debug.Log("avance a la direccion correcta?para LOS VEHICULOS");
        bomberTruckRB.AddForce(direction * moveSpeed * Time.deltaTime, ForceMode.Impulse);
        Debug.Log("le agrego la fuerza y velocidad,para LOS VEHICULOS");
    }

    #endregion MoveCars

    #region CollisionDestroy
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("BomberTruck") && isNearMyBomberTruckOrMyDoggie == true) {
            Debug.Log("estoy cerca del BomberTruck y listo para moverlo.");
            StartCoroutine(DestroyAfterSeconds(10f));
            Debug.Log("me destruire en 10 segundos bombertruck");
        }
    }
    IEnumerator DestroyAfterSeconds(float seconds) {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("BomberTruck") && isNearMyBomberTruckOrMyDoggie == false) {
            bomberTruckGO = null;
            bomberTruckRB = null;
            Debug.Log("me alejé del BomberTruck.");
        }
    }
    #endregion CollisionDestroy
}
