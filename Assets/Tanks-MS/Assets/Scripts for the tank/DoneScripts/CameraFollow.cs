using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LimitlessWarfare
{
    public class CameraFollow : MonoBehaviour
    {
        [Header("Camera Properties")]
        public GameObject player;
        public float cameraHeight = 30.0f;

        void Update()
        {
            Vector3 pos = player.transform.position;
            pos.y += cameraHeight;
            transform.position = pos;
        }
    }
}
