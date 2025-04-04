using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CamaraMovement : MonoBehaviour
{
    #region ENUM
    public enum fsmCamara
    {
        EXIT,
        ENTER,
        STAY
    }
    #endregion ENUM

    #region VARIABLES

    [SerializeField] GameObject player;
    [SerializeField] public MovePlayer script_MovPlayer;
    fsmCamara raycastCamara;
    RaycastHit hit;
    bool isPressed;


    #endregion VARIABLES

    #region PublicUnityMethods
    void Start()
    {

    }

    void FixedUpdate()
    {
        while (true)
        {
            if (Physics.Raycast)
            {
                raycastCamara == fsmCamara.ENTER;
            }
        }
    }
    #endregion PublicUnityMethods

    #region Public Methods

    public void OnInteractRaycast()
    {
        if (isPressed && raycastCamara == fsmCamara.STAY)
        {
            // Add<MovePlayer.GameState.PLAYING>;
        }
        if (Physics.Raycast as RaycastHit && raycastCamara == fsmCamara.EXIT)
        {
            transform.position = hit.point;

        }
        if (Physics.Raycast as RaycastHit && raycastCamara == fsmCamara.STAY)
        {
            transform.position = hit.point;
        }
        if (Physics.Raycast as RaycastHit && raycastCamara == fsmCamara.STAY || raycastCamara== fsmCamara.ENTER)
        {
            raycastCamara == fsmCamara.EXIT;
            transform.position = hit.point;
        }
    }

    #endregion Public Methods

    #region Collisions



    #endregion Collisions
}
