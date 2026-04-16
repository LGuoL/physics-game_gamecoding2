using UnityEngine;
using UnityEngine.InputSystem;

public class LeverDragRaycastInputSystem : MonoBehaviour
{
    [Header("References")]
    public Camera mainCamera;
    public Rigidbody leverHandleRb;
    public Transform leverKnob;
    public LayerMask hitMask = ~0;

    [Header("Drag Settings")]
    public float torqueForce = 10f;
    public float maxMouseDeltaY = 120f;
    public float deadZone = 2f;

    [Header("Debug")]
    public bool showDebugLog = true;

    private bool isDragging = false;
    private Vector2 lastMousePosition;

    void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        if (Mouse.current == null || mainCamera == null || leverHandleRb == null || leverKnob == null)
            return;

        HandleMouseDown();
        HandleMouseUp();
    }

    void FixedUpdate()
    {
        if (!isDragging || Mouse.current == null || leverHandleRb == null)
            return;

        Vector2 currentMousePosition = Mouse.current.position.ReadValue();
        float deltaY = currentMousePosition.y - lastMousePosition.y;

        // 向下拖动时 deltaY 为负，所以取反
        float downwardDrag = -deltaY;

        if (Mathf.Abs(downwardDrag) < deadZone)
        {
            lastMousePosition = currentMousePosition;
            return;
        }

        float normalized = Mathf.Clamp(downwardDrag / maxMouseDeltaY, 0f, 1f);

        if (normalized > 0f)
        {
            // 你的 Hinge Axis 现在是 X，所以这里绕 X 轴加扭矩
            leverHandleRb.AddTorque(Vector3.left * normalized * torqueForce, ForceMode.Acceleration);

            if (showDebugLog)
                Debug.Log($"Dragging Lever | downwardDrag={downwardDrag:F2}, normalized={normalized:F2}");
        }

        lastMousePosition = currentMousePosition;
    }

    void HandleMouseDown()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, hitMask))
        {
            bool hitKnob = false;

            if (hit.collider.transform == leverKnob)
                hitKnob = true;
            else if (hit.collider.GetComponent<LeverKnobMarker>() != null)
                hitKnob = true;
            else if (hit.collider.GetComponentInParent<LeverKnobMarker>() != null)
                hitKnob = true;

            if (hitKnob)
            {
                isDragging = true;
                lastMousePosition = Mouse.current.position.ReadValue();

                if (showDebugLog)
                    Debug.Log("Started dragging lever knob");
            }
            else
            {
                if (showDebugLog)
                    Debug.Log("Mouse hit: " + hit.collider.gameObject.name);
            }
        }
    }

    void HandleMouseUp()
    {
        if (!isDragging) return;

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isDragging = false;

            if (showDebugLog)
                Debug.Log("Stopped dragging lever knob");
        }
    }
}