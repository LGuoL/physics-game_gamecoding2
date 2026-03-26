using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{
    [Header("Grab Settings")]
    [Tooltip("How far away the player can grab objects from")]
    public float grabRange = 4;

    [Tooltip("How fast the held object moves to the hold point. higher = snappier")]
    public float holdSmoothing = 15f;

    public Transform holdPoint;

    public float throwForce = 15f;

    private Rigidbody heldObject;
    private bool isHolding = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void TryGrab()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * grabRange, Color.yellow, 0.5f);

        if (Physics.Raycast(ray, out hit, grabRange))
        {
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();
            heldObject = hit.collider.GetComponent<Rigidbody>();
            if (interactable != null)
            {
                heldObject.useGravity = false;
                heldObject.freezeRotation = true;
                heldObject.linearVelocity = Vector3.zero;
                heldObject.angularVelocity = Vector3.zero;

                isHolding = true;
                Debug.Log($"Grabbed {heldObject.name}");
            }
        }

    }

    void MoveHeldObject()
    {
        Vector3 targetPos = holdPoint.position;
        Vector3 currentPos = holdPoint.position;

        Vector3 newPos = Vector3.Lerp(currentPos, targetPos, holdSmoothing * Time.fixedDeltaTime);
    }

    void DropObject()
    {
        if (heldObject == null) return;

        heldObject.useGravity = true;
        heldObject.freezeRotation = false;

        heldObject = null;
        isHolding = false;
        Debug.Log("Dropped Object");
    }
}