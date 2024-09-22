using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Enemies.Common
{
    [CreateAssetMenu(fileName = "EnemyCollection", menuName = "ScriptableObjects/Enemies/EnemyCollection", order = 1)]
    public class ScriptableEnemyCollection : ScriptableObject
    {
        public ScriptableEnemy RegularEnemy;
        public ScriptableEnemy BossEnemy;
    }
}