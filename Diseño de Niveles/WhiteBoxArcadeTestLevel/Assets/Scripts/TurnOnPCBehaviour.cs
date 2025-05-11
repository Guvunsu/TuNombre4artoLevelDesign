using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurnOnPCBehaviour : MonoBehaviour
{
    public FuseBox fuseBox;

    public GameObject TurnedRedScreen;
    public GameObject TurnedGreenScreen;
    public Transform _playerTransform;
    [SerializeField] private float _activationRange = 2f;

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, _playerTransform.position);

        if (fuseBox.IsPowerOn && distance <= _activationRange)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                TurnedGreenScreen.SetActive(true);
                TurnedRedScreen.SetActive(false);
            }

        }
    }
}
