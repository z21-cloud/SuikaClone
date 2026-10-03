using SuikaClone.Core;
using SuikaClone.Fruits;
using SuikaClone.Pools;
using UnityEngine;

public class PlayerController : MonoBehaviour, IInputReader
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private BaseFruitPool _fruitPool;

    private const float _minYPosition = 3f;
    private const float _maxYPosition = 4f;

    private const float _minXPosition = -2;
    private const float _maxXPosition = 2;

    public bool MouseLMB => _inputManager.MouseLMB;
    public Vector2 MousePosition => _inputManager.MousePosition;


    private void Update()
    {
        if (MouseLMB)
        {
            Debug.Log($"Mouse Position: {MousePosition}");
            var fruit = _fruitPool.Get();

            if (fruit != null)
            {
                MoveFruitToMousePosition(fruit);
            }
            else
            {
                Debug.LogError("[PlayerController] No available fruit in the pool.");
            }
        }
    }

    private void MoveFruitToMousePosition(BaseFruit fruit)
    {
        if(!fruit.TryGetComponent(out Rigidbody2D rb))
        {
            Debug.LogError("[PlayerController] Fruit does not have a Rigidbody2D component.");
            return;
        }
        
        Vector2 mouseScreenPosition = MousePosition;
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        Vector2 spawnPosition = new(
            Mathf.Clamp(mouseWorldPosition.x, _minXPosition, _maxXPosition),
            Mathf.Clamp(mouseWorldPosition.y, _minYPosition, _maxYPosition)
        );

        fruit.transform.position = spawnPosition;
        fruit.gameObject.SetActive(true);

        rb.position = spawnPosition;
        rb.linearVelocity = Vector2.zero; // Reset velocity to avoid unexpected movement
        rb.angularVelocity = 0f; // Reset angular velocity to avoid unexpected rotation
        Physics2D.SyncTransforms(); // Ensure the physics engine is aware of the new position
    }
}
