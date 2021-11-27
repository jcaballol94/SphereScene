using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jCaballol94.SphereScene
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerStats m_stats;
        private Rigidbody m_rigidbody;

        private void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var movement = new Vector3(horizontal, 0f, vertical);
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
        }
    }
}