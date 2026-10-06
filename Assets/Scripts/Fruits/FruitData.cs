using UnityEngine;

[CreateAssetMenu(fileName = "FruitData", menuName = "Fruits/Fruit Data")]
public class FruitData : ScriptableObject
{
    [SerializeField] private int fruitLevel;
    [SerializeField] private int fruitScore;
    [SerializeField] private FruitType fruitType;

    public int FruitLevel => fruitLevel;
    public int FruitScore => fruitScore;
    public FruitType FruitType => fruitType;
}
