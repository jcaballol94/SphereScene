using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jCaballol94.SphereScene
{
    [RequireComponent(typeof(Light))]
    public class FireLight : MonoBehaviour
    {
        [SerializeField][Min(0f)] private float m_BigFrequency = 1f;
        [SerializeField] private float m_BigAmplitude = 0.5f;
        [SerializeField][Min(0f)] private float m_SmallFrequency = 10f;
        [SerializeField] private float m_SmallAmplitude = 0.1f;

        private float m_baseIntensity;
        private Light m_light;

        private void OnEnable()
        {
            m_light = GetComponent<Light>();
            m_baseIntensity = m_light.intensity;
        }

        private void OnDisable()
        {
            m_light.intensity = m_baseIntensity;
        }

        private void Update()
        {
            // The sinus, scaled by the frequency and amplitude
            var value = EvaluateWave(m_BigFrequency, m_BigAmplitude);
            value *= EvaluateWave(m_SmallFrequency, m_SmallAmplitude);

            m_light.intensity = value * m_baseIntensity;
        }

        private static float EvaluateWave(float freq, float amp)
        {
            var sin = Mathf.Sin(Time.time * freq * Mathf.PI * 2f);
            return 1f + sin * amp;
        }
    }
}