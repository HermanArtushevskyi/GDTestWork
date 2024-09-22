using System;
using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Common;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Entities.Interfaces;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.CodeBase.Runtime.Gameplay.Enemies
{
    public class Enemy : IEnemy
    {
        public EnemyStats Stats { get; private set; }
        public GameObject EnemyGameObject { get; private set; }
        public float HP => Stats.HP;
        public event Action<IHasHealth> OnDeath;
        public event Action<Enemy> OnEnemyDeath;

        private readonly Animator _animator;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly IUpdate _update;
        private readonly IPlayer _player;

        private float _lastAttackTime;
        private bool _isDead;

        public Enemy(EnemyStats stats, GameObject enemyGameObject, IUpdate update, IPlayer player)
        {
            Stats = stats.Clone();
            EnemyGameObject = enemyGameObject;
            _animator = enemyGameObject.GetComponent<Animator>();
            _navMeshAgent = enemyGameObject.GetComponent<NavMeshAgent>();
            _update = update;
            _player = player;
            update.OnUpdate += OnUpdate;
        }

        public void TakeDamage(float damage)
        {
            Stats.HP -= damage;
            
            if (Stats.HP <= 0)
            {
                Die();
            }
        }

        protected virtual void OnUpdate()
        {
            if(_isDead || EnemyGameObject == null)
            {
                _update.OnUpdate -= OnUpdate;
                return;
            }
            
            var distance = Vector3.Distance(EnemyGameObject.transform.position, _player.PlayerGameObject.transform.position);
            
            if (distance <= Stats.AttackRange)
            {
                _navMeshAgent.isStopped = true;
                if (Time.time - _lastAttackTime > Stats.AttackSpeed)
                {
                    _lastAttackTime = Time.time;
                    _player.TakeDamage(Stats.Damage);
                    _animator.SetTrigger("Attack");
                }
            }
            else
            {
                _navMeshAgent.SetDestination(_player.PlayerGameObject.transform.position);
            }
            
            _animator.SetFloat("Speed", _navMeshAgent.speed);
        }

        protected virtual void Die()
        {
            _animator.SetTrigger("Die");
            _isDead = true;
            OnDeath?.Invoke(this);
            OnEnemyDeath?.Invoke(this);
            GameObject.Destroy(EnemyGameObject);
        }
    }
}