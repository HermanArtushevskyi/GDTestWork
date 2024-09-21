using System;
using System.Collections.Generic;
using _Project.CodeBase.Runtime.Factories.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Enemies;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.GameLoop.Interfaces;
using Level;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.CodeBase.Runtime.Gameplay.GameLoop
{
    public class EnemySpawner : IEnemySpawner
    {
        public List<IEnemy> AliveEnemies { get; }
        public event Action<int> OnEnemiesCountChanged;
        
        private readonly IFactory<Enemy, IPlayer, Vector3, ScriptableEnemy> _enemyFactory;

        public EnemySpawner(
            IFactory<Enemy, IPlayer, Vector3, ScriptableEnemy> enemyFactory)
        {
            _enemyFactory = enemyFactory;
            AliveEnemies = new List<IEnemy>();
        }
        
        public void Spawn(IPlayer player, Wave wave)
        {
            foreach (ScriptableEnemy enemy in wave.Characters)
            {
                SpawnEnemy(player, enemy);
            }
        }

        private void SpawnEnemy(IPlayer player, ScriptableEnemy enemy)
        {
            Vector3 position = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
            Enemy spawned = _enemyFactory.Create(player, position, enemy);
            AliveEnemies.Add(spawned);
            spawned.OnDeath += OnEnemyDeath;
        }

        private void OnEnemyDeath(Enemy obj)
        {
            AliveEnemies.Remove(obj);
            OnEnemiesCountChanged?.Invoke(AliveEnemies.Count);
        }
    }
}