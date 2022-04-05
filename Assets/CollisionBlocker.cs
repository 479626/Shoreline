using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBlocker : MonoBehaviour
{
    public BoxCollider2D objectCollider;
    public CapsuleCollider2D noCollisionZone;

    void Start()
    {
        Physics2D.IgnoreCollision(objectCollider, noCollisionZone, true);
    }
}
