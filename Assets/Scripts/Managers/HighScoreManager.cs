using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SuikaClone.Managers
{
    public class HighScoreManager
    {
        private const int MaxScores = 3;
        private readonly List<int> _scores = new();

        public IReadOnlyList<int> Scores => _scores;

        private string SavePath => Path.Combine(Application.persistentDataPath, "highscores.json");

        public void UpdateScore(int newScore)
        {
            _scores.Add(newScore);
            SortAndLimitScores();
            Save();
        }

        public void Load()
        {
            _scores.Clear();

            string json;
            try
            {
                json = File.ReadAllText(SavePath);
            }
            catch (FileNotFoundException)
            {
                return;
            }
            catch (DirectoryNotFoundException)
            {
                return;
            }
            catch (IOException exception)
            {
                Debug.LogError($"[HighScoreManager] Could not read high scores from '{SavePath}': {exception}");
                return;
            }
            catch (UnauthorizedAccessException exception)
            {
                Debug.LogError($"[HighScoreManager] Could not read high scores from '{SavePath}': {exception}");
                return;
            }

            HighScoreData data;
            try
            {
                data = JsonUtility.FromJson<HighScoreData>(json);
            }
            catch (ArgumentException exception)
            {
                Debug.LogError($"[HighScoreManager] Could not parse high scores from '{SavePath}': {exception}");
                return;
            }

            if (data?.Scores == null)
            {
                Debug.LogError($"[HighScoreManager] Invalid high score data in '{SavePath}'.");
                return;
            }

            _scores.AddRange(data.Scores);
            SortAndLimitScores();
        }

        private void Save()
        {
            HighScoreData data = new()
            {
                Scores = new List<int>(_scores)
            };

            try
            {
                string json = JsonUtility.ToJson(data, true);
                Directory.CreateDirectory(Application.persistentDataPath);
                File.WriteAllText(SavePath, json);
            }
            catch (IOException exception)
            {
                Debug.LogError($"[HighScoreManager] Could not save high scores to '{SavePath}': {exception}");
            }
            catch (UnauthorizedAccessException exception)
            {
                Debug.LogError($"[HighScoreManager] Could not save high scores to '{SavePath}': {exception}");
            }
            catch (ArgumentException exception)
            {
                Debug.LogError($"[HighScoreManager] Could not save high scores to '{SavePath}': {exception}");
            }
        }

        private void SortAndLimitScores()
        {
            _scores.Sort((left, right) => right.CompareTo(left));

            if (_scores.Count > MaxScores)
            {
                _scores.RemoveRange(MaxScores, _scores.Count - MaxScores);
            }
        }
    }
}
