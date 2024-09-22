using System;

namespace _Project.CodeBase.Runtime.Gameplay.Enemies.Common
{
    [Serializable]
    public class EnemyStats
    {
        public float HP;
        public float Damage;
        public float AttackSpeed;
        public float AttackRange;
        
        public EnemyStats(float hp, float damage, float attackSpeed, float attackRange)
        {
            HP = hp;
            Damage = damage;
            AttackSpeed = attackSpeed;
            AttackRange = attackRange;
        }
        
        public EnemyStats Clone()
        {
            return new EnemyStats(HP, Damage, AttackSpeed, AttackRange);
        }
    }
}