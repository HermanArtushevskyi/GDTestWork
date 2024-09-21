using System;
using System.Collections.Generic;
using _Project.CodeBase.Runtime.Gameplay.Character.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.Entities.Interfaces;
using _Project.CodeBase.Runtime.Gameplay.GameLoop.Interfaces;
using _Project.CodeBase.Runtime.UnityContext.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.CodeBase.Runtime.Gameplay.Character
{
    public class Player : IPlayer
    {
        public PlayerStats Stats { get; private set; }
        public GameObject PlayerGameObject => _playerGameObject;
        public float HP { get => Stats.HP; }

        private float _lastAttackTime;
        private bool _isDead;
        
        private readonly GameObject _playerGameObject;
        private readonly IEnemySpawner _enemySpawner;
        private Animator _animator;
        private IUpdate _update;


        public Player(PlayerStats stats, IUpdate update, Animator animator, GameObject playerGameObject,
            IEnemySpawner enemySpawner)
        {
            Stats = stats.Clone();
            _update = update;
            _animator = animator;
            _playerGameObject = playerGameObject;
            _enemySpawner = enemySpawner;
            _update.OnUpdate += OnUpdate;
        }

        public void TakeDamage(float damage)
        {
            Stats.HP -= damage;
            
            if (Stats.HP <= 0)
            {
                Die();
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

            List<IEnemy> enemies =_enemySpawner.AliveEnemies;
            IEnemy closestEnemie = null;

            for (int i = 0; i < enemies.Count; i++)
            {
                var enemie = enemies[i];
                if (enemie == null)
                {
                    continue;
                }

                if (closestEnemie == null)
                {
                    closestEnemie = enemie;
                    continue;
                }

                var distance = Vector3.Distance(_playerGameObject.transform.position, enemie.EnemyGameObject.transform.position);
                var closestDistance =
                    Vector3.Distance(_playerGameObject.transform.position, closestEnemie.EnemyGameObject.transform.position);

                if (distance < closestDistance)
                {
                    closestEnemie = enemie;
                }
            }

            if (closestEnemie != null)
            {
                var distance = Vector3.Distance(_playerGameObject.transform.position, closestEnemie.EnemyGameObject.transform.position);
                if (distance <= Stats.AttackRange)
                {
                    if (Time.time - _lastAttackTime > Stats.AttackRange)
                    {
                        //transform.LookAt(closestEnemie.transform);
                        _playerGameObject.transform.rotation =
                            Quaternion.LookRotation(closestEnemie.EnemyGameObject.transform.position -
                                                    _playerGameObject.transform.position);

                        _lastAttackTime = Time.time;
                        closestEnemie.TakeDamage(Stats.Damage);
                        _animator.SetTrigger("Attack");
                    }
                }
            }
        }

        private void Die()
        {
            _isDead = true;
            _animator.SetTrigger("Die");

            SceneManager.Instance.GameOver();
        }
    }
}