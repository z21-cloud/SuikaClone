using System.Collections.Generic;
using SuikaClone.Fruits;
using SuikaClone.Managers;
using UnityEngine;

namespace SuikaClone.Core
{
    public class LoseLine : MonoBehaviour
    {
        private const float _loseTimer = 5f; // Time in seconds before the game is considered lost after a fruit crosses the lose line
        private readonly Dictionary<Collider2D, float> _fruitTimers = new Dictionary<Collider2D, float>();
        private GameManager _gameManager;
        private bool _gameOverTriggered;

        public void Initialize(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (_gameOverTriggered || !collision.TryGetComponent(out Fruit fruit) ||
                !fruit.TryGetComponent(out Rigidbody2D rb))
            {
                return;
            }

            if (rb.bodyType != RigidbodyType2D.Dynamic)
            {
                _fruitTimers.Remove(collision);
                return;
            }

            _fruitTimers.TryGetValue(collision, out float timer);
            timer += Time.fixedDeltaTime;
            _fruitTimers[collision] = timer;

            if (timer >= _loseTimer)
            {
                Debug.Log("[LoseLine] Fruit has crossed the lose line. Game Over!");
                _gameOverTriggered = true;
                _gameManager.GameOver();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _fruitTimers.Remove(collision);
        }
    }
}
