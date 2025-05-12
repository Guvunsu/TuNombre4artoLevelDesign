using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour
{
    [SerializeField] private Rigidbody itemRB;
    [SerializeField] private Collider itemCollider;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform container;

    [SerializeField] private SO_InteractObjects interactObject;
    [SerializeField] private GameObject targetToDisableCollider;
    bool hasInteracted = false;

    [SerializeField] private float pickUpRange = 3f;
    [SerializeField] private float throwForce = 5f;

    public static bool slotFull;
    private bool equipped;

    void Update()
    {
        Vector3 distanceToPlayer = playerTransform.position - transform.position;
        Debug.LogWarning("Update - Distance to player " + distanceToPlayer);
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange)
        {
            GetComponent<Renderer>().material.color = Color.magenta;
            Debug.LogWarning("Not equipped and distance in pick up range");
            if (Input.GetKeyDown(KeyCode.E) && !slotFull)
            {
                Debug.LogWarning("Pickup validated!!!!!");
                Interact(this.gameObject, targetToDisableCollider);
                hasInteracted = true;
                PickUp();
            }
        }
        else if (!equipped)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }

        if (equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
    }

    public virtual void Interact(GameObject sourceObject, GameObject targetToDisableCollider)
    {
        if (sourceObject.CompareTag("KeyVitrina") && targetToDisableCollider != null)
        {
            Collider targetCollider = targetToDisableCollider.GetComponent<Collider>();
            if (targetCollider != null)
            {
                targetCollider.enabled = false;
                Debug.Log("¡Puerta/Vitrina desbloqueada!");
            }
        }
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;
        GetComponent<Renderer>().material.color = Color.white;

        transform.SetParent(container);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;

        itemRB.isKinematic = true;
        itemCollider.isTrigger = true;
    }
    private void Drop()
    {
        equipped = false;
        slotFull = false;

        transform.SetParent(null);
        itemRB.isKinematic = false;
        itemCollider.isTrigger = false;

        if (CompareTag("BowlBall"))
        {
            Vector3 throwDirection = playerTransform.forward + Vector3.up * 0.5f;
            itemRB.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }

        if (CompareTag("BomberTruck"))
        {
            StartCoroutine(MoveForwardUntilCollision());
        }
    }
    IEnumerator MoveForwardUntilCollision()
    {
        float speed = 20f;
        while (true)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            yield return null;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {//debugs :3
        if (collision.gameObject.CompareTag("BomberTruck") ||
            collision.gameObject.CompareTag("WallBlock") ||
            collision.gameObject.CompareTag("BowlBall") ||
            collision.gameObject.CompareTag("BowlPine"))
        {// que avance el vechiculo y hacerle un scriipt para que avance y si toca el muro pasa una courutine y se desactivan o destruyen 
            // la pelota hacerle un scriupt para darle addforce para cuando lo lance tengo un aventon y cuando toque los pinos pasen un tiempo y se destrueyn o se desactiven 
            // cdebbugiar este codigo y probar cuiando hagarro cosas y suelto 
            Destroy(collision.gameObject);
            //Destroy(gameObject);
        }
    }
}
