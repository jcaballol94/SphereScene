using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jCaballol94.SphereScene
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerStats m_stats;
        [SerializeField] private float m_warpDistance;
        [SerializeField] private Transform m_cameraRoot;

        private Rigidbody m_rigidbody;

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Wrap();

            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var movement = m_cameraRoot.forward * vertical + m_cameraRoot.right * horizontal;
            movement = Vector3.ClampMagnitude(movement, 1f);
            movement *= m_stats.Speed * Time.deltaTime;

            m_rigidbody.MovePosition(transform.position + movement);
            
            if (movement.magnitude > 0)
            {
                var targetForward = movement.normalized;
                var newForward = Vector3.RotateTowards(transform.forward, targetForward,
                    m_stats.RotationSpeed * Mathf.Deg2Rad * Time.deltaTime, 0f);

                m_rigidbody.MoveRotation(Quaternion.LookRotation(newForward));
            }

            var camera = Input.GetAxis("Mouse X");
            var angles = m_cameraRoot.eulerAngles;
            angles.y += camera * Time.deltaTime * m_stats.CameraSpeed;
            m_cameraRoot.eulerAngles = angles;
        }

        private void Wrap()
        {
            var position = transform.position;

            if (position.x > m_warpDistance)
                position.x -= m_warpDistance * 2f;

            if (position.x < -m_warpDistance)
                position.x += m_warpDistance * 2f;

            if (position.z > m_warpDistance)
                position.z -= m_warpDistance * 2f;

            if (position.z < -m_warpDistance)
                position.z += m_warpDistance * 2f;

            transform.position = position;
        }
    }
}