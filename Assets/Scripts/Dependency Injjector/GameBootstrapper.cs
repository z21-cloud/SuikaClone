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

        [Header("Effects")]
        [SerializeField] private EffectManager effectManager;
        [SerializeField] private EffectsFactory effectsFactory;
        [SerializeField] private EffectsPool effectsPool;

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
                mergeManager
            );

            
            playerController.Initialize(fruitSpawnManager);

            effectManager.Initialize(effectsFactory, effectsPool);
            mergeManager.Initialize(fruitSpawnManager, scoreManager, effectManager);

            HighScoreManager highScoreManager = new();
            highScoreManager.Load();
            
            gameManager.Initialize(playerController, fruitSpawnManager, scoreManager, highScoreManager);
            loseLine.Initialize(gameManager);
        }
    }
}
