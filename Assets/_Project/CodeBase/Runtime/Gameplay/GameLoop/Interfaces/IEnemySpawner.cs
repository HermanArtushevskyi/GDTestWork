using System;
using System.Collections.Generic;
using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Interfaces;
using Level;

namespace _Project.CodeBase.Runtime.Gameplay.GameLoop.Interfaces
{
    public interface IEnemySpawner
    {
        public List<IEnemy> AliveEnemies { get; }
        public event Action<int> OnEnemiesCountChanged;
        
        void Spawn(IPlayer player, Wave wave);
    }
}