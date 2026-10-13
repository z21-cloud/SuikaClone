using SuikaClone.Core;
using SuikaClone.Fruits;
using UnityEngine;

public class LoseLine : MonoBehaviour
{
    private const float _loseTimer = 5f; // Time in seconds before the game is considered lost after a fruit crosses the lose line
    private float _timer;
    private GameManager _gameManager;
    public void Initialize(GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Fruit fruit) && fruit.TryGetComponent(out Rigidbody2D rb))
        {
            _timer += Time.deltaTime;
            
            if(rb.bodyType == RigidbodyType2D.Dynamic && _timer >= _loseTimer)
            {
                Debug.Log("[LoseLine] Fruit has crossed the lose line. Game Over!");
                // Here you can implement game over logic, such as stopping the game, showing a game over screen, etc.
                _gameManager.GameOver();
                _timer = 0f; // Reset the timer after triggering game over
            }
        }
    }
}
