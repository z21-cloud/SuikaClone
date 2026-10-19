using UnityEngine;
using TMPro;

namespace SuikaClone.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private int _currentScore = 0;
        [SerializeField] private TMP_Text _scoreText;

        public int CurrentScore => _currentScore;

        public void Start()
        {
            if (_scoreText != null) _scoreText.text = $"{_currentScore}";
            else Debug.LogWarning($"[ScoreManager] scoreText is null!");
        }

        public void AddScore(int scoreToAdd)
        {
            _currentScore += scoreToAdd;
            // Debug.Log($"[ScoreManager] Current Score: {_currentScore}");
            if (_scoreText != null) _scoreText.text = $"{CurrentScore}";
            else Debug.LogWarning($"[ScoreManager] scoreText is null!");
        }
    }
}