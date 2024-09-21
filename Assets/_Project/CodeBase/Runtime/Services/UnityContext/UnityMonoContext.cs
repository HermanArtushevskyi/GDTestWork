using System;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.Runtime.UnityContext
{
    public class UnityMonoContext : MonoBehaviour, IUpdate, IFixedUpdate
    {
        public event Action OnUpdate;
        public event Action OnFixedUpdate;
        
        private void Update()
        {
            OnUpdate?.Invoke();
        }
        
        private void FixedUpdate()
        {
            OnFixedUpdate?.Invoke();
        }
    }
}