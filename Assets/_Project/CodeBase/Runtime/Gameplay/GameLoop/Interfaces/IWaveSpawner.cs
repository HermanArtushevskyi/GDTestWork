using System;
using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;

namespace _Project.CodeBase.Runtime.Gameplay.GameLoop.Interfaces
{
    public interface IWaveSpawner
    {
        public int CurrentWave { get; }
        public event Action AllWavesCleared;
        public void Spawn(int waveNumber, IPlayer player);
    }
}