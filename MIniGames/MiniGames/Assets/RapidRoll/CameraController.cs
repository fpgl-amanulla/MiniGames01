using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RapidRoll
{
    public class CameraController : MonoBehaviour
    {
        public float cameraSpeed = 3.0f;


        private void LateUpdate()
        {
            if (GameManager.Instance.isGameStarted)
            {
                transform.Translate(Vector3.down * Time.deltaTime * cameraSpeed);
            }
        }
    }
}