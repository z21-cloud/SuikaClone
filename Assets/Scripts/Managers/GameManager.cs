using SuikaClone.Core;
using TMPro;
using UnityEngine;

namespace SuikaClone.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _loseUI;
        [SerializeField] private TMP_Text[] _highScoreTexts;
        private PlayerController _playerController;
        private FruitSpawnManager _fruitSpawnManager;
        private ScoreManager _scoreManager;
        private HighScoreManager _highScoreManager;
        
        public void Initialize(PlayerController playerController, FruitSpawnManager fruitSpawnManager, ScoreManager scoreManager, HighScoreManager highScoreManager)
        {
            _playerController = playerController;
            _fruitSpawnManager = fruitSpawnManager;
            _scoreManager = scoreManager;
            _highScoreManager = highScoreManager;

            RefreshHighScores();
            if (_loseUI != null) _loseUI.SetActive(false);
            else Debug.LogWarning($"[GameManager] loseUI is null!");
        }

        public void GameOver()
        {
            Debug.LogWarning("[GameManager] Game over");

            _playerController.enabled = false; // Disable player controls
            Debug.Log("[GameManager] Player controls disabled.");
            Debug.Log($"[GameManager] Final Score: {_scoreManager.CurrentScore}"); // Log the final score

            _highScoreManager.UpdateScore(_scoreManager.CurrentScore);
            RefreshHighScores();

            _fruitSpawnManager.ReturnAllActiveFruits(); // Return all active fruits
            _fruitSpawnManager.StopAllCoroutines(); // Stop any ongoing fruit spawning coroutines
            _fruitSpawnManager.AutomaticallySpawnFruitsRandomly();
            
            _loseUI.SetActive(true);
        }

        private void RefreshHighScores()
        {
            if (_highScoreTexts == null)
            {
                Debug.LogError("[GameManager] High score text fields are not configured.", this);
                return;
            }

            for (int i = 0; i < _highScoreTexts.Length; i++)
            {
                if (_highScoreTexts[i] == null)
                {
                    Debug.LogError($"[GameManager] High score text at index {i} is not assigned.", this);
                    continue;
                }

                int score = i < _highScoreManager.Scores.Count ? _highScoreManager.Scores[i] : 0;
                _highScoreTexts[i].text = score.ToString();
            }
        }
    }
}
