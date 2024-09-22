using System;

namespace _Project.CodeBase.Runtime.UnityContext.Interfaces
{
    public interface ILateUpdate
    {
        public event Action OnLateUpdate;
    }
}