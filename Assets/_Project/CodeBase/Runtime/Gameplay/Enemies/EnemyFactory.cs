using _Project.CodeBase.Runtime.Common;
using _Project.CodeBase.Runtime.Factories.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Common;
using _Project.CodeBase.Runtime.Gameplay.GameLoop.Interfaces;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Gameplay.Enemies
{
    public class EnemyFactory : IFactory<Enemy, IPlayer, Vector3, ScriptableEnemy, IEnemySpawner>
    {
        private readonly IUpdate _update;
        private readonly ScriptableEnemy _regularEnemyPrefab;

        public EnemyFactory(IUpdate update, ScriptableEnemyCollection regularEnemyPrefab)
        {
            _update = update;
            _regularEnemyPrefab = regularEnemyPrefab.RegularEnemy;
        }
        
        public Enemy Create(IPlayer player, Vector3 position, ScriptableEnemy enemy, IEnemySpawner enemySpawner)
        {
            GameObject enemyGameObject = Object.Instantiate(enemy.Prefab, position, Quaternion.identity);
            if (enemy.EnemyType == EnemyType.Regular)
            {
                return new Enemy(enemy.Stats, enemyGameObject, _update, player);
            }

            if (enemy.EnemyType == EnemyType.Boss)
            {
                return new BossEnemy(_regularEnemyPrefab, enemySpawner, enemy.Stats, enemyGameObject, _update, player);
            }

            return null;
        }
    }
}