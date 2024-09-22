using _Project.CodeBase.Runtime.Factories.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Character;
using _Project.CodeBase.Runtime.Gameplay.GameLoop.Interfaces;
using _Project.CodeBase.Runtime.Services.UI.Interfaces;
using _Project.CodeBase.Runtime.UI;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.Gameplay.GameLoop
{
    public class GameStarter : MonoBehaviour
    {
        private IFactory<Player, Vector3> _playerFactory;
        private IWaveSpawner _waveSpawner;
        private IView _gameView;
        private GamePresenter _presenter;

        [Inject]
        private void GetDependencies(IFactory<Player, Vector3> playerFactory, IWaveSpawner waveSpawner, IView gameView, GamePresenter presenter)
        {
            _playerFactory = playerFactory;
            _waveSpawner = waveSpawner;
            _gameView = gameView;
            _presenter = presenter;
        }
        
        private void Start()
        {
            Player player = _playerFactory.Create(Vector3.zero);
            _presenter.BindButtons(player);
            _waveSpawner.Spawn(0, player);
        }
    }
}