using System.Collections.Generic;
using GameMode.LevelMode;
using Script.GameMode.LevelMode;
using UnityEngine;

namespace Utilities
{

    [System.Serializable]
    public class LevelDataWrapper
    {
        public List<LevelData> data;
    }

    public static class LevelDataLoaderExtension
    {
        private static List<LevelData> cachedLevels;

        public static void LoadAllLevels()
        {
            if (cachedLevels != null) return;

            TextAsset jsonFile = Resources.Load<TextAsset>("Level Data/levelData");
            LevelDataWrapper wrapper = JsonUtility.FromJson<LevelDataWrapper>(jsonFile.text);
            cachedLevels = wrapper.data;
        }

        public static LevelData GetLevel(int index)
        {
            if (cachedLevels == null)
                LoadAllLevels();

            return cachedLevels[index];
        }

        public static void ConvertToScriptable(LevelData levelData, LevelModeSo levelSo)
        {
            levelSo.bugCount = levelData.bugCount;
            levelSo.wordCount = levelData.wordCount;
            levelSo.timeSec = levelData.timeSec;
            levelSo.totalScore = levelData.totalScore;
            levelSo.gridSize = levelData.gridSize;
            levelSo.gridData = levelData.gridData;
        }
    }


}