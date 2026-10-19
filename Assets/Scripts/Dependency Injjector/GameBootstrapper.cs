using SuikaClone.Factories;
using SuikaClone.Managers;
using SuikaClone.Pools;
using UnityEngine;

namespace SuikaClone.Core
{
    public class GameBootstrapper : MonoBehaviour
    {
        [Header("Base Factories")]
        [SerializeField] private BaseFruitFactory _baseFruitFactory;
        [SerializeField] private BaseRedFruitFactory _baseRedFruitFactory;
        [SerializeField] private BaseGreenFruitFactory _baseGreenFruitFactory;
        [SerializeField] private BaseBlueFruitFactory _baseBlueFruitFactory;

        [Header("Bubble Factories")]
        [SerializeField] private BubbleFruitFactory _bubbleFruitFactory;
        [SerializeField] private BubbleRedFruitFactory _bubbleRedFruitFactory;
        [SerializeField] private BubbleGreenFruitFactory _bubbleGreenFruitFactory;
        [SerializeField] private BubbleBlueFruitFactory _bubbleBlueFruitFactory;

        [Header("Square Factories")]
        [SerializeField] private SquareFruitFactory _squareFruitFactory;
        [SerializeField] private SquareRedFruitFactory _squareRedFruitFactory;
        [SerializeField] private SquareGreenFruitFactory _squareGreenFruitFactory;

        [Header("Effects")]
        [SerializeField] private EffectManager _effectManager;
        [SerializeField] private EffectsFactory _effectsFactory;
        [SerializeField] private EffectsPool _effectsPool;

        [Header("Managers")]
        [SerializeField] private MergeManager _mergeManager;
        [SerializeField] private FruitSpawnManager _fruitSpawnManager;
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private GameManager _gameManager;

        [Header("Player Controller")]
        [SerializeField] private PlayerController _playerController;

        [Header("Lose Line")]
        [SerializeField] private LoseLine _loseLine;

        private void Awake()
        {            
            _fruitSpawnManager.Initialize(
                _baseFruitFactory,
                _baseRedFruitFactory,
                _baseGreenFruitFactory,
                _baseBlueFruitFactory,
                _bubbleFruitFactory,
                _bubbleRedFruitFactory,
                _bubbleGreenFruitFactory,
                _bubbleBlueFruitFactory,
                _squareFruitFactory,
                _squareRedFruitFactory,
                _squareGreenFruitFactory,
                _mergeManager
            );

            
            _playerController.Initialize(_fruitSpawnManager);

            _effectManager.Initialize(_effectsFactory, _effectsPool);
            _mergeManager.Initialize(_fruitSpawnManager, _scoreManager, _effectManager);

            HighScoreManager highScoreManager = new();
            highScoreManager.Load();
            
            _gameManager.Initialize(_playerController, _fruitSpawnManager, _scoreManager, highScoreManager);
            _loseLine.Initialize(_gameManager);
        }
    }
}
