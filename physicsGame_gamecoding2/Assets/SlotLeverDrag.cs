using UnityEngine;

public class SlotLeverDrag : MonoBehaviour
{
    public Camera mainCamera;
    public Rigidbody targetRb;
    public float torqueForce = 12f;
    public float maxDragDistance = 0.5f;

    private bool isDragging;
    private Vector3 dragStartPoint;

    void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (targetRb == null)
            targetRb = GetComponentInParent<Rigidbody>();
    }

    void OnMouseDown()
    {
        Debug.Log("OnMouseDown on LeverKnob");
        isDragging = true;
        dragStartPoint = GetMouseWorldPoint();
    }

    void OnMouseUp()
    {
        Debug.Log("OnMouseUp on LeverKnob");
        isDragging = false;
    }

    void FixedUpdate()
    {
        if (!isDragging || targetRb == null) return;

        Vector3 currentPoint = GetMouseWorldPoint();
        Vector3 delta = currentPoint - dragStartPoint;

        float pullAmount = Mathf.Clamp(-delta.y, 0f, maxDragDistance);

        if (pullAmount > 0.001f)
        {
            Debug.Log("Dragging pullAmount = " + pullAmount);
            targetRb.AddTorque(Vector3.right * pullAmount * torqueForce, ForceMode.Acceleration);
        }
    }

    Vector3 GetMouseWorldPoint()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(-mainCamera.transform.forward, transform.position);

        if (plane.Raycast(ray, out float enter))
            return ray.GetPoint(enter);

        return transform.position;
    }
}