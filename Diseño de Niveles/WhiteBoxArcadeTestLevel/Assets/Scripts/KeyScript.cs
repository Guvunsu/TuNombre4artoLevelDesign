using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject key;
    public bool IsTheKeyTaken = false;
    public Transform _playerTransform;
    [SerializeField] private float _activationRange = 2f;

    private void FixedUpdate()
    { 
        float distance = Vector3.Distance(transform.position, _playerTransform.position);

        if (!IsTheKeyTaken && distance <= _activationRange)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                IsTheKeyTaken = true;
                key.SetActive(false);
            }
        }
    }
}
