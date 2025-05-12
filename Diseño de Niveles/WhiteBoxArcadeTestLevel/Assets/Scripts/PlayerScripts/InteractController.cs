using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour {
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float intereactRange;

    public static bool _interacted;
    public bool _interact;

    [SerializeField] SO_InteractObjects interactObjects;

    #region RunTimeVariables
    Vector3 distanceToPlayer;
    #endregion
    private void Update() {
        distanceToPlayer = playerTransform.position - transform.position;
        if (!_interact && distanceToPlayer.magnitude <= intereactRange) {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            if (Input.GetKeyDown(KeyCode.E) && !_interacted) {
                interactObjects.Interact(gameObject);
                _interact = true;
                _interacted = true;
            }
        } else {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }

}