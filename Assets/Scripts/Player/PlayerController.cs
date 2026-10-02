using SuikaClone.Core;
using SuikaClone.Fruits;
using SuikaClone.Pools;
using UnityEngine;

public class PlayerController : MonoBehaviour, IInputReader
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private BaseFruitPool _fruitPool;

    private const float _minYPosition = 3f;
    private const float _maxYPosition = 3f;

    private const float _minXPosition = -2;
    private const float _maxXPosition = 2;

    public bool MouseLMB => _inputManager.MouseLMB;
    public Vector2 MousePosition => _inputManager.MousePosition;


    private void Update()
    {
        if (MouseLMB)
        {
            Debug.Log($"Mouse Position: {MousePosition}");
            var fruit = _fruitPool.GetFruit();

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
        Vector2 mouseScreenPosition = MousePosition;
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        fruit.transform.position = new Vector2(
            Mathf.Clamp(mouseWorldPosition.x, _minXPosition, _maxXPosition),
            Mathf.Clamp(mouseWorldPosition.y, _minYPosition, _maxYPosition)
        );
        
        fruit.gameObject.SetActive(true);
    }
}
