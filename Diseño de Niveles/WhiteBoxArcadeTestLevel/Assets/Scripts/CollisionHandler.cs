using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour {
    #region Variables
    [SerializeField] private string targetTag;
    [SerializeField] private float delayBeforeDeactivate;

    #endregion Variables

    #region CollisionDestroy
    private void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.CompareTag(targetTag)) {
            Debug.Log($"{gameObject.name} physically collided with {targetTag}. Waiting {delayBeforeDeactivate} sec to deactivate both.");
            StartCoroutine(DeactivateAfterDelay(collision.gameObject));
        }
    }
    private IEnumerator DeactivateAfterDelay(GameObject other) {
        yield return new WaitForSeconds(delayBeforeDeactivate);
        gameObject.SetActive(false);
        other.SetActive(false);
    }
    #endregion CollisionDestroy
}