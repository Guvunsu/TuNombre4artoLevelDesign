using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Interact Object", order = 1)]
public class SO_InteractObjects : ScriptableObject
{
    public List<GameObject> Objects = new List<GameObject>();
    [SerializeField] bool doors;
    [SerializeField] bool move;
    public void Interact()
    {
        if (doors)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                Instantiate(Objects[i]);
            }
        }
        if (move)
        {

        }

    }

}