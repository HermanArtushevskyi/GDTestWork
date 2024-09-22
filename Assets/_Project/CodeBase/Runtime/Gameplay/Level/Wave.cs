using System;
using System.Collections;
using System.Collections.Generic;
using _Project.CodeBase.Runtime.Gameplay.Enemies;
using _Project.CodeBase.Runtime.Gameplay.Enemies.Common;
using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "Wave", menuName = "Data/Waves")]
    [Serializable]
    public class Wave : ScriptableObject
    {
        public ScriptableEnemy[] Characters;
    }
}