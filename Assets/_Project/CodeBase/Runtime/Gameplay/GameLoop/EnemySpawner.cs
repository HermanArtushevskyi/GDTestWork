using System;
using System.Collections.Generic;
using _Project.CodeBase.Runtime.Factories.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Enemies;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Common;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Entities.Interfaces;
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
        
        private readonly IFactory<Enemy, IPlayer, Vector3, ScriptableEnemy, IEnemySpawner> _enemyFactory;
        private IPlayer _player;

        public EnemySpawner(
            IFactory<Enemy, IPlayer, Vector3, ScriptableEnemy, IEnemySpawner> enemyFactory)
        {
            _enemyFactory = enemyFactory;
            AliveEnemies = new List<IEnemy>();
        }
        
        public void Spawn(IPlayer player, Wave wave)
        {
            _player = player;
            foreach (ScriptableEnemy enemy in wave.Characters)
            {
                Spawn(enemy, Vector3.zero);
            }
        }

        public void Spawn(ScriptableEnemy enemy, Vector3 position, bool randomPos = true)
        {
            if (randomPos)
            {
                SpawnEnemy(_player, enemy, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
                return;
            }
            
            SpawnEnemy(_player, enemy, position);
        }

        private void SpawnEnemy(IPlayer player, ScriptableEnemy enemy, Vector3 position)
        {
            IEnemy spawned = _enemyFactory.Create(player, position, enemy, this);
            AliveEnemies.Add(spawned);
            spawned.OnDeath += OnEnemyDeath;
        }

        private void OnEnemyDeath(IHasHealth obj)
        {
            IEnemy enemy = (IEnemy) obj;
            AliveEnemies.Remove(enemy);
            OnEnemiesCountChanged?.Invoke(AliveEnemies.Count);
        }
    }
}