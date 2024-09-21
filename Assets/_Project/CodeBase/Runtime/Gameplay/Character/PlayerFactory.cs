using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.GameLoop.Interfaces;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Gameplay.Character
{
    public class PlayerFactory : Factories.Interfaces.IFactory<Player, Vector3>
    {
        private readonly DiContainer _container;
        private readonly GameObject _playerPrefab;
        private readonly PlayerStats _defaultStats;
        private readonly IUpdate _update;
        private readonly IEnemySpawner _enemySpawner;

        public PlayerFactory(DiContainer container, GameObject playerPrefab, PlayerStats defaultStats, IUpdate update,
            IEnemySpawner enemySpawner)
        {
            _container = container;
            _playerPrefab = playerPrefab;
            _defaultStats = defaultStats;
            _update = update;
            _enemySpawner = enemySpawner;
        }

        public Player Create(Vector3 position)
        {
            GameObject playerGameObject = Object.Instantiate(_playerPrefab, position, Quaternion.identity);
            Animator animator = playerGameObject.GetComponent<Animator>();
            Player player = new Player(_defaultStats, _update, animator, playerGameObject, _enemySpawner);
            _container.Bind<IPlayer>().FromInstance(player).AsSingle();
            return player;
        }
    }
}