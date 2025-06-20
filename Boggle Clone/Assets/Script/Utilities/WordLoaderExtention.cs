using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class WordLoaderExtention
    {
        public static HashSet<string> GetWordList(string resourcePath)
        {
            TextAsset wordTextFile = Resources.Load<TextAsset>(resourcePath);

            if (wordTextFile == null)
            {
                Debug.LogError("Word list file not found at Resources/" + resourcePath);
                return null;
            }
            string[] words = wordTextFile.text.Split(new[]{'\r','\n'},System.StringSplitOptions.RemoveEmptyEntries);
            HashSet<string> wordSet = new HashSet<string>(words, System.StringComparer.OrdinalIgnoreCase);
            return wordSet;
        }
    }
}