using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfView : MonoBehaviour
{
    private Camera mainCamera;
    private Renderer objectRenderer;

    void Start()
    {
        mainCamera = Camera.main;
        objectRenderer = GetComponent<Renderer>();

        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found in the scene!");
        }

        if (objectRenderer == null)
        {
            Debug.LogError("No Renderer found on this GameObject!");
        }
    }

    void Update()
    {
        if (!IsObjectVisible())
        {
            Destroy(gameObject);
        }
    }

    bool IsObjectVisible()
    {
        if (mainCamera == null || objectRenderer == null)
        {
            return false;
        }

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        Bounds bounds = objectRenderer.bounds;

        return GeometryUtility.TestPlanesAABB(planes, bounds);
    }
}
