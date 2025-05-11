using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorScript : MonoBehaviour
{
    [SerializeField] protected KeyScript _keyScript;
    public Transform _playerTransform;
    [SerializeField] private float _activationRange = 1f;

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, _playerTransform.position);

        if (_keyScript.IsTheKeyTaken && distance <= _activationRange)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
