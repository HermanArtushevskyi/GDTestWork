using _Project.CodeBase.Runtime.Factories.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Character;
using _Project.CodeBase.Runtime.Gameplay.GameLoop.Interfaces;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Gameplay.GameLoop
{
    public class GameStarter : MonoBehaviour
    {
        private IFactory<Player, Vector3> _playerFactory;
        private IWaveSpawner _waveSpawner;

        [Inject]
        private void GetDependencies(IFactory<Player, Vector3> playerFactory, IWaveSpawner waveSpawner)
        {
            _playerFactory = playerFactory;
            _waveSpawner = waveSpawner;
        }
        
        private void Start()
        {
            Player player = _playerFactory.Create(Vector3.zero);
            _waveSpawner.Spawn(0, player);
        }
    }
}