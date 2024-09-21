using _Project.CodeBase.Runtime.Gameplay.Entities.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Enemies.Interfaces
{
    public interface IEnemy : IHasHealth
    {
        public GameObject EnemyGameObject { get; }
    }
}