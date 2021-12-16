using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jCaballol94.SphereScene
{
    [SelectionBase]
    public class CuttableRenderer : MonoBehaviour
    {
        [Header("Main")]
        [SerializeField] private Mesh m_mainMesh;
        [SerializeField] private Material m_outsideMaterial;
        [SerializeField] private Material m_insideMaterial;

        [Header("Interior details")]
        [SerializeField] private Mesh m_interiorMesh;
        [SerializeField] private Material m_interiorMaterial;
        [SerializeField] private bool m_addInteriorInside;

        [HideInInspector][SerializeField] private GameObject m_outsideObject;
        [HideInInspector][SerializeField] private GameObject m_insideObject;
        [HideInInspector][SerializeField] private GameObject m_interiorObject;
        [HideInInspector][SerializeField] private GameObject m_interiorInsideObject;

#if UNITY_EDITOR
        private void OnValidate()
        {
            m_outsideObject = CreateObject(m_outsideObject, m_mainMesh, m_outsideMaterial, 0, true, true);
            m_insideObject = CreateObject(m_insideObject, m_mainMesh, m_insideMaterial, 3, true, false);
            m_interiorObject = CreateObject(m_interiorObject, m_interiorMesh, m_interiorMaterial, 3, true, false);
            m_interiorInsideObject = CreateObject(m_interiorInsideObject, m_interiorMesh,
                m_insideMaterial, 3, m_addInteriorInside, false);

            if (m_outsideObject) m_outsideObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
            if (m_insideObject) m_insideObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
            if (m_interiorObject) m_interiorObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
            if (m_interiorInsideObject) m_interiorInsideObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
        }

        private GameObject CreateObject(GameObject current, Mesh mesh, Material material, 
            int layer, bool needed, bool shadows)
        {
            // No need for this, destroy the existing one
            if (!needed || !mesh || !material)
            {
                if (current)
                    UnityEditor.EditorApplication.delayCall += () => DestroyImmediate(current);

                return null;
            }

            // Create the object if it doesn't exist
            if (current == null)
            {
                current = new GameObject();
                current.transform.SetParent(transform, false);
                current.layer = layer;
            }

            // Get the filter
            if (!current.TryGetComponent<MeshFilter>(out var filter))
                filter = current.AddComponent<MeshFilter>();

            // Get the renderer
            if (!current.TryGetComponent<MeshRenderer>(out var renderer))
                renderer = current.AddComponent<MeshRenderer>();

            // Setup
            filter.sharedMesh = mesh;
            renderer.sharedMaterial = material;
            renderer.shadowCastingMode = shadows ? UnityEngine.Rendering.ShadowCastingMode.On : 
                UnityEngine.Rendering.ShadowCastingMode.Off;
            renderer.receiveShadows = shadows;

            return current;
        }
#endif
    }
}