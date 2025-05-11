using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : MonoBehaviour
{
    public bool IsPowerOn = false;
    public GameObject _upLever;
    public GameObject _downLever;
    public Transform _playerTransform; 
    [SerializeField] private float _activationRange = 2f; 

    private void FixedUpdate()
    { //Nose que te parezca esto pam, pienso que es mejor que ponerle un rigidbody y si el jugador esta cerca ps que lo pueda hacer
        float distance = Vector3.Distance(transform.position, _playerTransform.position);

        if (!IsPowerOn && distance <= _activationRange)
        {
            GetComponent<Renderer>().material.color = Color.yellow;

            if (Input.GetKeyDown(KeyCode.E))
            {
                IsPowerOn = true;
                _downLever.SetActive(true);
                _upLever.SetActive(false);
            }
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
    }

}
