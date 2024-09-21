using System;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Character
{
    [Serializable]
    public class PlayerStats
    {
        public float HP;
        public float Damage;
        public float AttackSpeed;
        public float AttackRange;
        
        public PlayerStats(float hp, float damage, float attackSpeed, float attackRange)
        {
            HP = hp;
            Damage = damage;
            AttackSpeed = attackSpeed;
            AttackRange = attackRange;
        }
        
        public PlayerStats Clone()
        {
            return new PlayerStats(HP, Damage, AttackSpeed, AttackRange);
        }
    }

    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Game/PlayerStats", order = 0)]
    public class ScriptablePlayerStats : ScriptableObject
    {
        public PlayerStats Stats;
    }
}