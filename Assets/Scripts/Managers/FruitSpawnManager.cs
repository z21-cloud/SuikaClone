using SuikaClone.Factories;
using SuikaClone.Fruits;
using SuikaClone.Managers;
using SuikaClone.Pools;
using UnityEngine;

public class FruitSpawnManager : MonoBehaviour
{
    [SerializeField] private BaseFruitPool baseFruitPool;
    [SerializeField] private BubbleFruitPool bubbleFruitPool;
    [SerializeField] private SquareFruitPool squareFruitPool;

    public void Initialize(BaseFruitFactory baseFruitFactory, BubbleFruitFactory bubbleFruitFactory, SquareFruitFactory squareFruitFactory, MergeManager mergeManager)
    {
        if (baseFruitPool == null || bubbleFruitPool == null || squareFruitPool == null)
        {
            Debug.LogError("[FruitSpawnManager] One or more fruit pools are not assigned in the inspector.");
        }

        // Factory initialization
        baseFruitFactory.Initialize(mergeManager);
        bubbleFruitFactory.Initialize(mergeManager);
        squareFruitFactory.Initalize(mergeManager);

        // Pool initialization
        baseFruitPool.Initialize(baseFruitFactory);
        bubbleFruitPool.Initialize(bubbleFruitFactory);
        squareFruitPool.Initialize(squareFruitFactory);
    }

    public Fruit SpawnFruit(int fruitLevel, Vector3 position)
    {
        FruitType fruitType = GetFruitType(fruitLevel);
        Fruit spawnedFruit = GetFruitObjectByType(fruitType);

        if (spawnedFruit != null)
        {
            spawnedFruit.IsMerging = false; // Reset merging state when spawning

            spawnedFruit.transform.position = position;
            spawnedFruit.gameObject.SetActive(true);    
            // Debug.Log($"[SpawnManager]: Spawned object - {spawnedFruit.FruitData.FruitType}");
            return spawnedFruit;
        }
        else
        {
            Debug.LogError($"[SpawnedManager] Spawned object is null! Return null...");
            return null;
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

    private Fruit GetFruitObjectByType(FruitType fruitType)
    {
        return fruitType switch
        {
            FruitType.Base => baseFruitPool.Get(),
            FruitType.Bubble => bubbleFruitPool.Get(),
            FruitType.Square => squareFruitPool.Get(),
            _ => null
        };
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

    public Fruit SpawnRandomFruit(Vector3 position)
    {
        int randomLevel = Random.Range(0, 3); // Randomly select a level between 0 and 2
        return SpawnFruit(randomLevel, position);
    }
}
