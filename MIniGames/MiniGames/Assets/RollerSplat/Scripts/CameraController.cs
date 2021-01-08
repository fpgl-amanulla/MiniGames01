using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Vector3 position;
    public float speed = 5.0f;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * speed);
    }
}
