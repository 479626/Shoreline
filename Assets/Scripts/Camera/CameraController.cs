using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Position")]
    public Transform player;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    void Start()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }

    void LateUpdate()
    {
        var pos = player.position;
        if (transform.position != player.position)
        {
            Vector3 playerPosition = new Vector3(pos.x, pos.y, transform.position.z);
            playerPosition.x = Mathf.Clamp(playerPosition.x, minPosition.x, maxPosition.x);
            playerPosition.y = Mathf.Clamp(playerPosition.y, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, playerPosition, smoothing);

        }
    }

    private Vector3 RoundPosition(Vector3 position)
    {
        float xOffset = position.x % .0625f;
        if (xOffset != 0)
        {
            position.x -= xOffset;
        }
        float yOffset = position.y % .0625f;
        if (yOffset != 0)
        {
            position.y -= yOffset;
        }
        return position;
    }
}
