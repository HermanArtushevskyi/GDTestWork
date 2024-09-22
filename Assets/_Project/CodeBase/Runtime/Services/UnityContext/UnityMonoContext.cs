using System;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.Runtime.UnityContext
{
    public class UnityMonoContext : MonoBehaviour, IUpdate, IFixedUpdate, ILateUpdate
    {
        public event Action OnUpdate;
        public event Action OnFixedUpdate;
        public event Action OnLateUpdate;

        private void Update()
        {
            OnUpdate?.Invoke();
        }

        private void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }

        private void LateUpdate()
        {
            OnLateUpdate?.Invoke();
        }
    }
}