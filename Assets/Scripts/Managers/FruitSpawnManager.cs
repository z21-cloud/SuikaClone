using System.Collections.Generic;
using SuikaClone.Factories;
using SuikaClone.Fruits;
using SuikaClone.Managers;
using SuikaClone.Pools;
using UnityEngine;

public class FruitSpawnManager : MonoBehaviour
{
    [Header("Base Fruit Pools")]
    [SerializeField] private BaseFruitPool baseFruitPool;
    [SerializeField] private BaseRedPool baseRedPool;
    [SerializeField] private BaseGreenPool baseGreenPool;
    [SerializeField] private BaseBluePool baseBluePool;

    [Header("Bubble Fruit Pools")]
    [SerializeField] private BubbleFruitPool bubbleFruitPool;
    [SerializeField] private BubbleRedFruitPool bubbleRedFruitPool;
    [SerializeField] private BubbleGreenFruitPool bubbleGreenFruitPool;
    [SerializeField] private BubbleBlueFruitPool bubbleBlueFruitPool;

    [Header("Square Fruit Pools")]
    [SerializeField] private SquareFruitPool squareFruitPool;
    [SerializeField] private SquareRedFruitPool squareRedFruitPool;
    [SerializeField] private SquareGreenFruitPool squareGreenFruitPool;

    private List<Fruit> _activeFruits = new List<Fruit>();

    public void Initialize(
        BaseFruitFactory baseFruitFactory,
        BaseRedFruitFactory baseRedFruitFactory,
        BaseGreenFruitFactory baseGreenFruitFactory,
        BaseBlueFruitFactory baseBlueFruitFactory,
        BubbleFruitFactory bubbleFruitFactory,
        BubbleRedFruitFactory bubbleRedFruitFactory,
        BubbleGreenFruitFactory bubbleGreenFruitFactory,
        BubbleBlueFruitFactory bubbleBlueFruitFactory,
        SquareFruitFactory squareFruitFactory,
        SquareRedFruitFactory squareRedFruitFactory,
        SquareGreenFruitFactory squareGreenFruitFactory,
        MergeManager mergeManager)
    {
        if (baseFruitFactory == null || baseRedFruitFactory == null || baseGreenFruitFactory == null || baseBlueFruitFactory == null ||
            bubbleFruitFactory == null || bubbleRedFruitFactory == null || bubbleGreenFruitFactory == null || bubbleBlueFruitFactory == null ||
            squareFruitFactory == null || squareRedFruitFactory == null || squareGreenFruitFactory == null || mergeManager == null ||
            baseFruitPool == null || baseRedPool == null || baseGreenPool == null || baseBluePool == null ||
            bubbleFruitPool == null || bubbleRedFruitPool == null || bubbleGreenFruitPool == null || bubbleBlueFruitPool == null ||
            squareFruitPool == null || squareRedFruitPool == null || squareGreenFruitPool == null)
        {
            Debug.LogError("[FruitSpawnManager] All fruit factories and pools must be assigned before initialization.");
            return;
        }

        // Factory initialization
        baseFruitFactory.Initialize(mergeManager);
        baseRedFruitFactory.Initialize(mergeManager);
        baseGreenFruitFactory.Initialize(mergeManager);
        baseBlueFruitFactory.Initialize(mergeManager);
        bubbleFruitFactory.Initialize(mergeManager);
        bubbleRedFruitFactory.Initialize(mergeManager);
        bubbleGreenFruitFactory.Initialize(mergeManager);
        bubbleBlueFruitFactory.Initialize(mergeManager);
        squareFruitFactory.Initialize(mergeManager);
        squareRedFruitFactory.Initialize(mergeManager);
        squareGreenFruitFactory.Initialize(mergeManager);

        // Pool initialization
        baseFruitPool.Initialize(baseFruitFactory);
        baseRedPool.Initialize(baseRedFruitFactory);
        baseGreenPool.Initialize(baseGreenFruitFactory);
        baseBluePool.Initialize(baseBlueFruitFactory);
        bubbleFruitPool.Initialize(bubbleFruitFactory);
        bubbleRedFruitPool.Initialize(bubbleRedFruitFactory);
        bubbleGreenFruitPool.Initialize(bubbleGreenFruitFactory);
        bubbleBlueFruitPool.Initialize(bubbleBlueFruitFactory);
        squareFruitPool.Initialize(squareFruitFactory);
        squareRedFruitPool.Initialize(squareRedFruitFactory);
        squareGreenFruitPool.Initialize(squareGreenFruitFactory);
    }

