using SuikaClone.Fruits;
using SuikaClone.Managers;
using SuikaClone.Pools;
using UnityEngine;

public class FruitSpawnManager : MonoBehaviour
{
    [SerializeField] private BaseFruitPool baseFruitPool;
    [SerializeField] private BubbleFruitPool bubbleFruitPool;

    public Fruit SpawnFruit(int fruitLevel, Vector3 position)
    {
        FruitType fruitType = fruitLevel == 0 ? FruitType.Base : FruitType.Bubble;
        Fruit spawnedFruit = null;

        switch (fruitType)
        {
            case FruitType.Base:
                spawnedFruit = baseFruitPool.Get();
                break;
            case FruitType.Bubble:
                spawnedFruit = bubbleFruitPool.Get();
                break;
            default:
                Debug.LogError($"Unsupported fruit type: {fruitType}");
                break;
        }

        if (spawnedFruit != null)
        {
            spawnedFruit.transform.position = position;
            spawnedFruit.gameObject.SetActive(true);
            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized; // Random direction
            spawnedFruit.GetComponent<Rigidbody2D>().AddForce(randomDirection * 7.5f, ForceMode2D.Impulse); // Add force for visual effect    
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
            default:
                Debug.LogError($"Unsupported fruit type: {fruit.FruitData.FruitType}");
                break;
        }
    }
}
