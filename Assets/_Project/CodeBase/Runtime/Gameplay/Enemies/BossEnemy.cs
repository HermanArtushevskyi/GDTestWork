using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Common;
using _Project.CodeBase.Runtime.Gameplay.GameLoop.Interfaces;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Enemies
{
    public class BossEnemy : Enemy
    {
        private readonly ScriptableEnemy _regularEnemyPrefab;
        private readonly IEnemySpawner _enemySpawner;

        public BossEnemy(ScriptableEnemy regularEnemyPrefab, IEnemySpawner enemySpawner, EnemyStats stats, GameObject enemyGameObject, IUpdate update,
            IPlayer player) : base(stats, enemyGameObject, update, player)
        {
            _regularEnemyPrefab = regularEnemyPrefab;
            _enemySpawner = enemySpawner;
        }
        
        protected override void Die()
        {
            Vector3 firstSpawnPosition = EnemyGameObject.transform.position + new Vector3(1, 0, 1);
            Vector3 secondSpawnPosition = EnemyGameObject.transform.position + new Vector3(-1, 0, -1);
            _enemySpawner.Spawn(_regularEnemyPrefab, firstSpawnPosition, false);
            _enemySpawner.Spawn(_regularEnemyPrefab, secondSpawnPosition, false);
            base.Die();
        }
    }
}