    public Fruit SpawnFruit(int fruitLevel, Vector3 position)
    {
        FruitType fruitType = GetFruitType(fruitLevel);
        Fruit spawnedFruit = GetFruitObjectByType(fruitType);

        if (spawnedFruit != null)
        {
            spawnedFruit.IsMerging = false; // Reset merging state when spawning
            _activeFruits.Add(spawnedFruit);

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
            1 => FruitType.BaseRed,
            2 => FruitType.BaseGreen,
            3 => FruitType.BaseBlue,
            4 => FruitType.Bubble,
            5 => FruitType.BubbleRed,
            6 => FruitType.BubbleGreen,
            7 => FruitType.BubbleBlue,
            8 => FruitType.Square,
            9 => FruitType.SquareRed,
            10 => FruitType.SquareGreen,
            _ => FruitType.Base
        };
    }

    private Fruit GetFruitObjectByType(FruitType fruitType)
    {
        return fruitType switch
        {
            FruitType.Base => baseFruitPool.Get(),
            FruitType.BaseRed => baseRedPool.Get(),
            FruitType.BaseGreen => baseGreenPool.Get(),
            FruitType.BaseBlue => baseBluePool.Get(),
            FruitType.Bubble => bubbleFruitPool.Get(),
            FruitType.BubbleRed => bubbleRedFruitPool.Get(),
            FruitType.BubbleGreen => bubbleGreenFruitPool.Get(),
            FruitType.BubbleBlue => bubbleBlueFruitPool.Get(),
            FruitType.Square => squareFruitPool.Get(),
            FruitType.SquareRed => squareRedFruitPool.Get(),
            FruitType.SquareGreen => squareGreenFruitPool.Get(),
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
            case FruitType.BaseRed:
                baseRedPool.Return(fruit);
                break;
            case FruitType.BaseGreen:
                baseGreenPool.Return(fruit);
                break;
            case FruitType.BaseBlue:
                baseBluePool.Return(fruit);
                break;
            case FruitType.Bubble:
                bubbleFruitPool.Return(fruit);
                break;
            case FruitType.BubbleRed:
                bubbleRedFruitPool.Return(fruit);
                break;
            case FruitType.BubbleGreen:
                bubbleGreenFruitPool.Return(fruit);
                break;
            case FruitType.BubbleBlue:
                bubbleBlueFruitPool.Return(fruit);
                break;
            case FruitType.Square:
                squareFruitPool.Return(fruit);
                break;
            case FruitType.SquareRed:
                squareRedFruitPool.Return(fruit);
                break;
            case FruitType.SquareGreen:
                squareGreenFruitPool.Return(fruit);
                break;
            default:
                Debug.LogError($"Unsupported fruit type: {fruit.FruitData.FruitType}");
                break;
        }
    }

    public Fruit SpawnRandomFruit(Vector3 position)
    {
        int randomLevel = Random.Range(0, 4); // Randomly select a level between 0 and 4
        return SpawnFruit(randomLevel, position);
    }

    public void ReturnAllActiveFruits()
    {
        foreach (var fruit in _activeFruits)
        {
            ReturnFruit(fruit);
        }
        _activeFruits.Clear();
    }

    public void AutomaticallySpawnFruitsRandomly()
    {
        // Start a coroutine to spawn fruits at random intervals
        StartCoroutine(SpawnFruitsCoroutine());
    }

    private System.Collections.IEnumerator SpawnFruitsCoroutine()
    {
        while (true)
        {
            // Randomly select a position within the defined range
            float randomX = Random.Range(-2f, 2f);
            float randomY = Random.Range(3.75f, 5f);
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

            // Spawn a random fruit at the selected position
            SpawnRandomFruit(spawnPosition);

            // Wait for a random interval before spawning the next fruit
            float randomInterval = Random.Range(1f, 3f); // Adjust the range as needed
            yield return new WaitForSeconds(randomInterval);
        }
    }
}
