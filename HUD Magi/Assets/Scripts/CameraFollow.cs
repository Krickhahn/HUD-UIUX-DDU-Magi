using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MinimapFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float height = 40f;

    void LateUpdate()
    {
        if (!target) return;
        transform.position = new Vector3(target.position.x, target.position.y + height, target.position.z);
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
}
