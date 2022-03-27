using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    private BoxCollider2D bounds;
    private PlayerCamera theCamera;

    void Start()
    {
        bounds = GetComponent<BoxCollider2D>();
        theCamera = FindObjectOfType<PlayerCamera>();
        theCamera.SetBounds(bounds);
    }

}
