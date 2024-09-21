using System;
using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.GameLoop.Interfaces;
using Level;

namespace _Project.CodeBase.Runtime.Gameplay.GameLoop
{
    public class WaveSpawner : IWaveSpawner
    {
        public int CurrentWave { get; private set; }
        public event Action AllWavesCleared;
        
        private readonly LevelConfig _config;
        private readonly IEnemySpawner _enemySpawner;
        private IPlayer _player;

        public WaveSpawner(LevelConfig config, IEnemySpawner enemySpawner)
        {
            _config = config;
            _enemySpawner = enemySpawner;
            _enemySpawner.OnEnemiesCountChanged += OnEnemiesCountChanged;
        }

        public void Spawn(int waveNumber, IPlayer player)
        {
            _player = player;
            if (waveNumber >= _config.Waves.Length)
            {
                AllWavesCleared?.Invoke();
                return;
            }
            var wave = _config.Waves[waveNumber];
            _enemySpawner.Spawn(player, wave);
            CurrentWave = waveNumber;
        }

        private void OnEnemiesCountChanged(int n)
        {
            if (n == 0)
            {
                CurrentWave++;
                Spawn(CurrentWave, _player);
            }
        }
    }
}