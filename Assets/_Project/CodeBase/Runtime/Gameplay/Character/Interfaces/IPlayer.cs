using _Project.CodeBase.Runtime.Gameplay.Entities.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Character.Interfaces
{
    public interface IPlayer : IHasHealth
    {
        public GameObject PlayerGameObject { get; }
    }
}