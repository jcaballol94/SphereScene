using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jCaballol94.SphereScene
{
    [ExecuteAlways]
    public class WorldSphere : MonoBehaviour
    {
        [SerializeField] private float m_radius = 5f;

        private static readonly int WORLD_SPHERE = Shader.PropertyToID("_WorldSphere");

        private void LateUpdate()
        {
            var sphere = new Vector4(transform.position.x, transform.position.y,
                transform.position.z, m_radius);

            Shader.SetGlobalVector(WORLD_SPHERE, sphere);
        }
    }
}