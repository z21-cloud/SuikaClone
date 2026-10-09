using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using SuikaClone.Factories;
using SuikaClone.Fruits;
using SuikaClone.Managers;
using SuikaClone.Pools;
using UnityEngine;
using UnityEngine.TestTools;

public class MergeCollisionTests
{
    private readonly List<GameObject> gameObjects = new List<GameObject>();
    private readonly List<FruitData> fruitDataAssets = new List<FruitData>();
    private MergeManager mergeManager;
    private ScoreManager scoreManager;
    private FruitSpawnManager fruitSpawnManager;
    private BaseFruitPool baseFruitPool;
    private BubbleFruitPool bubbleFruitPool;
    private SquareFruitPool squareFruitPool;
    private TestFruitFactory baseFruitFactory;
    private TestFruitFactory bubbleFruitFactory;
    private TestFruitFactory squareFruitFactory;

    [SetUp]
    public void SetUp()
    {
        mergeManager = CreateComponent<MergeManager>("MergeManager");
        scoreManager = CreateComponent<ScoreManager>("ScoreManager");
        fruitSpawnManager = CreateComponent<FruitSpawnManager>("FruitSpawnManager");
        baseFruitPool = CreateComponent<BaseFruitPool>("BaseFruitPool");
        bubbleFruitPool = CreateComponent<BubbleFruitPool>("BubbleFruitPool");
        squareFruitPool = CreateComponent<SquareFruitPool>("SquareFruitPool");

        SetPrivateField(fruitSpawnManager, "baseFruitPool", baseFruitPool);
        SetPrivateField(fruitSpawnManager, "bubbleFruitPool", bubbleFruitPool);
        SetPrivateField(fruitSpawnManager, "squareFruitPool", squareFruitPool);

        baseFruitFactory = new TestFruitFactory(
            CreateFruitData(0, 1, FruitType.Base),
            mergeManager,
            gameObjects);
        bubbleFruitFactory = new TestFruitFactory(
            CreateFruitData(1, 10, FruitType.Bubble),
            mergeManager,
            gameObjects);
        squareFruitFactory = new TestFruitFactory(
            CreateFruitData(2, 20, FruitType.Square),
            mergeManager,
            gameObjects);

        baseFruitPool.Initialize(baseFruitFactory);
        bubbleFruitPool.Initialize(bubbleFruitFactory);
        squareFruitPool.Initialize(squareFruitFactory);

        mergeManager.Initialize(fruitSpawnManager, scoreManager);
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        foreach (var gameObject in gameObjects)
        {
            if (gameObject != null)
            {
                gameObject.SetActive(false);
                Object.Destroy(gameObject);
            }
        }

        foreach (var fruitData in fruitDataAssets)
        {
            if (fruitData != null)
            {
                Object.Destroy(fruitData);
            }
        }

        yield return null;
    }

    [Test]
    public void MergeFruits_WhenCalledAgainForSamePair_ProcessesPairOnlyOnce()
    {
        Fruit first = GetBubble(Vector2.zero);
        Fruit second = GetBubble(Vector2.right);

        mergeManager.MergeFruits(first, second);
        mergeManager.MergeFruits(first, second);

        Assert.That(scoreManager.CurrentScore, Is.EqualTo(20));
        Assert.That(CountActive(bubbleFruitFactory), Is.EqualTo(0));
        Assert.That(CountActive(squareFruitFactory), Is.EqualTo(1));
    }

    [UnityTest]
    public IEnumerator ThreeTouchingBubbles_MergeOnePairOnly()
    {
        GetBubble(new Vector2(-0.75f, 0f));
        GetBubble(Vector2.zero);
        GetBubble(new Vector2(0.75f, 0f));

        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForFixedUpdate();
        }

        Assert.That(scoreManager.CurrentScore, Is.EqualTo(20));
        Assert.That(CountActive(bubbleFruitFactory), Is.EqualTo(1));
        Assert.That(CountActive(squareFruitFactory), Is.EqualTo(1));
    }

    private T CreateComponent<T>(string objectName) where T : Component
    {
        var gameObject = new GameObject(objectName);
        gameObjects.Add(gameObject);
        return gameObject.AddComponent<T>();
    }

    private Fruit GetBubble(Vector2 position)
    {
        Fruit fruit = bubbleFruitPool.Get();
        fruit.transform.position = position;

        Rigidbody2D body = fruit.GetComponent<Rigidbody2D>();
        body.linearVelocity = Vector2.zero;
        body.angularVelocity = 0f;

        fruit.gameObject.SetActive(true);
        return fruit;
    }

    private FruitData CreateFruitData(int level, int score, FruitType fruitType)
    {
        FruitData fruitData = ScriptableObject.CreateInstance<FruitData>();
        fruitDataAssets.Add(fruitData);
        SetPrivateField(fruitData, "fruitLevel", level);
        SetPrivateField(fruitData, "fruitScore", score);
        SetPrivateField(fruitData, "fruitType", fruitType);
        return fruitData;
    }

    private static void SetPrivateField(object target, string fieldName, object value)
    {
        FieldInfo field = target.GetType().GetField(
            fieldName,
            BindingFlags.Instance | BindingFlags.NonPublic);
        Assert.That(field, Is.Not.Null, $"Could not find field '{fieldName}'.");
        field.SetValue(target, value);
    }

    private static int CountActive(TestFruitFactory factory)
    {
        int activeCount = 0;
        foreach (Fruit fruit in factory.CreatedFruits)
        {
            if (fruit.gameObject.activeInHierarchy)
            {
                activeCount++;
            }
        }

        return activeCount;
    }

    private sealed class TestFruitFactory : IFactory<Fruit>
    {
        private readonly FruitData fruitData;
        private readonly MergeManager mergeManager;
        private readonly List<GameObject> gameObjects;

        public readonly List<Fruit> CreatedFruits = new List<Fruit>();

        public TestFruitFactory(FruitData fruitData, MergeManager mergeManager, List<GameObject> gameObjects)
        {
            this.fruitData = fruitData;
            this.mergeManager = mergeManager;
            this.gameObjects = gameObjects;
        }

        public Fruit Create()
        {
            var gameObject = new GameObject(fruitData.FruitType.ToString());
            gameObjects.Add(gameObject);

            Fruit fruit = gameObject.AddComponent<Fruit>();
            SetPrivateField(fruit, "fruitData", fruitData);
            fruit.Initialize(mergeManager);

            Rigidbody2D body = gameObject.AddComponent<Rigidbody2D>();
            body.gravityScale = 0f;
            body.freezeRotation = true;
            gameObject.AddComponent<CircleCollider2D>();

            CreatedFruits.Add(fruit);
            return fruit;
        }
    }
}
