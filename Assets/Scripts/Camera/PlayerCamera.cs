using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;
    public BoxCollider2D boundingBox;
    private Vector3 minBound, maxBound;
    private Camera c;
    private float height, width;

    void Start()
    {
        minBound = boundingBox.bounds.min;
        maxBound = boundingBox.bounds.max;
        c = GetComponent<Camera>();
        height = c.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    void Awake()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }

    void Update()
    {
        // This will follow the player in the scene
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        // This will keep the camera inside of a set bounding box using the Box Collider 2D component
        float clampX = Mathf.Clamp(transform.position.x, minBound.x + width, maxBound.x - width);
        float clampY = Mathf.Clamp(transform.position.y, minBound.y + height, maxBound.y - height);
        transform.position = new Vector3(clampX, clampY, transform.position.z);

        if (boundingBox != null)
        {
            float clampedX = Mathf.Clamp(transform.position.x, minBound.x + width, maxBound.x - width);
            float clampedY = Mathf.Clamp(transform.position.y, minBound.y + height, maxBound.y - height);
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
        if (boundingBox == null)
        {
            boundingBox = FindObjectOfType<BoundingBox>().GetComponent<BoxCollider2D>();
            minBound = boundingBox.bounds.min;
            maxBound = boundingBox.bounds.max;
        }
    }

    // Function to be called from BoundingBox.cs to set a new bounding box for different scenes
    public void SetBounds(BoxCollider2D newBoundingBox)
    {
        boundingBox = newBoundingBox;

        minBound = boundingBox.bounds.min;
        maxBound = boundingBox.bounds.max;
    }
}
