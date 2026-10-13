using SuikaClone.Factories;
using SuikaClone.Managers;
using SuikaClone.Pools;
using UnityEngine;

namespace SuikaClone.Core
{
    public class GameBootstrapper : MonoBehaviour
    {

        [Header("Base Factories")]
        [SerializeField] private BaseFruitFactory baseFruitFactory;
        [SerializeField] private BaseRedFruitFactory baseRedFruitFactory;
        [SerializeField] private BaseGreenFruitFactory baseGreenFruitFactory;
        [SerializeField] private BaseBlueFruitFactory baseBlueFruitFactory;

        [Header("Bubble Factories")]
        [SerializeField] private BubbleFruitFactory bubbleFruitFactory;
        [SerializeField] private BubbleRedFruitFactory bubbleRedFruitFactory;
        [SerializeField] private BubbleGreenFruitFactory bubbleGreenFruitFactory;
        [SerializeField] private BubbleBlueFruitFactory bubbleBlueFruitFactory;

        [Header("Square Factories")]
        [SerializeField] private SquareFruitFactory squareFruitFactory;
        [SerializeField] private SquareRedFruitFactory squareRedFruitFactory;
        [SerializeField] private SquareGreenFruitFactory squareGreenFruitFactory;

        [Header("Pools")]
        [SerializeField] private BaseFruitPool baseFruitPool;
        [SerializeField] private BubbleFruitPool bubbleFruitPool;
        [SerializeField] private SquareFruitPool squareFruitPool;

        [Header("Managers")]
        [SerializeField] private MergeManager mergeManager;
        [SerializeField] private FruitSpawnManager fruitSpawnManager;
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private GameManager gameManager;

        [Header("Player Controller")]
        [SerializeField] private PlayerController playerController;

        [Header("Lose Line")]
        [SerializeField] private LoseLine loseLine;

        private void Awake()
        {
            fruitSpawnManager.Initialize(
                baseFruitFactory,
                baseRedFruitFactory,
                baseGreenFruitFactory,
                baseBlueFruitFactory,
                bubbleFruitFactory,
                bubbleRedFruitFactory,
                bubbleGreenFruitFactory,
                bubbleBlueFruitFactory,
                squareFruitFactory,
                squareRedFruitFactory,
                squareGreenFruitFactory,
                mergeManager);
            
            playerController.Initialize(fruitSpawnManager);

            mergeManager.Initialize(fruitSpawnManager, scoreManager);

            gameManager.Initialize(playerController, fruitSpawnManager, scoreManager);
            loseLine.Initialize(gameManager);
        }
    }
}
