using SuikaClone.Core;
using SuikaClone.Fruits;
using SuikaClone.Pools;
using UnityEngine;

public class PlayerController : MonoBehaviour, IInputReader
{
    [SerializeField] private InputManager _inputManager;

    private const float _spawnRate = 2f; // Time in seconds between fruit spawns
    // private const float _minYPosition = 3f;
    // private const float _maxYPosition = 4f;

    private readonly Vector2 _minSpawnPosition = new(-2, 3.75f);
    private readonly Vector2 _maxSpawnPosition = new(2, 5);

    // private const float _minXPosition = -2;
    // private const float _maxXPosition = 2;

    public bool MouseLMB => _inputManager.MouseLMB;
    public Vector2 MousePosition => _inputManager.MousePosition;

    private FruitSpawnManager _fruitSpawnManager;
    private Fruit _currentFruit;
    private float _timer;

    public void Initialize(FruitSpawnManager fruitSpawnManager)
    {
        _fruitSpawnManager = fruitSpawnManager;
        _timer = _spawnRate; // Initialize the timer to allow immediate spawning
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_currentFruit == null && _timer >= _spawnRate)
        {
            Vector2 positionToSpawn = ClampMousePosition();
            _currentFruit = _fruitSpawnManager.SpawnRandomFruit(positionToSpawn);
        }

        if (_currentFruit == null)
        {
            Debug.Log("[PlayerController] Current fruit is null. Cannot proceed with spawning.");
            return;
        }

        if (!_currentFruit.TryGetComponent(out Rigidbody2D fruitRb))
        {
            Debug.LogError("[PlayerController] No available fruit in the pool.");
            return;
        }

        fruitRb.bodyType = RigidbodyType2D.Kinematic; // Make the fruit kinematic to prevent physics interactions while moving
        Vector2 spawnPosition = ClampMousePosition();
        _currentFruit.transform.position = spawnPosition;

        if (MouseLMB)
        {
            Debug.Log($"[PlayerController] Fruit Spawned at Position: {spawnPosition}");
            MoveFruitToMousePosition(fruitRb, spawnPosition);
        }
    }

    private Vector2 ClampMousePosition()
    {
        Vector2 mouseScreenPosition = MousePosition;
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        Vector2 spawnPosition = new(
            Mathf.Clamp(mouseWorldPosition.x, _minSpawnPosition.x, _maxSpawnPosition.x),
            Mathf.Clamp(mouseWorldPosition.y, _minSpawnPosition.y, _maxSpawnPosition.y)
        );
        return spawnPosition;
    }

    private void MoveFruitToMousePosition(Rigidbody2D fruitRb, Vector3 spawnPosition)
    {
        fruitRb.position = spawnPosition;
        fruitRb.linearVelocity = Vector2.zero; // Reset velocity to avoid unexpected movement
        fruitRb.angularVelocity = 0f; // Reset angular velocity to avoid unexpected rotation
        fruitRb.bodyType = RigidbodyType2D.Dynamic; // Make the fruit dynamic to allow physics interactions
        Physics2D.SyncTransforms(); // Ensure the physics engine is aware of the new position
        _currentFruit = null; // Reset the current fruit reference after moving it
        _timer = 0f; // Reset the timer after spawning a fruit
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((_minSpawnPosition + _maxSpawnPosition) / 2, _maxSpawnPosition - _minSpawnPosition);
    }
}