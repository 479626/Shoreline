using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Position")]
    public Transform player;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    private void Start()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }

    private void LateUpdate()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        var pos = player.position;
        var position = transform.position;

        if (transform.position != player.position)
        {
            var playerPosition = new Vector3(pos.x, pos.y, position.z);
            playerPosition.x = Mathf.Clamp(playerPosition.x, minPosition.x, maxPosition.x);
            playerPosition.y = Mathf.Clamp(playerPosition.y, minPosition.y, maxPosition.y);
            position = Vector3.Lerp(position, playerPosition, smoothing);
            transform.position = position;
        }
    }

    private Vector3 RoundPosition(Vector3 position)
    {
        var xOffset = position.x % .0625f;
        var yOffset = position.y % .0625f;
        
        if (xOffset != 0)
        {
            position.x -= xOffset;
        }
        
        if (yOffset != 0)
        {
            position.y -= yOffset;
        }
        return position;
    }
}
