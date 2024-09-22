using _Project.CodeBase.Runtime.Common;
using _Project.CodeBase.Runtime.Gameplay.Character;
using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Enemies;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Common;
using _Project.CodeBase.Runtime.Gameplay.GameLoop;
using _Project.CodeBase.Runtime.Gameplay.GameLoop.Interfaces;
using _Project.CodeBase.Runtime.Services.InputService;
using _Project.CodeBase.Runtime.Services.InputService.Interfaces;
using _Project.CodeBase.Runtime.Services.UI.Interfaces;
using _Project.CodeBase.Runtime.UI;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.DI.Game
{
    public class GameDependenciesInstaller : MonoInstaller
    {
        [SerializeField] private GameView _gameView;
        [SerializeField] private ScriptableEnemyCollection _enemyCollection;
        
        public override void InstallBindings()
        {
            Container.Bind(typeof(IView), typeof(GameView)).FromInstance(_gameView).AsSingle();
            Container.Bind<GamePresenter>().AsSingle();
            Container.Bind<ScriptableEnemyCollection>().FromInstance(_enemyCollection).AsSingle();
            Container.Bind<Factories.Interfaces.IFactory<Player, Vector3>>().To<PlayerFactory>().AsSingle();
            Container.Bind<Factories.Interfaces.IFactory<Enemy, IPlayer, Vector3, ScriptableEnemy, IEnemySpawner>>().To<EnemyFactory>().AsSingle();
            Container.Bind<IEnemySpawner>().To<EnemySpawner>().AsSingle();
            Container.Bind<IWaveSpawner>().To<WaveSpawner>().AsSingle();
            Container.Bind<IInputSource>().FromInstance(new InputSource(new KeyboardSource()));
        }
    }
}