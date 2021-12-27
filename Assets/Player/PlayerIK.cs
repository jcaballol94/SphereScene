using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jCaballol94.SphereScene
{
    public class PlayerIK : MonoBehaviour
    {
        public Transform target;
        [Range(0f, 1f)] public float weight;

        private Animator m_animator;

        private void Start()
        {
            m_animator = GetComponent<Animator>();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (!target)
            {
                m_animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
                m_animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
                return;
            }

            m_animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, weight);
            m_animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, weight);
            m_animator.SetIKPosition(AvatarIKGoal.LeftHand, target.position);
            m_animator.SetIKRotation(AvatarIKGoal.LeftHand, target.rotation);
        }
    }
}