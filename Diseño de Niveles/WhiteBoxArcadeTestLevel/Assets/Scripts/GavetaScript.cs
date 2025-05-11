using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GavetaScript : MonoBehaviour
{
    public GameObject _nextPosition;
    public Transform _playerTransform;
    [SerializeField] private float _activationRange = 1f;

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, _playerTransform.position);

        if (distance <= _activationRange)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {

                transform.position = _nextPosition.transform.position;
            }
        }
    }
}


