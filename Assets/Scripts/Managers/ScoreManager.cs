using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int _currentScore = 0;

    public int CurrentScore => _currentScore;

    public void Initialize()
    {
        // Initialization logic if needed
    }

    public void AddScore(int scoreToAdd)
    {
        _currentScore += scoreToAdd;
        Debug.Log($"[ScoreManager] Current Score: {_currentScore}");
    }
}
