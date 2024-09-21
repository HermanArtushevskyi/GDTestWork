using _Project.CodeBase.Runtime.Gameplay.Character;
using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Enemies;
using _Project.CodeBase.Runtime.Gameplay.GameLoop;
using _Project.CodeBase.Runtime.Gameplay.GameLoop.Interfaces;
using _Project.CodeBase.Runtime.Scenes.Interfaces;
using _Project.CodeBase.Runtime.Scenes;
using _Project.CodeBase.Runtime.UnityContext;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using Level;
using UnityEngine;
using Zenject;

namespace _Project.CodeBase.Runtime.DI.Global
{
    public class GlobalDependenciesInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _unityContextPrefab;
        [SerializeField] private ScriptablePlayerStats _playerStats;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private LevelConfig _levelConfig;
        
        public override void InstallBindings()
        {
            BindUnityContext();
            BindSceneLoader();
            BindPlayerStats();
            BindPlayerPrefab();
            BondLevelConfig();
            BindGameLoop();
            BindFactories();
        }

        private void BindGameLoop()
        {
            Container.Bind<IEnemySpawner>().To<EnemySpawner>().AsSingle();
            Container.Bind<IWaveSpawner>().To<WaveSpawner>().AsSingle();
        }

        private void BondLevelConfig()
        {
            Container.Bind<LevelConfig>().FromInstance(_levelConfig).AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<Factories.Interfaces.IFactory<Player, Vector3>>().To<PlayerFactory>().AsSingle();
            Container.Bind<Factories.Interfaces.IFactory<Enemy, IPlayer, Vector3, ScriptableEnemy>>().To<EnemyFactory>().AsSingle();
        }

        private void BindPlayerPrefab()
        {
            Container.Bind<GameObject>().FromInstance(_playerPrefab).AsCached();
        }

        private void BindPlayerStats()
        {
            Container.Bind<PlayerStats>().FromInstance(_playerStats.Stats).AsSingle();
        }

        private void BindUnityContext()
        {
            GameObject unityContext = GameObject.Instantiate(_unityContextPrefab);
            Container.Bind(typeof(IFixedUpdate), typeof(IUpdate)).To<UnityMonoContext>()
                .FromInstance(unityContext.GetComponent<UnityMonoContext>()).AsSingle();
        }

        private void BindSceneLoader()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        }
    }
}