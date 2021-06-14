using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static Vector3 DirectionToPlayer(Vector3 position)
    {
        var playerPosition = PlayerController.Instance.transform.position;
        var direction = playerPosition - position;
        direction.y = 0f;
        direction.Normalize();

        return direction;
    }

    public static Vector3 DirectionTo(Transform t1, Transform t2)
    {
        var dir = t2.position - t1.position;
        dir.Normalize();

        return dir;
    }
}
