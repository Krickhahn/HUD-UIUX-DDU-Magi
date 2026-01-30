using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapPlayerMarker : MonoBehaviour
{
    [SerializeField] private Transform player; // the object whose Y rotation is your look direction
    [SerializeField] private float rotationOffset = 0f; // tweak if your arrow points up/right/etc

    void LateUpdate()
    {
        if (!player) return;

        // UI is in screen space: rotate around Z to represent world Y rotation
        float yaw = player.eulerAngles.y;
        transform.localRotation = Quaternion.Euler(0f, 0f, -yaw + rotationOffset);
    }
}
