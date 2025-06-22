using System.Collections.Generic;
using GameMode.BaseMode;
using GameMode.Tiles;
using UnityEngine;

namespace Script.GameMode.LevelMode
{
    [CreateAssetMenu(fileName = "Level Mode", menuName = "ScriptableObjects/GameMode/LevelMode")]
    public class LevelModeSo : BaseModeSo
    {
        [Header("Level")] 
        public int numberOfLevels;
    }
}