using SuikaClone.Core;
using UnityEngine;

namespace SuikaClone.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject ui;
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
            
            ui.SetActive(false);
        }

        public void GameOver()
        {
            Debug.Log("[GameManager] Game Over triggered. Implement game over logic here.");
            Debug.LogWarning("[GameManager] Game over");
            // Implement game over logic here, such as stopping the game, showing a game over screen

            _playerController.enabled = false; // Disable player controls
            Debug.Log("[GameManager] Player controls disabled.");
            Debug.Log($"[GameManager] Final Score: {_scoreManager.CurrentScore}"); // Log the final score

            _highScoreManager.UpdateScore(_scoreManager.CurrentScore);

            _fruitSpawnManager.ReturnAllActiveFruits(); // Return all active fruits
            // Additional game over logic can be added here, such as resetting the game or returning to
            _fruitSpawnManager.StopAllCoroutines(); // Stop any ongoing fruit spawning coroutines
            _fruitSpawnManager.AutomaticallySpawnFruitsRandomly();
            
            ui.SetActive(true);
        }
    }
}
