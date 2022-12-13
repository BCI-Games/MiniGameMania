using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Submissions.BoatRescue
{
    public class GameManager : MonoBehaviour
    {
        [Header("Initialize")]
        [SerializeField] private GameObject[] _initializables = Array.Empty<GameObject>();
        [SerializeField] private GameObject _onInitializeCompletePrefab;

        [Header("Levels")]
        [SerializeField] private int _levelIndex;
        [SerializeField] private LevelBuilderInstructions[] _levelSequences = Array.Empty<LevelBuilderInstructions>();


        public static GameManager Instance;

        public Level ActiveLevel { get; private set; }

        //Services
        private InputManager _inputManager;
        private UIManager _uiManager;
        private LevelBuilder _builder;

        private int _movesCount = 0;
        private int _totalRewards = 0;
        private int _rewardsCount = 0;

        private Coroutine _waitForMovementCoroutine;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                foreach (var initGO in _initializables)
                {
                    IInitializable[] initializables = initGO.GetComponents<IInitializable>();
                    foreach (var initializable in initializables)
                    {
                        initializable.Initialize();
                    }
                }

                _inputManager = InputManager.Instance;
                _inputManager.OnDirectionTriggered.AddListener(MovePlayers);
                
                Instantiate(_onInitializeCompletePrefab);
            }
        }

        #region Managers

        public void RegisterBuilder(LevelBuilder builder)
        {
            _builder = builder;
            if (ActiveLevel == null)
            {
                EnterLevel();
            }
        }

        public void UnregisterBuilder(LevelBuilder builder)
        {
            if (_builder == builder)
            {
                _builder = null;
            }
        }

        public void RegisterUIManager(UIManager manager)
        {
            _uiManager = manager;
        }

        public void UnregisterUIManager(UIManager manager)
        {
            if (_uiManager == manager)
            {
                _uiManager = null;
            }
        }

        #endregion

        [ContextMenu("Build Test Level")]
        public void EnterLevel()
        {
            Reset();

            if (_levelSequences.Length == 0)
            {
                return;
            }

            EnterLevel(_levelIndex);
        }

        [ContextMenu("Destroy Test Level")]
        public void LeaveLevel()
        {
            if (ActiveLevel == null)
            {
                return;
            }

            _inputManager.ToggleStimulusInput(false);

            _builder.DestroyLevel(ActiveLevel);
            ActiveLevel = null;
        }

        public void EnterLevel(int levelNumber)
        {
            if (levelNumber < 0)
            {
                levelNumber = _levelSequences.Length - 1;
            }
            else if (levelNumber >= _levelSequences.Length)
            {
                levelNumber = 0;
            }

            _levelIndex = levelNumber;
            ActiveLevel = _builder.BuildLevel(_levelSequences[_levelIndex]);
            _uiManager.InitializeLevelDetails(ActiveLevel);
            _totalRewards = ActiveLevel.TotalRewards;

            _inputManager.ToggleStimulusInput(true);
        }

        public void NextLevel()
        {
            LeaveLevel();
            EnterLevel(++_levelIndex);
        }

        [ContextMenu("Restart Test Level")]
        public void RestartLevel()
        {
            LeaveLevel();
            EnterLevel();
        }

        public void MovePlayers(PlayerMoveDirection direction)
        {
            if (ActiveLevel == null || _waitForMovementCoroutine != null)
            {
                return;
            }

            _inputManager.ToggleStimulusInput(false);

            UpdateMoveScore();
            foreach (var player in ActiveLevel.Players)
            {
                player.Move(direction);
            }

            _waitForMovementCoroutine = StartCoroutine(WaitForPlayerMovement());
        }

        public void TriggerLevelWin()
        {
            _inputManager.ToggleStimulusInput(false);
            _uiManager.ToggleGameWin(true);
            _inputManager.ToggleStimulusInput(true);
        }

        public void TriggerGameOver()
        {
            _inputManager.ToggleStimulusInput(false);
            _uiManager.ToggleGameOver(true);
            _inputManager.ToggleStimulusInput(true);
        }

        public void UpdateRewardScore()
        {
            ++_rewardsCount;
            _uiManager.UpdateRewardScore(_rewardsCount);

            if (_rewardsCount >= _totalRewards)
            {
                TriggerLevelWin();
            }
        }

        public void UpdateMoveScore()
        {
            ++_movesCount;
            _uiManager.UpdateMovementScore(_movesCount);
        }

        private void Reset()
        {
            ActiveLevel = null;
            _movesCount = 0;
            _rewardsCount = 0;
        }

        private IEnumerator WaitForPlayerMovement()
        {
            yield return new WaitForSecondsRealtime(0.2f);
            yield return new WaitUntil(() => !ActiveLevel.PlayersMoving());

            _inputManager.ToggleStimulusInput(true);
            _waitForMovementCoroutine = null;
        }
    }
}