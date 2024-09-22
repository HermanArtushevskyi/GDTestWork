using System;

namespace _Project.CodeBase.Runtime.Gameplay.Character
{
    [Serializable]
    public class PlayerStats
    {
        public float HP;
        public float Damage;
        public float AttackSpeed;
        public float SuperAttackSpeed;
        public float AttackRange;
        public float MoveSpeed;
        
        public PlayerStats(float hp, float damage, float attackSpeed, float superAttackSpeed, float attackRange, float moveSpeed)
        {
            HP = hp;
            Damage = damage;
            AttackSpeed = attackSpeed;
            SuperAttackSpeed = superAttackSpeed;
            AttackRange = attackRange;
            MoveSpeed = moveSpeed;
        }
        
        public PlayerStats Clone()
        {
            return new PlayerStats(HP, Damage, AttackSpeed, SuperAttackSpeed, AttackRange, MoveSpeed);
        }
    }
}