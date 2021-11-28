using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jCaballol94.SphereScene
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "SphereScene/Player Stats")]
    public class PlayerStats : ScriptableObject
    {
        [SerializeField] [Min(0f)] private float m_speed = 1f;
        [SerializeField] [Min(0f)] private float m_rotationSpeed = 360f;
        [SerializeField] [Min(0f)] private float m_cameraSpeed = 360f;

        public float Speed => m_speed;
        public float RotationSpeed => m_rotationSpeed;
        public float CameraSpeed => m_cameraSpeed;
    }
}