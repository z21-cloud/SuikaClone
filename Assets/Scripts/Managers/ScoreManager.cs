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
            _scoreText.text = $"0";
        }

        public void AddScore(int scoreToAdd)
        {
            _currentScore += scoreToAdd;
            // Debug.Log($"[ScoreManager] Current Score: {_currentScore}");
            _scoreText.text = $"{CurrentScore}";
        }
    }
}