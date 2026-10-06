using UnityEngine;

[CreateAssetMenu(fileName = "FruitData", menuName = "Fruits/Fruit Data")]
public class FruitData : ScriptableObject
{
    [SerializeField] private int fruitLevel;
    [SerializeField] private int fruitScore;

    public int FruitLevel => fruitLevel;
    public int FruitScore => fruitScore;
}
