using _Project.CodeBase.Runtime.Factories.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Enemies
{
    public class EnemyFactory : IFactory<Enemy, IPlayer, Vector3, ScriptableEnemy>
    {
        private readonly IUpdate _update;

        public EnemyFactory(IUpdate update)
        {
            _update = update;
        }
        
        public Enemy Create(IPlayer player, Vector3 position, ScriptableEnemy enemy)
        {
            GameObject enemyGameObject = Object.Instantiate(enemy.Prefab, position, Quaternion.identity);
            return new Enemy(enemy.Stats, enemyGameObject, _update, player);
        }
    }
}