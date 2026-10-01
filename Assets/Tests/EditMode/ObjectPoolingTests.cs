using NUnit.Framework;
using UnityEngine;

public class ObjectPoolingTests
{
    private class TestPoolItem : MonoBehaviour
    {
    }

    [Test]
    public void Constructor_CreatesRequestedNumberOfInactiveObjects()
    {
        var prefab = new GameObject("PoolItemPrefab").AddComponent<TestPoolItem>();
        var pool = new ObjectPooling<TestPoolItem>(prefab, 3);

        var first = pool.Get();
        var second = pool.Get();
        var third = pool.Get();

        Assert.That(first, Is.Not.Null);
        Assert.That(second, Is.Not.Null);
        Assert.That(third, Is.Not.Null);
        Assert.That(first, Is.Not.SameAs(second));
        Assert.That(second, Is.Not.SameAs(third));
        Assert.That(pool.Get(), Is.Null, "The pool should be empty after the initial capacity is consumed.");
    }

    [Test]
    public void Get_ActivatesReturnedObject()
    {
        var prefab = new GameObject("PoolItemPrefab").AddComponent<TestPoolItem>();
        var pool = new ObjectPooling<TestPoolItem>(prefab, 1);

        var item = pool.Get();

        Assert.That(item, Is.Not.Null);
        Assert.That(item.gameObject.activeSelf, Is.True);
    }

    [Test]
    public void Return_DeactivatesObject()
    {
        var prefab = new GameObject("PoolItemPrefab").AddComponent<TestPoolItem>();
        var pool = new ObjectPooling<TestPoolItem>(prefab, 1);

        var item = pool.Get();
        pool.Return(item);

        Assert.That(item.gameObject.activeSelf, Is.False);
    }

    [Test]
    public void Get_ReusesPreviouslyReturnedObject()
    {
        var prefab = new GameObject("PoolItemPrefab").AddComponent<TestPoolItem>();
        var pool = new ObjectPooling<TestPoolItem>(prefab, 1);

        var first = pool.Get();
        pool.Return(first);

        var second = pool.Get();

        Assert.That(second, Is.SameAs(first));
    }

    [Test]
    public void Get_WhenPoolIsExhausted_ReturnsNull()
    {
        var prefab = new GameObject("PoolItemPrefab").AddComponent<TestPoolItem>();
        var pool = new ObjectPooling<TestPoolItem>(prefab, 1);

        var first = pool.Get();
        var second = pool.Get();

        Assert.That(first, Is.Not.Null);
        Assert.That(second, Is.Null);
    }
}
