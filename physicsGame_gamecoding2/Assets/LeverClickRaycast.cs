using UnityEngine;

public class LeverClickRaycast : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask hitMask = ~0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (mainCamera == null)
                mainCamera = Camera.main;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, hitMask))
            {
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
            }
            else
            {
                Debug.Log("Raycast hit nothing");
            }
        }
    }
}