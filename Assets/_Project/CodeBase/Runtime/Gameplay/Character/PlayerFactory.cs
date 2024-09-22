using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.GameLoop.Interfaces;
using _Project.CodeBase.Runtime.Services.InputService.Interfaces;
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
        private readonly IInputSource _inputSource;

        public PlayerFactory(DiContainer container, GameObject playerPrefab, PlayerStats defaultStats, IUpdate update,
            IEnemySpawner enemySpawner, IInputSource inputSource)
        {
            _container = container;
            _playerPrefab = playerPrefab;
            _defaultStats = defaultStats;
            _update = update;
            _enemySpawner = enemySpawner;
            _inputSource = inputSource;
        }

        public Player Create(Vector3 position)
        {
            GameObject playerGameObject = Object.Instantiate(_playerPrefab, position, Quaternion.identity);
            Animator animator = playerGameObject.GetComponent<Animator>();
            Player player = new Player(_defaultStats, _update, animator, playerGameObject, _enemySpawner, _inputSource);
            _container.Bind<IPlayer>().FromInstance(player).AsSingle();
            return player;
        }
    }
}