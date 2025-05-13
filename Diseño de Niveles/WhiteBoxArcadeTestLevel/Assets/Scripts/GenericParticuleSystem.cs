using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericParticuleSystem : MonoBehaviour
{
    [SerializeField] GameObject particules;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            particules.SetActive(true);
        }
    }
}
