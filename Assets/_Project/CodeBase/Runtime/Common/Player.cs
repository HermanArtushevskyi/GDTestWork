using System.Collections;
using System.Collections.Generic;
using _Project.CodeBase.Runtime.Gameplay.Enemies;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Interfaces;
using UnityEngine;

namespace Common
{
    public class Player : MonoBehaviour
    {
        public float Hp;
        public float Damage;
        public float AtackSpeed;
        public float AttackRange = 2;

        private float lastAttackTime = 0;
        private bool isDead = false;
        public Animator AnimatorController;

        private void Update()
        {
            if (isDead)
            {
                return;
            }

            if (Hp <= 0)
            {
                Die();
                return;
            }


            var enemies = SceneManager.Instance.Enemies;
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

                var distance = Vector3.Distance(transform.position, enemie.EnemyGameObject.transform.position);
                var closestDistance = Vector3.Distance(transform.position, closestEnemie.EnemyGameObject.transform.position);

                if (distance < closestDistance)
                {
                    closestEnemie = enemie;
                }

            }

            if (closestEnemie != null)
            {
                var distance = Vector3.Distance(transform.position, closestEnemie.EnemyGameObject.transform.position);
                if (distance <= AttackRange)
                {
                    if (Time.time - lastAttackTime > AtackSpeed)
                    {
                        //transform.LookAt(closestEnemie.transform);
                        transform.transform.rotation =
                            Quaternion.LookRotation(closestEnemie.EnemyGameObject.transform.position - transform.position);

                        lastAttackTime = Time.time;
                        AnimatorController.SetTrigger("Attack");
                    }
                }
            }
        }

        private void Die()
        {
            isDead = true;
            AnimatorController.SetTrigger("Die");

            SceneManager.Instance.GameOver();
        }


    }
}
