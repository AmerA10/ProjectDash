using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Vector2 clampX, clampY;
    public Transform player;

    [Range(0, 1)]
    public float smoothSpeed = 0.125f;

    // Update is called once per frame
    void LateUpdate()
    {
        MoveWithPlayer();
    }

    private void MoveWithPlayer()
    {
        float targetX = Mathf.Clamp(player.position.x, clampX[0], clampX[1]);
        float targetY = Mathf.Clamp(player.position.y, clampY[0], clampY[1]);

        Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
