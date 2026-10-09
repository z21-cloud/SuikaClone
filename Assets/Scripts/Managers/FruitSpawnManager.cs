using SuikaClone.Fruits;
using SuikaClone.Managers;
using SuikaClone.Pools;
using UnityEngine;

public class FruitSpawnManager : MonoBehaviour
{
    [SerializeField] private BaseFruitPool baseFruitPool;
    [SerializeField] private BubbleFruitPool bubbleFruitPool;
    [SerializeField] private SquareFruitPool squareFruitPool;

    public Fruit SpawnFruit(int fruitLevel, Vector3 position)
    {
        FruitType fruitType = GetFruitType(fruitLevel);
        Fruit spawnedFruit = GetFruitObject(fruitType);

        if (spawnedFruit != null)
        {
            spawnedFruit.IsMerging = false; // Reset merging state when spawning
            
            spawnedFruit.transform.position = position;
            spawnedFruit.gameObject.SetActive(true);
            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized; // Random direction
            spawnedFruit.GetComponent<Rigidbody2D>().AddForce(randomDirection * 7.5f, ForceMode2D.Impulse); // Add force for visual effect    
            Debug.Log($"[SpawnManager]: Spawned object - {spawnedFruit.FruitData.FruitType}");
            return spawnedFruit;
        }
        else
        {
            Debug.LogError($"[SpawnedManager] Spawned object is null! Return null...");
            return null;
        }
    }

    private Fruit GetFruitObject(FruitType fruitType)
    {
        Fruit spawnedFruit = null;

        switch (fruitType)
        {
            case FruitType.Base:
                spawnedFruit = baseFruitPool.Get();
                break;
            case FruitType.Bubble:
                spawnedFruit = bubbleFruitPool.Get();
                break;
            case FruitType.Square:
                spawnedFruit = squareFruitPool.Get();
                break;
            default:
                Debug.LogError($"Unsupported fruit type: {fruitType}");
                break;
        }

        return spawnedFruit;
    }

    public void ReturnFruit(Fruit fruit)
    {
        switch (fruit.FruitData.FruitType)
        {
            case FruitType.Base:
                baseFruitPool.Return(fruit);
                break;
            case FruitType.Bubble:
                bubbleFruitPool.Return(fruit);
                break;
            case FruitType.Square:
                squareFruitPool.Return(fruit);
                break;
            default:
                Debug.LogError($"Unsupported fruit type: {fruit.FruitData.FruitType}");
                break;
        }
    }

    private FruitType GetFruitType(int level)
    {
        return level switch
        {
            0 => FruitType.Base,
            1 => FruitType.Bubble,
            2 => FruitType.Square,
            _ => FruitType.Base
        };
    }
}
