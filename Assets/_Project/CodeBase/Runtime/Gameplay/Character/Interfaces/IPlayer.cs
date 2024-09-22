using _Project.CodeBase.Runtime.Gameplay.Entities.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Character.Interfaces
{
    public interface IPlayer : IHasHealth
    {
        public GameObject PlayerGameObject { get; }
        public bool CanAttack { get; }
        public bool CanSuperAttack { get; }
        
        public void Attack();
        public void SuperAttack();
    }
}