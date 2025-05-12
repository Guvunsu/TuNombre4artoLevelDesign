using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour {

    [SerializeField] private string targetTag;
    [SerializeField] private float delayBeforeDeactivate = 1f;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(targetTag)) {
            Debug.Log($"{gameObject.name} touched {targetTag}. Waiting {delayBeforeDeactivate} sec to deactivate both.");
            StartCoroutine(DeactivateAfterDelay(other.gameObject));
        }
    }

    private IEnumerator DeactivateAfterDelay(GameObject other) {
        yield return new WaitForSeconds(delayBeforeDeactivate);
        gameObject.SetActive(false);
        other.SetActive(false);
    }
}