using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SuikaClone.Managers
{
    [Serializable]
    public class HighScoreManager
    {
        public List<int> Scores = new();

        private string SavePath => Path.Combine(Application.persistentDataPath, "highscores.json");
        
        public void UpdateScore(int newScore)
        {
            Scores.Add(newScore);

            SortScore();

            if (Scores.Count > 3) Scores.RemoveRange(3, Scores.Count - 3);

            Save();
        }


        public void Load()
        {
            if (!File.Exists(SavePath)) return;

            string json = File.ReadAllText(SavePath);
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);

            if (data == null || data.Scores == null) return;

            Scores = data.Scores;
            SortScore();

            if (Scores.Count > 3) Scores.RemoveRange(3, Scores.Count - 3);
        }

        private void Save()
        {
            HighScoreData data = new HighScoreData
            {
                Scores = Scores
            };

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(SavePath, json);
        }

        private void SortScore()
        {
            for (int i = 0; i < Scores.Count - 1; i++)
            {
                int maxIndex = i;
                for (int j = i + 1; j < Scores.Count; j++)
                {
                    if (Scores[maxIndex] < Scores[j])
                    {
                        maxIndex = j;
                    }
                }

                if (maxIndex != i)
                {
                    int temp = Scores[i];
                    Scores[i] = Scores[maxIndex];
                    Scores[maxIndex] = temp;
                }
            }
        }
    }
}
