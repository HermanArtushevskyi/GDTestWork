using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Enemies
{
    [CreateAssetMenu(fileName = "ScriptableEnemy", menuName = "Game/ScriptableEnemy", order = 0)]
    public class ScriptableEnemy : ScriptableObject
    {
        public EnemyStats Stats;
        public GameObject Prefab;
    }
}