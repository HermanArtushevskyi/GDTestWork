using System;
using Unity.VisualScripting;

namespace _Project.CodeBase.Runtime.UnityContext.Interfaces
{
    public interface IUpdate
    {
        public event Action OnUpdate; 
    }
}