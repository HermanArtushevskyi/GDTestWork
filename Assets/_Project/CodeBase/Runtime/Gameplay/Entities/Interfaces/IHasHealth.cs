﻿using System;

namespace _Project.CodeBase.Runtime.Gameplay.Entities.Interfaces
{
    public interface IHasHealth
    {
        public float HP { get; }
        public void TakeDamage(float damage);
        public event Action<IHasHealth> OnDeath;
    }
}