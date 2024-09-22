using System;
using System.Collections.Generic;
using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Entities.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.GameLoop.Interfaces;
using _Project.CodeBase.Runtime.Services.InputService.Common;
using _Project.CodeBase.Runtime.Services.InputService.Interfaces;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Character
{
    public class Player : IPlayer
    {
        public PlayerStats Stats { get; private set; }
        public GameObject PlayerGameObject => _playerGameObject;
        public float HP { get => Stats.HP; }
        public bool CanAttack => Time.time - _lastAttackTime > Stats.AttackSpeed;
        public bool CanSuperAttack => Time.time - _lastSuperAttackTime > Stats.SuperAttackSpeed;
        public event Action<IHasHealth> OnDeath;

        private float _lastAttackTime;
        private float _lastSuperAttackTime;
        private bool _isDead;
        
        private readonly GameObject _playerGameObject;
        private readonly IEnemySpawner _enemySpawner;
        private readonly IInputSource _inputSource;
        private readonly Animator _animator;
        private readonly Rigidbody _rb;
        private readonly IUpdate _update;


        public Player(PlayerStats stats, IUpdate update, Animator animator, GameObject playerGameObject,
            IEnemySpawner enemySpawner, IInputSource inputSource)
        {
            Stats = stats.Clone();
            _update = update;
            _animator = animator;
            _playerGameObject = playerGameObject;
            _enemySpawner = enemySpawner;
            _inputSource = inputSource;
            _update.OnUpdate += OnUpdate;
            _rb = playerGameObject.GetComponent<Rigidbody>();
            _enemySpawner.OnEnemiesCountChanged += OnEnemiesCountChanged;
        }

        public void TakeDamage(float damage)
        {
            Stats.HP -= damage;
            
            if (Stats.HP <= 0)
            {
                Die();
            }
        }


        public void Attack()
        {
            if (Time.time - _lastAttackTime < Stats.AttackSpeed)
            {
                return;
            }
            
            _lastAttackTime = Time.time;
            
            _animator.SetTrigger("Attack");
            
            List<IEnemy> Enemies = _enemySpawner.AliveEnemies;
            
            foreach (IEnemy enemy in Enemies)
            {
                if (Vector3.Distance(_playerGameObject.transform.position, enemy.EnemyGameObject.transform.position) < Stats.AttackRange)
                {
                    enemy.TakeDamage(Stats.Damage);
                    _playerGameObject.transform.LookAt(enemy.EnemyGameObject.transform.position);
                    return;
                }
            }
        }

        public void SuperAttack()
        {
            if (Time.time - _lastSuperAttackTime < Stats.SuperAttackSpeed)
            {
                return;
            }
            
            _lastSuperAttackTime = Time.time;
            
            _animator.SetTrigger("SwordDoubleAttack");
            
            List<IEnemy> Enemies = _enemySpawner.AliveEnemies;
            
            foreach (IEnemy enemy in Enemies)
            {
                if (Vector3.Distance(_playerGameObject.transform.position, enemy.EnemyGameObject.transform.position) < Stats.AttackRange)
                {
                    enemy.TakeDamage(Stats.Damage * 2);
                    _playerGameObject.transform.LookAt(enemy.EnemyGameObject.transform.position);
                    return;
                }
            }
        }

        private void OnUpdate()
        {
            if (PlayerGameObject == null)
            { 
                _update.OnUpdate -= OnUpdate;
                return;
            }
            
            if (_isDead)
            {
                return;
            }
            
            Move();
        }

        private void Move()
        {
            RawInput input = _inputSource.GetInput();
            Vector3 moveVector = new Vector3(input.Movement.x, 0, input.Movement.y) * Stats.MoveSpeed;
            _rb.AddForce(moveVector);
            _playerGameObject.transform.LookAt(_playerGameObject.transform.position + moveVector);
            _animator.SetFloat("Speed", moveVector.magnitude);
        }

        private void Die()
        {
            _isDead = true;
            _animator.SetTrigger("Die");
            OnDeath?.Invoke(this);
        }

        private void OnEnemiesCountChanged(int obj)
        {
            Stats.HP++;
        }
    }
}