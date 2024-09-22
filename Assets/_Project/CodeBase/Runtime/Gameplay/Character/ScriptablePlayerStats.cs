using UnityEngine;

namespace _Project.CodeBase.Runtime.Gameplay.Character
{
    [CreateAssetMenu(fileName = "PlayerStat", menuName = "Game/PlayerStats", order = 0)]
    public class ScriptablePlayerStats : ScriptableObject
    {
        public PlayerStats Stats;
    }
}