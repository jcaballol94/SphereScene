using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jCaballol94.SphereScene
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField][InlineObject] private PlayerStats m_stats;
        [SerializeField] private float m_warpDistance;
        [SerializeField] private Transform m_cameraRoot;

        private Rigidbody m_rigidbody;
        private Animator m_animator;
        private Vector3 m_worldOffset = Vector3.zero;

        private static readonly int MOVE_SPEED = Animator.StringToHash("MoveSpeed");
        private static readonly int WORLD_OFFSET = Shader.PropertyToID("_WorldOffset");

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Shader.SetGlobalVector(WORLD_OFFSET, m_worldOffset);
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            Wrap();

            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var movement = m_cameraRoot.forward * vertical + m_cameraRoot.right * horizontal;
            movement = Vector3.ClampMagnitude(movement, 1f);
            m_animator.SetFloat(MOVE_SPEED, movement.magnitude * m_stats.Speed);
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
            var offset = Vector3.zero;

            var position = transform.position;

            if (position.x > m_warpDistance)
                offset.x -= m_warpDistance * 2f;

            if (position.x < -m_warpDistance)
                offset.x += m_warpDistance * 2f;

            if (position.z > m_warpDistance)
                offset.z -= m_warpDistance * 2f;

            if (position.z < -m_warpDistance)
                offset.z += m_warpDistance * 2f;

            transform.position += offset;
            m_worldOffset -= offset;

            Shader.SetGlobalVector(WORLD_OFFSET, m_worldOffset);
        }
    }
}